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
using System.Runtime.InteropServices; // подключаем для использования DllImport 

namespace KeyLayoutIndicator
{
    public partial class Form1 : Form
    {
        // задаем переменные класса
        SerialPort currentPort; // глобально объявляем используемы порт, т.к. будем обращаться из разных методов

        //bool timerEnable = false;
        //String CurrentLayout = "";
        bool minimized = false;
        System.Threading.Timer timer;
        //String namePort = "";

        //private delegate void updateDelegate(string txt);


        public Form1() // метод создания формы
        {
            InitializeComponent();
            this.Resize += new System.EventHandler(this.Form1_Resize);  // добавляем событие на изменение окна
            notifyIcon1.Visible = false;    // приячем иконку в трее, пока программа не свернута
            ComPortScan(); // сканируем список компортов и добавляем их в комбобокс
        }

        void ComPortScan()
        {
            CBComs.Items.Clear(); //чистим список COM портов - необходимо, когда вызывается на кнопке "обновить список"            
            string[] portnames = SerialPort.GetPortNames(); // Получаем список COM портов доступных в системе
            // Проверяем есть ли доступные порты, если нет, выводим сообщение что не найдены
            if (portnames.Length == 0)
            {
                MessageBox.Show("COM PORT not found");
            }
            foreach (string portName in portnames) // прочесываем массив с именами компортов и добавляем их в комбобокс
            {
                CBComs.Items.Add(portName);
                Console.WriteLine(portnames.Length);
                if (portnames[0] != null)
                {
                    CBComs.SelectedItem = portnames[0];
                }
            }
        }
        private void BReScan_Click(object sender, EventArgs e)
        {
            ComPortScan();
        }
        private void BConnect_Click(object sender, EventArgs e) // нажатие кнопки "подключить"
        {
            currentPort = new SerialPort(CBComs.Text, 9600);
            currentPort.DtrEnable = true;
            currentPort.Open();
            //serialPort1.Close();
            //serialPort1.PortName = CBComs.Text;
            //namePort = CBComs.Text;
            //serialPort1.Open();
            //timerEnable = true;
            labelSost.Text = "Подключен";
            timer = new System.Threading.Timer(TimerTick, null, 1000, 300); // создаем таймер
            BConnect.Enabled = false; // делаем кнопмку подключения неактивной
        }

        private void BDisconnect_Click(object sender, EventArgs e)
        {
            currentPort.Close();
            labelSost.Text = "Отключен";
            BConnect.Enabled = true; // делаем кнопмку подключения неактивной
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            // проверяем наше окно, и если оно было свернуто, делаем событие        
            if (WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false; // прячем наше окно из панели
                notifyIcon1.Visible = true; // делаем нашу иконку в трее активной
                minimized = true;
            }
        }
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e) // по двойному щелчку разворачиваем окно 
        {
            notifyIcon1.Visible = false;    // делаем нашу иконку скрытой
            this.ShowInTaskbar = true;  // возвращаем отображение окна в панели
            WindowState = FormWindowState.Normal;   //разворачиваем окно
            minimized = false;
        }

        void TimerTick(object sender)
        {
            if (minimized) this.Invoke(new Action(() => GetLayOut())); //вызов из свернутого состояния, для определения раскладки другого окна
            else this.Invoke(new Action(() => GetLayOutInForm()));// вызов, если активно окно нашей программы
            // используется Invoke из-за того, что форма и объекты из кода рождаются в разных процессах, Invoke нужен для доступа одногопроцесса в другой

        }
        void GetLayOutInForm()
        {
            InputLanguage myCurrentLanguage = InputLanguage.CurrentInputLanguage;
            if (myCurrentLanguage != null)
            {

                String txt = myCurrentLanguage.LayoutName;
                if (txt == "Русская")
                {
                    PortWrite("1");
                    lLangStatus.Text = "RU";
                }
                if (txt == "США")
                {
                    PortWrite("3");
                    lLangStatus.Text = "EN";
                }
            }
        }
        void GetLayOut() // метод определения раскладки активного окна 
        {
            IntPtr selectedWindow = KeyboardLayoutTools.NativeMethods.GetForegroundWindow();
            int currId;
            if (KeyboardLayoutTools.CheckKeyboardLayout(Handle, selectedWindow, out currId))
            {

                switch (currId)
                {
                    case 1033:  //английский
                        PortWrite("1");
                        break;
                    case 1049:  //русский
                        PortWrite("3");
                        break;
                }
            }
        }



        void PortWrite(String txt) //функция записи переданного параметра в порт
        {
            try
            {
                System.Threading.Thread.Sleep(100); // небольшая пауза, ведь SerialPort не терпит суеты
                currentPort.Write(txt);
            }
            catch
            {
                //if (!minimized) labelSost.Text = "Ошибка открытия порта";
            }

        }


    }
    public class KeyboardLayoutTools
    {

        private static readonly InputLanguageCollection InstalledInputLanguages = InputLanguage.InstalledInputLanguages;
        private static int _lastKeyLayout = -1;


        public static int GetKeyboardLayoutId(IntPtr windowHandle)
        {
            int lpdwProcessId;
            int winThreadProcId = NativeMethods.GetWindowThreadProcessId(windowHandle, out lpdwProcessId);
            IntPtr keybLayout = NativeMethods.GetKeyboardLayout(winThreadProcId);

            for (int i = 0; i < InstalledInputLanguages.Count; i++)
                if (keybLayout == InstalledInputLanguages[i].Handle)
                    return InstalledInputLanguages[i].Culture.KeyboardLayoutId;

            return -1;
        }

        public static bool CheckKeyboardLayout(IntPtr thisWindow, IntPtr selectedWindow, out int currentLayId)
        {
            currentLayId = _lastKeyLayout;
            if (selectedWindow != thisWindow)
            {
                int foregroundWindowInput = GetKeyboardLayoutId(selectedWindow);
                if (foregroundWindowInput == _lastKeyLayout) return false;

                int thisWindowInput = GetKeyboardLayoutId(thisWindow);
                currentLayId = _lastKeyLayout = thisWindowInput;

                if (thisWindowInput != foregroundWindowInput)
                {
                    NativeMethods.ActivateKeyboardLayout(new IntPtr(1), 8);
                    return true;
                }
            }
            return false;
        }

        #region Nested type: NativeMethods

        public static class NativeMethods
        {
            /// 
            /// Получает маркер активного окна Windows.
            /// 
            [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
            public static extern IntPtr GetForegroundWindow();

            /// 
            /// Получает идентификационный номер потока для окна.
            /// 
            [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
            public static extern int GetWindowThreadProcessId(IntPtr handleWindow, out int lpdwProcessId);

            /// 
            /// Получает информацию о раскладке клавиатуры.
            /// 
            [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
            public static extern IntPtr GetKeyboardLayout(int windowsThreadProcessId);

            /// 
            /// Активирует раскладку клавиатуры.
            /// 
            [DllImport("user32.dll")]
            public static extern int ActivateKeyboardLayout(IntPtr nkl, uint flags);
        }

        #endregion
    }
}
