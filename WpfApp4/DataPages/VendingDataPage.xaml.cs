using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Diagnostics;
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
    /// Логика взаимодействия для VendingDataPage.xaml
    /// </summary>
    public partial class VendingDataPage : Page
    {
        private int PageCount = 1;
        private int PageZap;
        private int Count;
        public VendingDataPage()
        {
            InitializeComponent();
            LoadCb();
        }

        private void LoadData()
        {
            if(ZapCb.SelectedIndex == 4)
            {
                if (string.IsNullOrEmpty(FilterTb.Text))
                {
                    vendingDataGrid.ItemsSource = Db.Vending.ToList();
                    vendingListView.ItemsSource = Db.Vending.ToList();

                    Count = Db.Vending.Count();

                }
                else
                {
                    vendingDataGrid.ItemsSource = Db.Vending.Where(el => el.Name.Contains(FilterTb.Text)).ToList();
                    vendingListView.ItemsSource = Db.Vending.Where(el => el.Name.Contains(FilterTb.Text)).ToList();

                    Count = Db.Vending.Where(el => el.Name.Contains(FilterTb.Text)).Count();
                }
                CountLab.Content = $"Записи с {Count - (Count - 1)} до {Count} из {Count}";
            }
            else
            {
                if (string.IsNullOrEmpty(FilterTb.Text))
                {
                    vendingDataGrid.ItemsSource = Db.Vending.ToList().Skip(PageZap * (PageCount - 1)).Take(PageZap);
                    vendingListView.ItemsSource = Db.Vending.ToList().Skip(PageZap * (PageCount - 1)).Take(PageZap);

                    Count = Db.Vending.Count();

                }
                else
                {
                    vendingDataGrid.ItemsSource = Db.Vending.Where(el => el.Name.Contains(FilterTb.Text)).ToList().Skip(PageZap * (PageCount - 1)).Take(PageZap);
                    vendingListView.ItemsSource = Db.Vending.Where(el => el.Name.Contains(FilterTb.Text)).ToList().Skip(PageZap * (PageCount - 1)).Take(PageZap);

                    Count = Db.Vending.Where(el => el.Name.Contains(FilterTb.Text)).Count();
                }
                CountLab.Content = $"Записи с {((PageCount - 1) * PageZap) + 1} до {Math.Min(Count, PageZap * PageCount)} из {Count}";
            }

            PageCountTb.Text = PageCount.ToString();
        }

        private void LoadCb()
        {
            ZapCb.Items.Add(1);
            ZapCb.Items.Add(5);
            ZapCb.Items.Add(10);
            ZapCb.Items.Add(15);
            ZapCb.Items.Add("Все");

            ZapCb.SelectedIndex = 0;
        }

        private void ExpBtn_Click(object sender, RoutedEventArgs e)
        {
            List<NewVending> Items = new List<NewVending>();


            if(string.IsNullOrEmpty(FilterTb.Text))
            {
                foreach(var item in Db.Vending.ToList())
                {
                    var el = new NewVending();
                    el.Id = item.Id.ToString();
                    el.Name = item.Name;
                    el.ModelName = item.Model.Name;
                    el.CompanyName = item.Company.Name;
                    el.ModemName = item.Modem.Name.ToString();
                    el.AddressAndPlace = item.AddressAndPlace;
                    el.Date = item.DateInstall.ToString("d");

                    Items.Add(el);
                }
            }
            else
            {
                foreach (var item in Db.Vending.Where(el => el.Name.Contains(FilterTb.Text)).ToList())
                {
                    var el = new NewVending();
                    el.Id = item.Id.ToString();
                    el.Name = item.Name;
                    el.ModelName = item.Model.Name;
                    el.CompanyName = item.Company.Name;
                    el.ModemName = item.Modem.Name.ToString();
                    el.AddressAndPlace = item.AddressAndPlace;
                    el.Date = item.DateInstall.ToString("d");

                    Items.Add(el);
                }
            }

            var dialog = new SaveFileDialog();
            dialog.FileName = "Document";
            dialog.DefaultExt = ".json";
            dialog.Filter = "JSON Documents(.json) | *.json";

            var result = dialog.ShowDialog();

            if(result == true)
            {
                var filename = dialog.FileName;
                string stringItems = JsonConvert.SerializeObject(Items, Formatting.Indented);
                File.WriteAllText(filename, stringItems);
            }
        }

        private void ZapCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ZapCb.SelectedIndex != 4)
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

        private void ForwardBtn_Click(object sender, RoutedEventArgs e)
        {
            if((PageCount * PageZap) < Count)
            {
                PageCount++;
                LoadData();
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            if(PageCount > 1)
            {
                PageCount--;
                LoadData();
            }
        }

        private void TileImg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TableImg.Source = BitmapFrame.Create(new Uri(@"C:\Users\Ансар\source\repos\SessionRoss4\SessionRoss4\assets\free-icon-table-grid-25617.png"));
            TileImg.Source = BitmapFrame.Create(new Uri(@"C:\Users\Ансар\source\repos\SessionRoss4\SessionRoss4\assets\free-icon-tile-875161_gol.png"));

            vendingListView.Visibility = Visibility.Visible;
            vendingDataGrid.Visibility = Visibility.Collapsed;
        }

        private void TableImg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TableImg.Source = BitmapFrame.Create(new Uri(@"C:\Users\Ансар\source\repos\SessionRoss4\SessionRoss4\assets\free-icon-table-grid-25617_gol.png"));
            TileImg.Source = BitmapFrame.Create(new Uri(@"C:\Users\Ансар\source\repos\SessionRoss4\SessionRoss4\assets\free-icon-tile-875161.png"));

            vendingListView.Visibility = Visibility.Collapsed;
            vendingDataGrid.Visibility = Visibility.Visible;
        }
    }
    public class NewVending
    {
        public string Id;
        public string Name;
        public string ModelName;
        public string CompanyName;
        public string ModemName;
        public string AddressAndPlace;
        public string Date;
    }
}
