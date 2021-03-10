using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO.Ports; //подключаем класс ком-порта
using System.Threading;
using System.Threading.Tasks; // подключаем для таймера
using System.Windows.Forms;

namespace KeyLayoutIndicator
{
    public partial class Form1 : Form
    {
        bool timerEnable = false;
        


        public Form1()
        {
            InitializeComponent();
            // добавляем событие на изменение окна
            this.Resize += new System.EventHandler(this.Form1_Resize); 
            notifyIcon1.Visible = false;

            //CBComs.Items.Clear(); чистим список COM портов 
            // Получаем список COM портов доступных в системе
            string[] portnames = SerialPort.GetPortNames();
            // Проверяем есть ли доступные
            if (portnames.Length == 0)
            {
                MessageBox.Show("COM PORT not found");
            }
            foreach (string portName in portnames)
            {
                //добавляем доступные COM порты в список           
                CBComs.Items.Add(portName);
                Console.WriteLine(portnames.Length);
                if (portnames[0] != null)
                {
                    CBComs.SelectedItem = portnames[0];
                }
            }
            System.Threading.Timer tim2 = new System.Threading.Timer(TimerTick, null, 1000, 500);



        }

        void TimerTick(object sender)
        {
             getLayout();
            
        }



        private void getLayout()
        {
            if (!timerEnable) return;
            InputLanguage myCurrentLanguage = InputLanguage.CurrentInputLanguage;
            if (myCurrentLanguage != null)
            {

                String txt = myCurrentLanguage.LayoutName;
                if (txt == "Русская")
                {
                    PortWrite("1");
                }
                if (txt == "США")
                {
                    PortWrite("3");
                }
                lLangStatus.Text = txt;


            }
        }

        void PortWrite(String txt) //функция записи переданного параметра в порт
        {
            try
            {
                //установить проверку, сменился ли язык, что бы не спамить ком порт
                //serialPort1.Open();
                //lComStatus.Text = "Работает";
                //System.Threading.Thread.Sleep(100);
                // небольшая пауза, ведь SerialPort не терпит суеты
                serialPort1.Write(txt);
                //serialPort1.Close();
                
            }
            catch
            {
                //lComStatus.Text = "Ошибка открытия порта";
            }

        }

       
        private void button1_Click(object sender, EventArgs e)
        {
            serialPort1.Close();
            serialPort1.PortName = CBComs.Text;
            serialPort1.Open();
            timerEnable = true;
            //tm.Start();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            // проверяем наше окно, и если оно было свернуто, делаем событие        
            if (WindowState == FormWindowState.Minimized)
            {
                // прячем наше окно из панели
                this.ShowInTaskbar = false;
                // делаем нашу иконку в трее активной
                notifyIcon1.Visible = true;
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // делаем нашу иконку скрытой
            notifyIcon1.Visible = false;
            // возвращаем отображение окна в панели
            this.ShowInTaskbar = true;
            //разворачиваем окно
            WindowState = FormWindowState.Normal;
        }
    }
}
