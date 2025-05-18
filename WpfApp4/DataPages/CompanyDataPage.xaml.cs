using Microsoft.Win32;
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
using static WpfApp4.Classes.Helper;

namespace WpfApp4.DataPages
{
    /// <summary>
    /// Логика взаимодействия для CompanyDataPage.xaml
    /// </summary>
    public partial class CompanyDataPage : Page
    {
        private int PageCount = 1;
        private int PageZap;
        private int Count;

        public CompanyDataPage()
        {
            InitializeComponent();
            LoadCb();
        }

        private void LoadCb()
        {
            ZapCb.Items.Add(1);
            ZapCb.Items.Add(5);
            ZapCb.Items.Add(10);
            ZapCb.Items.Add("Все");

            ZapCb.SelectedIndex = 0;
        }

        private void LoadData()
        {
            if(ZapCb.SelectedIndex == 3)
            {
                if (string.IsNullOrEmpty(FilterTb.Text))
                {
                    companyDataGrid.ItemsSource = Db.Company.ToList();

                    Count = Db.Company.Count();
                }
                else
                {
                    //LINQ не поддерживает поиск по стороннему классу, спросить у Караева!
                    companyDataGrid.ItemsSource = Db.Company.Where(el => el.CompanyFIO.Contains(FilterTb.Text)).ToList();

                    Count = Db.Company.Where(el => el.CompanyFIO.Contains(FilterTb.Text)).Count();
                }

                CountLab.Content = $"Записи с {Count - (Count - 1)} до {Count} из {Count}";
            }
            else
            {
                if (string.IsNullOrEmpty(FilterTb.Text))
                {
                    //LINQ не поддерживает поиск по стороннему классу, спросить у Караева!
                    companyDataGrid.ItemsSource = Db.Company.ToList().Skip(PageZap * (PageCount - 1)).Take(PageZap);

                    Count = Db.Company.Count();
                }
                else
                {
                    //LINQ не поддерживает поиск по стороннему классу, спросить у Караева!
                    companyDataGrid.ItemsSource = Db.Company.Where(el => el.CompanyFIO.Contains(FilterTb.Text)).ToList().Skip(PageZap * (PageCount - 1)).Take(PageZap);

                    Count = Db.Company.Where(el => el.CompanyFIO.Contains(FilterTb.Text)).Count();
                }

                CountLab.Content = $"Записи с {(PageZap * (PageCount - 1)) + 1} до {Math.Min(Count, PageCount * PageZap)} из {Count}";
            }
            PageCountTb.Text = PageCount.ToString();
        }
        

        private void ZapCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ZapCb.SelectedIndex != 3)
            {
                PageZap = (int)ZapCb.SelectedValue;
            }
            PageCount = 1;
            LoadData();
        }

        private void FilterTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            PageCount = 1;
            LoadData();
        }

        private void ExpBtn_Click(object sender, RoutedEventArgs e)
        {
            var data = "Название;Адрес;Сайт;Код;Ответсвтенное лицо;Логин;Телефон;Email;Статус" + "\n";


            if(string.IsNullOrEmpty(FilterTb.Text))
            {
                foreach (var el in Db.Company.ToList())
                {
                    var com = el.Company2;

                    if (com == null)
                    {
                        data += el.Name + ";" + "Отсутсвует" + ";" + el.Address + ";" + el.WebsiteAddress + ";" + el.CompanyFIO + ";" + el.CompanyLogin + ";" +
                            el.CompanyPhone + ";" + el.StatusCompany.Name + "\n";
                    }
                    else
                    {
                        data += el.Name + ";" + el.Company2.Name + ";" + el.Address + ";" + el.WebsiteAddress + ";" + el.CompanyFIO + ";" + el.CompanyLogin + ";" +
                            el.CompanyPhone + ";" + el.StatusCompany.Name + "\n";
                    }
                }
            }
            else
            {
                // Всё также косяк с LINQ
            }

            var dialog = new SaveFileDialog();

            dialog.FileName = "Document";
            dialog.DefaultExt = ".csv";
            dialog.Filter = "CSV Documents (.csv) | *.csv";

            var result = dialog.ShowDialog();

            if(result == true)
            {
                var filename = dialog.FileName;
                File.WriteAllText(filename, data);
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            if(PageCount > 1)
            {
                PageCount --;
                LoadData();
            }
        }

        private void ForwardBtn_Click(object sender, RoutedEventArgs e)
        {
            if(PageCount * PageZap < Count)
            {
                PageCount++;
                LoadData();
            }
        }
    }
}
