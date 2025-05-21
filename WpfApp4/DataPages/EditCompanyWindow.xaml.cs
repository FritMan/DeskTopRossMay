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
using System.Windows.Shapes;
using WpfApp4.Data;
using static WpfApp4.Classes.Helper;

namespace WpfApp4.DataPages
{
    /// <summary>
    /// Логика взаимодействия для EditCompanyWindow.xaml
    /// </summary>
    /// 
    public partial class EditCompanyWindow : Window
    {
        private int _Id;
        private Company com = new Company();
        public EditCompanyWindow(int Id)
        {
            InitializeComponent();
            _Id = Id;

            if (_Id == -1)
            {

                com.CompanyId = 1;
                com.DateCreate = DateTime.Now;
                com.Company2 = Db.Company.First(el => el.Id == com.CompanyId);
                com.Contacts = "+ 7 923 345 34 34";

                UserComboBox.ItemsSource = Db.User.ToList();
                UserComboBox.SelectedIndex = 0;


                grid1.DataContext = com;
            }
            else
            {
                ReRegCode.Visibility = Visibility.Collapsed;

                com = Db.Company.First(el => el.Id == _Id);
                grid1.DataContext = com;
                grid1.IsEnabled = false;
                EdiUserLab.Visibility = Visibility.Visible;
                UserComboBox.Visibility = Visibility.Collapsed;
                OkBtn.Visibility = Visibility.Collapsed;
                EditComStack.Visibility = Visibility.Visible;
            }
        }

        private void Regen()
        {
            string symbols = "1234567890";
            string code = "";

            Random random = new Random();

            for (int i = 0; i < 9; i++)
            {
                code += random.Next(0, symbols.Length);
            }

            codeTextBox.Text = code;
            com.Code = code;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_Id == -1)
            {
                Regen();
            }
        }

        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
                if(_Id == -1)
                {
                    com.StatusId = 2;
                    Db.Company.Add(com);
                }
                Db.SaveChanges();
                Close();
        }

        private void ReRegCode_Click(object sender, RoutedEventArgs e)
        {
            Regen();
        }

        private void UserComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var sel_user = UserComboBox.SelectedItem as User;
            if (sel_user != null)
            {
                com.UserId = sel_user.Id;
            }
        }

        private void ActiveBtn_Click(object sender, RoutedEventArgs e)
        {
            if(com.StatusId == 2)
            {
                com.StatusId = 3;
                Db.SaveChanges();
                Close();
            }
        }

        private void BlockBtn_Click(object sender, RoutedEventArgs e)
        {
            com.StatusId = 4;
            Db.SaveChanges();
            Close();
        }
    }
}
