using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO.Ports; //подключаем класс ком-порта
using System.Windows.Forms;

namespace KeyLayoutIndicator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Resize += new System.EventHandler(this.Form1_Resize); // добавляем событие на изменение окна
            notifyIcon1.Visible = false;
            CBComs.Items.Clear();
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

        private void timer1_Tick(object sender, EventArgs e)
        {
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
                //lLangStatus.Text = txt;


            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            serialPort1.PortName = CBComs.Text;
            serialPort1.Open();
            timer1.Enabled = true;
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
