using Microsoft.Win32;
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

namespace santa_luchia
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        sssrEntities context;
        string currentLetter = "";
        public MainWindow()
        {
            InitializeComponent();
            context = new sssrEntities();
            Tovar.ItemsSource = context.Товар.ToList();
            

        }

        private void dob_Click(object sender, RoutedEventArgs e)
        {
            var Dob = new Товар();
            context.Товар.Add(Dob);
            OpenFileDialog das = new OpenFileDialog();
            das.Title = "Выберите фото к товару";
            das.Filter = "All supported graphics|*.jpeg;*.jpg;*.png|" + " JPEG(*.jpeg;*.jpg)|*.jpeg;*.jpg|" +
            "Portable Network Graphic (*.png)|*.png";
            if (das.ShowDialog() == true)
            {
                Dob.Фотодж = new Фотодж { Фото = das.FileName };
            }
            var Dob1 = new Redact(context, Dob);
            Dob1.ShowDialog();
        }

        private void red_Click(object sender, RoutedEventArgs e)
        {
            Button red1 = sender as Button;
            var red2 = red1.DataContext as Товар;
            OpenFileDialog das = new OpenFileDialog();
            das.Title = "Выберите фото к товару";
            das.Filter = "All supported graphics|*.jpeg;*.jpg;*.png|" + " JPEG(*.jpeg;*.jpg)|*.jpeg;*.jpg|" +
            "Portable Network Graphic (*.png)|*.png";
            if (das.ShowDialog() == true)
            {
                red2.Фотодж = new Фотодж { Фото = das.FileName };
            }
            var red3 = new Redact(context, red2);
            red3.ShowDialog();
        }

       

        private void fil1_Click(object sender, RoutedEventArgs e)
        {
            


        }

        private void fil_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (fil.SelectedIndex == 0)
            {
                ShowTable();
            }
            if (fil.SelectedIndex == 1)
            {
                Tovar.ItemsSource = context.Товар.Where(x => x.Тип.Тип_товара.Contains("Сок")).ToList();
            }
            if (fil.SelectedIndex == 2)
            {
                Tovar.ItemsSource = context.Товар.Where(x => x.Тип.Тип_товара.Contains("Заморозка")).ToList();

            }
            if (fil.SelectedIndex == 3)
            {
                Tovar.ItemsSource = context.Товар.Where(x => x.Тип.Тип_товара.Contains("Чай/кофе")).ToList();

            }
            if (fil.SelectedIndex == 4)
            {
                Tovar.ItemsSource = context.Товар.Where(x => x.Тип.Тип_товара.Contains("Кассовые")).ToList();

            }
            if (fil.SelectedIndex == 5)
            {
                Tovar.ItemsSource = context.Товар.Where(x => x.Тип.Тип_товара.Contains("Фитнес питание")).ToList();

            }

        }

        private void ShowTable()
        {
            context = new sssrEntities();
            Tovar.ItemsSource = context.Товар.ToList();
        }

        private void del_Click(object sender, RoutedEventArgs e)
        {
            var dea = Tovar.SelectedItem as Товар;
            if (dea == null)
            {
                MessageBox.Show("Выберите строку");
                return;
            }
            MessageBoxResult messageBoxResult = MessageBox.Show("Вы действительно ходите удалить эту строку?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                context.Товар.Remove(dea);
                context.SaveChanges();

            }
        }

        private void ShowTable1()
        {
            if (p.Text == null)
                return;
            List<Товар> azx = context.Товар.ToList();
            azx = azx.Where(x => x.Поставщики.Поставщик.ToLower().Contains(p.Text.ToLower())).ToList();
            if (currentLetter.Count() == 1)
            {
                azx = azx.Where(x => x.Наименование.Contains(currentLetter)).ToList();
            }
            Tovar.ItemsSource = azx.OrderBy(x => x.Наименование).ToList();
        }

        private void p_TextChanged(object sender, TextChangedEventArgs e)
        {
            ShowTable1();
        }
    }
}
