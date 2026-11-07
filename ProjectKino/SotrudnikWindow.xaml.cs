using Microsoft.EntityFrameworkCore;
using ProjectKino.Models;
using System.Linq;
using System.Windows;

namespace ProjectKino
{
    public partial class SotrudnikWindow : Window
    {
        KinoStudioContext db = new KinoStudioContext();

        public SotrudnikWindow()
        {
            InitializeComponent();
            LoadSotrudnik();
        }

        private void LoadSotrudnik()
        {
            db.ChangeTracker.Clear();

            var polzovatels = db.Сотрудникs
                .Include(x => x.СпециализацияNavigation)
                .Include(x => x.КонтактныеДанныеNavigation)
                .ToList();

            ListPolzovatels.ItemsSource = polzovatels;
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            var SotrudnikWindowAdd = new AddSotrudnikWindow();
            SotrudnikWindowAdd.ShowDialog();
            LoadSotrudnik();
        }

        private void ButtonChange_Click(object sender, RoutedEventArgs e)
        {
            var SelectedSotrudnik = ListPolzovatels.SelectedItem as Сотрудник;
            if (SelectedSotrudnik == null)
            {
                MessageBox.Show("Выберите сотрудника для редактирования", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var SotrudnikWindowChange = new ChangeSotrudnikWindow(SelectedSotrudnik);
            SotrudnikWindowChange.ShowDialog();
            LoadSotrudnik();
        }

        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            string search = TBSearch.Text.Trim();
            db.ChangeTracker.Clear();

            var query = db.Сотрудникs
                .Include(x => x.СпециализацияNavigation)
                .Include(x => x.КонтактныеДанныеNavigation)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.Фамилия.ToLower().StartsWith(search.ToLower()));
            }

            ListPolzovatels.ItemsSource = query.ToList();
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            var SelectedSotrudnik = ListPolzovatels.SelectedItem as Сотрудник;
            if (SelectedSotrudnik == null)
            {
                MessageBox.Show("Выберите сотрудника для удаления", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var result = MessageBox.Show($"Удалить сотрудника {SelectedSotrudnik.Фамилия} {SelectedSotrudnik.Имя} ?",
                                         "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes)
                return;

            db.ChangeTracker.Clear();

            var sotrudnikDB = db.Сотрудникs.FirstOrDefault(x => x.ТабельныйНомер == SelectedSotrudnik.ТабельныйНомер);
            if (sotrudnikDB == null)
            {
                MessageBox.Show("Сотрудник уже удален", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                LoadSotrudnik();
                return;
            }

            var contactDB = db.КонтактныеДанныеs.FirstOrDefault(x => x.КодКонтактныхДанных == sotrudnikDB.КонтактныеДанные);

            db.Сотрудникs.Remove(sotrudnikDB);

            if (contactDB != null)
            {
                db.КонтактныеДанныеs.Remove(contactDB);
            }

            db.SaveChanges();
            LoadSotrudnik();
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            var mainmenuwindow = new MainMunu("");

            mainmenuwindow.Show();
            this.Close();
        }
    }
}