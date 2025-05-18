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
using WpfApp4.DataPages;

namespace WpfApp4.Pages
{
    /// <summary>
    /// Логика взаимодействия для MenuPage.xaml
    /// </summary>
    public partial class MenuPage : Page
    {
        public MenuPage()
        {
            InitializeComponent();
            MenuFrame.Content = new GraphPage();
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MenuFrame.Content = new GraphPage();
        }

        private void AutomatiLab_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MenuFrame.Content = new VendingDataPage();
        }

        private void CompanyLab_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MenuFrame.Content = new CompanyDataPage();
        }
    }
}
