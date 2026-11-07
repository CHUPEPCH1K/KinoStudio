using ProjectKino.Models;
using System;
using System.Linq;
using System.Windows;

namespace ProjectKino
{
    
    public partial class AddProjectWindow : Window
    {
    
        KinoStudioContext db = new KinoStudioContext();


        public AddProjectWindow()
        {
            InitializeComponent();

            CBGenre.ItemsSource = db.Жанрs.ToList();
            CBSatus.ItemsSource = db.Статусs.ToList();
            CBAuditoriya.ItemsSource = db.ЦелеваяАудиторияs.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TBName.Text))
            {
                MessageBox.Show("Введите название проекта", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(TBBank.Text))
            {
                MessageBox.Show("Введите бюджет", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!decimal.TryParse(TBBank.Text, out decimal budget))
            {
                MessageBox.Show("Бюджет должен быть числом", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (budget <= 0) 
            {
                MessageBox.Show("Бюджет должен быть больше нуля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (DataStartBlock.SelectedDate == null)
            {
                MessageBox.Show("Выберите дату начала съёмок", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (DataFinishBlock.SelectedDate == null)
            {
                MessageBox.Show("Выберите дату окончания съёмок", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (DataFinishBlock.SelectedDate < DataStartBlock.SelectedDate)
            {
                MessageBox.Show("Дата окончания съёмок не может быть раньше даты начала", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (CBGenre.SelectedValue == null ||
                CBSatus.SelectedValue == null ||
                CBAuditoriya.SelectedValue == null)
            {
                MessageBox.Show("Выберите жанр, статус и целевую аудиторию", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var newProject = new Проект
            {
                Название = TBName.Text.Trim(),
                Жанр = (int)CBGenre.SelectedValue,
                Бюджет = budget,
                ДатаНачалаСъёмок = DateOnly.FromDateTime(DataStartBlock.SelectedDate.Value),
                ДатаОкончанияСъёмок = DateOnly.FromDateTime(DataFinishBlock.SelectedDate.Value),
                Статус = (int)CBSatus.SelectedValue,
                ЦелеваяАудитория = (int)CBAuditoriya.SelectedValue,

                Площадка = null,
                ГрафикСъёмок = null,
                Затраты = null
            };

            db.Проектs.Add(newProject);
            db.SaveChanges();

            MessageBox.Show("Проект успешно добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}