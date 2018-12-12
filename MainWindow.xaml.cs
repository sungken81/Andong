using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO.Ports;

namespace serialCommunication
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private SerialPort serial;
        public MainWindow()
        {
            InitializeComponent();
            this.serial = new SerialPort(portName: $"COM9", baudRate: 9600, parity: Parity.None, dataBits: 8, stopBits: StopBits.One);
            this.serial.Encoding = Encoding.UTF8;
            var f = new Random();
            
            
        }


        private void Click_On_Open(object sender, RoutedEventArgs e)
        {
            this.serial.Open();
            if (this.serial.IsOpen)
            {
                MessageBox.Show(messageBoxText: $"시리얼 통신이 연결 되었습니다.");
                this.serial.DataReceived += Serial_DataReceived;
                this.Open.IsEnabled = false;
                this.Close.IsEnabled = true;

            }
            else
            {
                MessageBox.Show(messageBoxText: $"시리얼 통신이 연결 되지 않았습니다.");
            }

        }

        private void Serial_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //throw new NotImplementedException();
            var string_Builder = new StringBuilder();
            string_Builder.Append(value: $"{serial.ReadExisting()}");
            this.Dispatcher.Invoke(callback: () =>
            {
                this.ListView_Window.Items.Add(string_Builder.ToString());

            });
        }


        private void Click_On_close(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(messageBoxText: $"연결을 종료합니다.");
            this.serial.Close();
            this.Open.IsEnabled = true;
            this.Close.IsEnabled = false;
        }




    }
}
