using ProjectKino.Models;
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

namespace ProjectKino
{
    /// <summary>
    /// Логика взаимодействия для AddPloshadkaWindow.xaml
    /// </summary>
    public partial class AddPloshadkaWindow : Window
    {
        KinoStudioContext db = new KinoStudioContext();
        
        public AddPloshadkaWindow()
        {
            InitializeComponent();
            CBType.ItemsSource = db.ТипПлощадкиs.ToList();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(TBAdres.Text))
            {
                MessageBox.Show("Адрес обязательное поле", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!int.TryParse(TBPrice.Text, out int price))
            {
                MessageBox.Show("Стоимость должна быть числом", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!int.TryParse(TBMesto.Text, out int mesto))
            {
                MessageBox.Show("Вместимость должна быть числом", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (price <= 0)
            {
                MessageBox.Show("Стоимость должна быть больше нуля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (mesto <= 0)
            {
                MessageBox.Show("Вместимость должна быть больше нуля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var ploshadka = new СъёмочнаяПлощадка {
                Адрес = TBAdres.Text.Trim(),
                ТипПлощадки = (int)CBType.SelectedValue,
                СтоимостьАрендыВДень = price,
                Вместимость = mesto




             };
            db.СъёмочнаяПлощадкаs.Add(ploshadka);
            db.SaveChanges();
            MessageBox.Show("съёмочная площадка  успешно создана!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();


        }
    }
}
