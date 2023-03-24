#include "mainwindow.h"
#include "./ui_mainwindow.h"
#include <QtSerialPort/QSerialPort>
#include <QtSerialPort/QSerialPortInfo>

#include <QTimer>
#include <windows.h>

#pragma comment(lib, "User32.lib")

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);

    updateComList();

    connect(timer, SIGNAL(timeout()), this, SLOT(sendLayout()));


}

MainWindow::~MainWindow()
{
    delete ui;
}


void MainWindow::on_pushButton_clicked()
{

       // Write data to the serial port
       QString data = ui->textEdit->toPlainText();
       qint64 bytesWritten = serial.write(data.toUtf8());
       //qDebug() << bytesWritten << "bytes written to serial port.";

       // Read data from the serial port
       QByteArray responseData = serial.readAll();
       //qDebug() << "Response from serial port:" << responseData;
}


void MainWindow::on_pushButton_2_clicked()
{
    updateComList();
}


void MainWindow::on_comboBox_currentIndexChanged(int index)
{
    port = ui->comboBox->currentText();
}

void MainWindow::updateComList()
{
    ui->comboBox->clear();
    const auto infos = QSerialPortInfo::availablePorts();

    for (const QSerialPortInfo &info : infos) {
      ui->comboBox->addItem(info.portName());
    }
}


void MainWindow::on_pushButton_3_clicked()
{
    serial.setPortName(port); // Replace with your serial port name
    serial.setBaudRate(QSerialPort::Baud115200);
    serial.setDataBits(QSerialPort::Data8);
    serial.setParity(QSerialPort::NoParity);
    serial.setStopBits(QSerialPort::OneStop);
    serial.setFlowControl(QSerialPort::NoFlowControl);

    if (!serial.open(QIODevice::ReadWrite)) {
        //qDebug() << "Failed to open serial port!";
        return;
     }

   // qDebug() << "Serial port opened.";
    timer->start(100);
}


void MainWindow::on_pushButton_4_clicked()
{
    serial.close();
    //qDebug() << "Serial port closed.";
}


void MainWindow::on_pushButton_5_clicked()
{
    HKL currentLayout = GetKeyboardLayout(0);
    LANGID currentLangID = LOWORD(currentLayout);
    //qDebug() << "Current input language identifier: " << currentLangID;


    //ui->textEdit->setText(QString::fromWCharArray(currentLangID));
}

void MainWindow::sendLayout()
{
    // Get the handle and thread ID of the active window
    HWND activeWindow = GetForegroundWindow();
    DWORD activeThreadID = GetWindowThreadProcessId(activeWindow, NULL);

    // Get the keyboard layout handle associated with the active thread
    HKL activeLayout = GetKeyboardLayout(activeThreadID);
    LANGID activeLangID = LOWORD(activeLayout);

    //qDebug() << "Active window layout identifier: " << activeLangID;

       int capsLock = GetKeyState(VK_CAPITAL);
       int numLock = GetKeyState(VK_NUMLOCK);
       int scrollLock = GetKeyState(VK_SCROLL);
       int insertKey = GetKeyState(VK_INSERT);

//       qDebug() << "Caps Lock is " << (capsLock ? "on" : "off");
//       qDebug() << "Num Lock is " << (numLock ? "on" : "off");
//       qDebug() << "Scroll Lock is " << (scrollLock ? "on" : "off");
//       qDebug() << "Insert key is " << (insertKey ? "pressed" : "not pressed");
    //формируем строку, где: 1,2,3,4,5,6,7 светодиоды справа налево
     /*  1 - тип передачи (1 - адресное включение светодиодов)
      *  2 - вкл\выкл первый свтодиод
      *  3 - вкл\выкл второй свтодиод
      *  4 - вкл\выкл третий свтодиод
      *  5 - вкл\выкл четветрый свтодиод
      *  6 - вкл\выкл пятый свтодиод
      *  7 - яркость светодиодов
      */
    QString dataSTR = "1,";//
    dataSTR += QString::number(insertKey);  dataSTR +=",";
    dataSTR += QString::number(scrollLock); dataSTR +=",";
    dataSTR += QString::number(capsLock);   dataSTR +=",";
    dataSTR += QString::number(numLock);    dataSTR +=",";
    dataSTR += activeLangID==1033 ? "1" : "0";   dataSTR +=","; //если английский, то 1, иначе 0
    dataSTR += "15"; //яркость

    qint64 bytesWritten = serial.write(dataSTR.toUtf8());
    //qDebug() << bytesWritten << "bytes written to serial port.";



}

