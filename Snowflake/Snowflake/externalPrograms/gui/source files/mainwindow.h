#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>
#include <QCheckBox>
#include <thread>

namespace Ui {
class MainWindow;
}

class MainWindow : public QMainWindow
{
    Q_OBJECT

public:
    explicit MainWindow(QWidget *parent = 0);
    ~MainWindow();

private slots:
    void on_radioButton_clicked();

    void on_radioButton_2_clicked();

    void on_m_SliderForceX_valueChanged(int value);

    void on_m_SliderForceY_valueChanged(int value);

    void on_m_SliderForceZ_valueChanged(int value);

    void on_pushButton_clicked();

private:
    Ui::MainWindow *ui;
    std::thread m_SinusThreads[3];
    int m_SliderValues[3];

    bool m_StopProgram;
    void sinWatcher(std::string sendTag, const QCheckBox* cb, const int* value);
};

#endif // MAINWINDOW_H
