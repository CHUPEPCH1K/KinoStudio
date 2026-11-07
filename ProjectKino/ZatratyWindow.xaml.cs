using Microsoft.EntityFrameworkCore;
using ProjectKino.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ProjectKino
{
    public partial class ZatratyWindow : Window
    {
        KinoStudioContext db = new KinoStudioContext();

        public ZatratyWindow()
        {
            InitializeComponent();
            CBFilterProject.ItemsSource = db.Проектs.ToList();
            CBFilterType.SelectedIndex = 0; 
            LoadData();
        }

        private void LoadData()
        {
            db.ChangeTracker.Clear();
            var data = db.Проектs
                .Include(p => p.ЗатратыNavigation)
                .ToList();

            ZatratyList.ItemsSource = data;
        }

        private void ButtonFilter_Click(object sender, RoutedEventArgs e)
        {
            db.ChangeTracker.Clear();

            var query = db.Проектs.Include(p => p.ЗатратыNavigation).AsQueryable();

            if (CBFilterProject.SelectedValue != null)
            {
                int projId = (int)CBFilterProject.SelectedValue;
                query = query.Where(p => p.КодПроекта == projId);
            }

            if (CBFilterType.SelectedItem != null)
            {
                string type = ((ComboBoxItem)CBFilterType.SelectedItem).Content.ToString();

                if (type == "Зарплаты")
                    query = query.Where(p => p.ЗатратыNavigation != null && p.ЗатратыNavigation.Зарплаты > 0);
                else if (type == "Аренда")
                    query = query.Where(p => p.ЗатратыNavigation != null && p.ЗатратыNavigation.Аренда > 0);
                else if (type == "Оборудование")
                    query = query.Where(p => p.ЗатратыNavigation != null && p.ЗатратыNavigation.Оборудование > 0);
                else if (type == "Постобработка")
                    query = query.Where(p => p.ЗатратыNavigation != null && p.ЗатратыNavigation.Постобработка > 0);
            }

            ZatratyList.ItemsSource = query.ToList();
        }

        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            CBFilterProject.SelectedIndex = -1;
            CBFilterType.SelectedIndex = 0;
            LoadData();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var addWin = new AddZatratyWindow();
            addWin.ShowDialog();
            LoadData();
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            var mainmenuwindow = new MainMunu("");

            mainmenuwindow.Show();
            this.Close();
        }
    }
}