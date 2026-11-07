using Microsoft.EntityFrameworkCore;
using ProjectKino.Models;
using System;
using System.Linq;
using System.Windows;

namespace ProjectKino
{
    public partial class GrafikWindow : Window
    {
        KinoStudioContext db = new KinoStudioContext();

        public GrafikWindow()
        {
            InitializeComponent();

            CBProjectFilter.ItemsSource = db.Проектs.ToList();

            LoadGrafik();
        }

        private void LoadGrafik()
        {
            db.ChangeTracker.Clear();

            var grafik = db.ГрафикСъёмокs
                .Include(x => x.ПлощадкаNavigation)
                    .ThenInclude(p => p.СъёмочнаяПлощадкаNavigation)
                .Include(x => x.СъёмочнаяГруппаNavigation)
                    .ThenInclude(g => g.ПроектГруппыNavigation)
                .ToList();

            ListGrafik.ItemsSource = grafik;
        }

        private void ButtonFilter_Click(object sender, RoutedEventArgs e)
        {
            db.ChangeTracker.Clear();

            var query = db.ГрафикСъёмокs
                .Include(x => x.ПлощадкаNavigation)
                    .ThenInclude(p => p.СъёмочнаяПлощадкаNavigation)
                .Include(x => x.СъёмочнаяГруппаNavigation)
                    .ThenInclude(g => g.ПроектГруппыNavigation)
                .AsQueryable();

            if (DateFilter.SelectedDate != null)
            {
                DateOnly filterDate = DateOnly.FromDateTime(DateFilter.SelectedDate.Value);
                query = query.Where(x => x.Дата == filterDate);
            }

            if (CBProjectFilter.SelectedValue != null)
            {
                int projectId = (int)CBProjectFilter.SelectedValue;
                query = query.Where(x => x.СъёмочнаяГруппаNavigation.ПроектГруппы == projectId);
            }

            ListGrafik.ItemsSource = query.ToList();
        }

        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            DateFilter.SelectedDate = null;
            CBProjectFilter.SelectedIndex = -1;

            LoadGrafik();
        }

        private void ButtonAddGrafik_Click(object sender, RoutedEventArgs e)
        {
            var WindowGrafikAdd = new AddGrafikWindow();
            WindowGrafikAdd.ShowDialog();

            LoadGrafik();
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            var mainmenuwindow = new MainMunu("");
           
            mainmenuwindow.Show();
            this.Close();

        }
    }
}