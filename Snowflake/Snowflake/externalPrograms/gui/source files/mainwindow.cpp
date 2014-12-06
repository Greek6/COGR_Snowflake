#include "mainwindow.h"
#include "ui_mainwindow.h"
#include <iostream>
#include <fstream>
#include <thread>
#include <mutex>

const std::string dataFile = "../data.file";
std::mutex mutex;


void sendData(std::string data){
    std::lock_guard<std::mutex> guard(mutex);
    int counter = 0;

    while(std::fstream(dataFile, std::fstream::in).good()){
        std::this_thread::sleep_for(std::chrono::milliseconds(200));
        if (counter > 10)
            break;
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

    this->m_SinusThreads[0] = std::thread(&MainWindow::sinWatcher, this, "force x: ", this->ui->m_CheckBox_SinX, &this->m_SliderValues[0]);
    this->m_SinusThreads[1] = std::thread(&MainWindow::sinWatcher, this, "force y: ", this->ui->m_CheckBox_SinY, &this->m_SliderValues[1]);
    this->m_SinusThreads[2] = std::thread(&MainWindow::sinWatcher, this, "force z: ", this->ui->m_CheckBox_SinZ, &this->m_SliderValues[2]);
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
        value = pow(value, 3);

    this->m_SliderValues[0] = value;
    //qDebug("value changed to " + std::to_string(value));
    if (!this->ui->m_CheckBox_SinX->isChecked())
        sendData("force x: " + std::to_string(value));
}
void MainWindow::on_m_SliderForceY_valueChanged(int value)
{
    if (ui->m_HighspeedX->isChecked())
        value = pow(value, 3);

    this->m_SliderValues[1] = value;
    if (!this->ui->m_CheckBox_SinY->isChecked())
        sendData("force y: " + std::to_string(value));
}
void MainWindow::on_m_SliderForceZ_valueChanged(int value)
{
    if (ui->m_HighspeedX->isChecked())
        value = pow(value, 3);

    this->m_SliderValues[2] = value;
    if (!this->ui->m_CheckBox_SinZ->isChecked())
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

