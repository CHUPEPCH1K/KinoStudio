using ProjectKino.Models;
using System.Linq;
using System.Windows;

namespace ProjectKino
{
    public partial class AddGruppWindow : Window
    {
        KinoStudioContext db = new KinoStudioContext();

        public AddGruppWindow()
        {
            InitializeComponent();

            CBProject.ItemsSource = db.Проектs.ToList();
            CBEmployee.ItemsSource = db.Сотрудникs.ToList();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (CBProject.SelectedValue == null || CBEmployee.SelectedValue == null)
            {
                MessageBox.Show("Выберите проект и сотрудника!");
                return;
            }

            int projectId = (int)CBProject.SelectedValue;
            int employeeId = (int)CBEmployee.SelectedValue;

            var group = db.СъёмочнаяГруппаs.FirstOrDefault(g => g.ПроектГруппы == projectId);

            if (group == null)
            {
                group = new СъёмочнаяГруппа { ПроектГруппы = projectId };
                db.СъёмочнаяГруппаs.Add(group);
                db.SaveChanges();
            }

            var exists = db.СоставСъёмочнойГруппыs
                .Any(s => s.Сотрудник == employeeId && s.СёмочнаяГруппа == group.КодСъёмочнойГруппы);

            if (exists)
            {
                MessageBox.Show("Этот сотрудник уже добавлен в этот проект!");
                return;
            }

            var newSostav = new СоставСъёмочнойГруппы
            {
                Сотрудник = employeeId,
                СёмочнаяГруппа = group.КодСъёмочнойГруппы
            };

            db.СоставСъёмочнойГруппыs.Add(newSostav);
            db.SaveChanges();

            MessageBox.Show("Сотрудник успешно добавлен в проект!");
            this.Close();
        }
    }
}