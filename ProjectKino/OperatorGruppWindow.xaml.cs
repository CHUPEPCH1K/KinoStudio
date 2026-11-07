using Microsoft.EntityFrameworkCore;
using ProjectKino.Models;
using System.Linq;
using System.Windows;

namespace ProjectKino
{
    public partial class OperatorGruppWindow : Window
    {
        KinoStudioContext db = new KinoStudioContext();

        public OperatorGruppWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            db.ChangeTracker.Clear();

            var sostav = db.СоставСъёмочнойГруппыs
                .Include(s => s.СотрудникNavigation)
                    .ThenInclude(emp => emp.СпециализацияNavigation) 
                .Include(s => s.СёмочнаяГруппаNavigation)
                    .ThenInclude(g => g.ПроектГруппыNavigation)
                .ToList();

            GruppList.ItemsSource = sostav;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var WindowAddGroup = new AddGruppWindow();
            WindowAddGroup.ShowDialog();
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