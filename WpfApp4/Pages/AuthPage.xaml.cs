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
using System.Windows.Threading;
using static WpfApp4.Classes.Helper;

namespace WpfApp4.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        private int count;
        public AuthPage()
        {
            InitializeComponent();
        }

        private void AuthBtn_Click(object sender, RoutedEventArgs e)
        {
            var user = Db.User.FirstOrDefault(el => el.Login == LoginTb.Text && el.Password == PasswordPb.Password);

            if (user != null)
            {
                UserExp.DataContext = user;
                UserExp.Visibility = Visibility.Visible;
                NavigationService.Navigate(new MenuPage());
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль");
                count += 1;

                if (count == 3)
                {
                    DispatcherTimer time = new DispatcherTimer();
                    time.Tick += Timer_Tick;
                    time.Start();
                    time.Interval = TimeSpan.FromSeconds(20);
                    count = 0;
                }
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if(AuthStack.IsEnabled == true)
            {
                AuthStack.IsEnabled = false;
            }
            else
            {
                AuthStack.IsEnabled = true;
            }
        }
    }
}
