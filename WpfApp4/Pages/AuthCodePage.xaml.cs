using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Threading;

namespace WpfApp4.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthCodePage.xaml
    /// </summary>
    /// 
    public partial class AuthCodePage : Page
    {
        private string code;
        private int count;
        public AuthCodePage()
        {
            InitializeComponent();
            GeneretingCode();
        }

        private void GeneretingCode()
        {
            string symbols = "1234567890!@#%&qwertyuiopasdfghjklzxcvbnm";

            Random randon = new Random();

            for (int i = 0; i < 7; i++)
            {
                code += symbols[randon.Next(0, symbols.Length)];
            }

            string time = DateTime.Now.ToUniversalTime().ToString("u");

            string timecode = code + "-" + time;

            File.WriteAllText(System.IO.Path.GetTempPath() + "/logs/passcode.txt", timecode);
        }


        private void Ок_Click(object sender, RoutedEventArgs e)
        {
            if (count != 3)
            {
                if (code == PassCodeTb.Text)
                {
                    NavigationService.Navigate(new AuthPage());
                }
                else
                {
                    MessageBox.Show("Неверный код");
                    count += 1;

                    if (count == 3)
                    {
                        DispatcherTimer time = new DispatcherTimer();
                        time.Tick += Time_Tick;
                        time.Start();
                        time.Interval = TimeSpan.FromSeconds(20);
                        count = 0;
                    }
                }
            }
        }

        private void Time_Tick(object sender, EventArgs e)
        {
            if (CodeStack.IsEnabled == true)
            {
                CodeStack.IsEnabled = false;
            }
            else
            {
                CodeStack.IsEnabled = true;
            }
        }
    }
}
