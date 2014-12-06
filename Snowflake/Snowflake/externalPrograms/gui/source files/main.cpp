#include "mainwindow.h"
#include <QApplication>
#include <QFile>


int main(int argc, char *argv[])
{
    // Load an application style
    QFile styleFile(".\\styles\\stylesheet_black_orange.css");
    styleFile.open( QFile::ReadOnly );

    // Apply the loaded stylesheet
    QString style( styleFile.readAll() );


    QApplication a(argc, argv);
    a.setStyleSheet(style);
    MainWindow w;
    w.show();

    return a.exec();
}
