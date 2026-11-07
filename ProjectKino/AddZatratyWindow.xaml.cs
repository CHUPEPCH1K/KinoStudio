using Microsoft.EntityFrameworkCore;
using ProjectKino.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ProjectKino
{
    public partial class AddZatratyWindow : Window
    {
        KinoStudioContext db = new KinoStudioContext();

        public AddZatratyWindow()
        {
            InitializeComponent();
            CBProject.ItemsSource = db.Проектs.ToList();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (CBProject.SelectedValue == null || CBType.SelectedItem == null ||
                string.IsNullOrWhiteSpace(TBSum.Text) || DPDate.SelectedDate == null)
            {
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!decimal.TryParse(TBSum.Text, out decimal sum) || sum <= 0)
            {
                MessageBox.Show("Сумма должна быть числом больше нуля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DateOnly selectedDate = DateOnly.FromDateTime(DPDate.SelectedDate.Value);
            if (selectedDate > DateOnly.FromDateTime(DateTime.Today))
            {
                MessageBox.Show("Дата затраты не может быть в будущем!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int projectId = (int)CBProject.SelectedValue;
            string typeName = ((ComboBoxItem)CBType.SelectedItem).Content.ToString();

            var project = db.Проектs
                .Include(p => p.ЗатратыNavigation)
                .FirstOrDefault(p => p.КодПроекта == projectId);

            if (project != null)
            {
                if (project.ЗатратыNavigation == null)
                {
                    project.ЗатратыNavigation = new Затраты { Дата = selectedDate };
                }

                if (typeName == "Зарплаты")
                    project.ЗатратыNavigation.Зарплаты = (project.ЗатратыNavigation.Зарплаты ?? 0) + sum;
                else if (typeName == "Аренда")
                    project.ЗатратыNavigation.Аренда = (project.ЗатратыNavigation.Аренда ?? 0) + sum;
                else if (typeName == "Оборудование")
                    project.ЗатратыNavigation.Оборудование = (project.ЗатратыNavigation.Оборудование ?? 0) + sum;
                else if (typeName == "Постобработка")
                    project.ЗатратыNavigation.Постобработка = (project.ЗатратыNavigation.Постобработка ?? 0) + sum;

                project.ЗатратыNavigation.Дата = selectedDate;

                db.SaveChanges();
            }

            MessageBox.Show("Затрата успешно добавлена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}