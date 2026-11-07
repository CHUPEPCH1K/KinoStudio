using ProjectKino.Models;
using System;
using System.Linq;
using System.Windows;

namespace ProjectKino
{
    public partial class AddProkatWindow : Window
    {
        KinoStudioContext db = new KinoStudioContext();

        public AddProkatWindow()
        {
            InitializeComponent();
            CBProject.ItemsSource = db.Проектs.ToList();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (CBProject.SelectedValue == null || DPDate.SelectedDate == null ||
                string.IsNullOrWhiteSpace(TBCount.Text) || string.IsNullOrWhiteSpace(TBEarnings.Text) ||
                string.IsNullOrWhiteSpace(TBRating.Text))
            {
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(TBCount.Text, out int count) || count <= 0)
            {
                MessageBox.Show("Количество кинотеатров должно быть целым числом больше нуля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!decimal.TryParse(TBEarnings.Text, out decimal earnings) || earnings < 0)
            {
                MessageBox.Show("Сборы должны быть числом не меньше нуля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!decimal.TryParse(TBRating.Text, out decimal rating) || rating < 0 || rating > 10)
            {
                MessageBox.Show("Рейтинг должен быть числом от 0 до 10!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            
            var newProkat = new Прокат
            {
                Проект = (int)CBProject.SelectedValue,
                ДатаВыхода = DateOnly.FromDateTime(DPDate.SelectedDate.Value),
                КоличествоКинотеатров = count,
                Сборы = earnings,
                РейтингЗрителей = rating
            };

            db.Прокатs.Add(newProkat);
            db.SaveChanges();

            MessageBox.Show("Прокат успешно добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}