using Microsoft.EntityFrameworkCore;
using ProjectKino.Models;
using System.Linq;
using System.Windows;

namespace ProjectKino
{
    public partial class ProkatWindow : Window
    {
        KinoStudioContext db = new KinoStudioContext();

        public ProkatWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            db.ChangeTracker.Clear();

            var data = db.Прокатs
                .Include(p => p.ПроектNavigation)
                .ToList();

            ProkatList.ItemsSource = data;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var addWin = new AddProkatWindow();
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