#include "mainwindow.h"
#include "ui_mainwindow.h"
#include <iostream>
#include <fstream>
#include <thread>
#include <mutex>
#include "simplexnoise.cpp" // .h doesn't seem to work, cpp fixed this issue

const std::string dataFile = "../data.file";
std::mutex mutex;


void sendData(std::string data){
    std::lock_guard<std::mutex> guard(mutex);
    for (int counter = 0; std::fstream(dataFile, std::fstream::in).good(); ++counter){
        std::this_thread::sleep_for(std::chrono::milliseconds(200));
        if (counter > 5)
            return;
    }

    std::fstream stream(dataFile, std::fstream::out);
    stream << data;
}




MainWindow::MainWindow(QWidget *parent) :
    QMainWindow(parent),
    ui(new Ui::MainWindow),
    m_StopProgram(false)
{
    ui->setupUi(this);

    for(int i = 0; i < 3; ++i)
        this->m_SliderValues[i] = 0;

    //this->m_SinusThreads[0] = std::thread(&MainWindow::sinWatcher, this, "force x: ", this->ui->m_CheckBoxX, &this->m_SliderValues[0]);
    //this->m_SinusThreads[1] = std::thread(&MainWindow::sinWatcher, this, "force y: ", this->ui->m_CheckBoxY, &this->m_SliderValues[1]);
    //this->m_SinusThreads[2] = std::thread(&MainWindow::sinWatcher, this, "force z: ", this->ui->m_CheckBoxZ, &this->m_SliderValues[2]);

    this->m_SinusThread = std::thread(&MainWindow::sinWatcher_test, this);
}

MainWindow::~MainWindow()
{
    this->m_StopProgram = true;
    delete ui;

    for(int i = 0; i < 3; ++i)
        this->m_SinusThreads[i].join();
}

void MainWindow::on_radioButton_clicked()
{
    sendData("wind");
}

void MainWindow::on_radioButton_2_clicked()
{
    sendData("tornado");
}




void MainWindow::on_m_SliderForceX_valueChanged(int value)
{
    if (ui->m_HighspeedX->isChecked())
        value *= 3;

    this->m_SliderValues[0] = value;
    //qDebug("value changed to " + std::to_string(value));
    if (!this->ui->m_CheckBoxX->isChecked())
        sendData("force x: " + std::to_string(value));
}
void MainWindow::on_m_SliderForceY_valueChanged(int value)
{
    if (ui->m_HighspeedY->isChecked())
        value *= 3;

    this->m_SliderValues[1] = value;
    if (!this->ui->m_CheckBoxY->isChecked())
        sendData("force y: " + std::to_string(value));
}
void MainWindow::on_m_SliderForceZ_valueChanged(int value)
{
    if (ui->m_HighspeedZ->isChecked())
        value *= 3;

    this->m_SliderValues[2] = value;
    if (!this->ui->m_CheckBoxZ->isChecked())
        sendData("force z: " + std::to_string(value));
}

void MainWindow::on_pushButton_clicked()
{
    ui->m_SliderForceX->setValue(0);
    ui->m_SliderForceY->setValue(0);
    ui->m_SliderForceZ->setValue(0);
}


void MainWindow::sinWatcher(std::string sendTag, const QCheckBox *cb, const int *value)
{
    float angle = 0.f;
    using namespace std::chrono;

    auto start = high_resolution_clock::now();

    while(!this->m_StopProgram){
        if (cb->isChecked())
            sendData(sendTag + std::to_string(std::sinf(angle) * (*value)));
        else
            std::this_thread::sleep_for(milliseconds(500));

        angle = duration_cast<milliseconds>(high_resolution_clock::now() - start).count() * 0.01f;
    }
}





void MainWindow::sinWatcher_test()
{
    using namespace std::chrono;

    static const std::string sendTag_x = "force x: ";
    static const std::string sendTag_y = "force y: ";
    static const std::string sendTag_z = "force z: ";
    static const int longSleep = 500; // ms

    float angle = 0.f;
    float currentNoisePoint = 0.f;
    auto start = high_resolution_clock::now();

    while(!this->m_StopProgram){
        std::string toSend("");
        bool sinWatcherActive = false;
        // force
        if (this->ui->m_RadioForce->isChecked()){
            double k, o;
            static const double k_start_value = 0.5;
            static const double PI = 3.141592653589793238463;

            if (this->ui->m_CheckBoxX->isChecked()){
                k = (double)this->ui->m_K_X->value() / (double)this->ui->m_K_X->maximum() + k_start_value;
                o = (double)this->ui->m_O_X->value() / (double)this->ui->m_O_X->maximum() * PI;
                if (toSend.size())  // always false, but uses same code as y, z
                    toSend += '\n';

                toSend += sendTag_x + std::to_string(std::sinf(k * angle + o) * this->m_SliderValues[0]);
                sinWatcherActive = true;
            }
            if (this->ui->m_CheckBoxY->isChecked()){
                k = (double)this->ui->m_K_Y->value() / (double)this->ui->m_K_Y->maximum() + k_start_value;
                o = (double)this->ui->m_O_Y->value() / (double)this->ui->m_O_Y->maximum() * PI;
                if (toSend.size())
                    toSend += '\n';

                toSend += sendTag_y + std::to_string(std::sinf(k * angle + o) * this->m_SliderValues[1]);
                sinWatcherActive = true;
            }
            if (this->ui->m_CheckBoxZ->isChecked()){
                k = (double)this->ui->m_K_Z->value() / (double)this->ui->m_K_Z->maximum() + k_start_value;
                o = (double)this->ui->m_O_Z->value() / (double)this->ui->m_O_Z->maximum() * PI;
                if (toSend.size())
                    toSend += '\n';

                toSend += sendTag_z + std::to_string(std::sinf(k * angle + o) * this->m_SliderValues[2]);
                sinWatcherActive = true;
            }

            angle = duration_cast<milliseconds>(high_resolution_clock::now() - start).count() * 0.01f;
        }
        // noise
        else if (this->ui->m_RadioNoise->isChecked()){
            double octaves, persistance;
            static const double k_downScale = 1.0;
            static const double k_startValue = 0.001;

            if (this->ui->m_CheckBoxX->isChecked()){
                octaves     = (double)this->ui->m_K_X->value() / k_downScale + k_startValue;
                persistance = (double)this->ui->m_O_X->value() / (double)this->ui->m_O_X->maximum();
                if (toSend.size())
                    toSend += '\n';

                toSend += sendTag_x + std::to_string((double)this->m_SliderValues[0] * octave_noise_2d(octaves, persistance, 1, currentNoisePoint, 0.f));
                sinWatcherActive = true;
            }
            if (this->ui->m_CheckBoxY->isChecked()){
                octaves     = (double)this->ui->m_K_Y->value() / k_downScale + k_startValue;
                persistance = (double)this->ui->m_O_Y->value() / (double)this->ui->m_O_Y->maximum();
                if (toSend.size())
                    toSend += '\n';

                toSend += sendTag_y + std::to_string((double)this->m_SliderValues[1] * octave_noise_2d(octaves, persistance, 1, 0.f, currentNoisePoint));
                sinWatcherActive = true;
            }
            if (this->ui->m_CheckBoxZ->isChecked()){
                octaves     = (double)this->ui->m_K_Z->value() / k_downScale + k_startValue;
                persistance = (double)this->ui->m_O_Z->value() / (double)this->ui->m_O_Z->maximum();
                if (toSend.size())
                    toSend += '\n';

                toSend += sendTag_z + std::to_string((double)this->m_SliderValues[2] * octave_noise_2d(octaves, persistance, 1, currentNoisePoint, currentNoisePoint));
                sinWatcherActive = true;
            }

            currentNoisePoint = duration_cast<milliseconds>(high_resolution_clock::now() - start).count() * 0.001f;
        }
        // tornado => nothing to do
        else
            std::this_thread::sleep_for(std::chrono::milliseconds(longSleep));

        // send data
        if (sinWatcherActive)
            //qDebug(toSend.c_str());
            sendData(toSend);

        // wait some time
        std::this_thread::sleep_for(std::chrono::milliseconds(200));
    }
}

