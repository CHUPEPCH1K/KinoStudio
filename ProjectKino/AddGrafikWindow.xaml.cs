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
    /// Логика взаимодействия для AddGrafikWindow.xaml
    /// </summary>
    public partial class AddGrafikWindow : Window
    {
        KinoStudioContext db = new KinoStudioContext();
        public AddGrafikWindow()
        {
            InitializeComponent();
            CBProject.ItemsSource = db.Проектs.ToList();
            CBProject.DisplayMemberPath = "Название"; 
            CBProject.SelectedValuePath = "КодПроекта"; 

          
            CBPloshadka.ItemsSource = db.СъёмочнаяПлощадкаs.ToList();
            CBPloshadka.DisplayMemberPath = "Адрес"; 
            CBPloshadka.SelectedValuePath = "КодСъёмочнойПлощадки"; 

        }

        private void ButtonAddGrafik_Click(object sender, RoutedEventArgs e)
        {

            if (DateGrafik.SelectedDate == null)
            {
                MessageBox.Show("Выберите дату");
                return;
            }
            if (string.IsNullOrWhiteSpace(TBTime.Text))
            {
                MessageBox.Show("Введите время в формате ЧЧ:ММ (например, 14:30)");
                return;
            }
            if (CBProject.SelectedValue == null)
            {
                MessageBox.Show("Выберите проект");
                return;
            }
            if (CBPloshadka.SelectedValue == null)
            {
                MessageBox.Show("Выберите площадку");
                return;
            }
            if (string.IsNullOrWhiteSpace(TBScena.Text))
            {
                MessageBox.Show("Введите название сцены");
                return;
            }

            if (!TimeOnly.TryParse(TBTime.Text, out TimeOnly selectedTime))
            {
                MessageBox.Show("Неверный формат времени. Введите в формате ЧЧ:ММ (например, 09:30)");
                return;
            }

            int projectId = (int)CBProject.SelectedValue;
            int ploshadkaId = (int)CBPloshadka.SelectedValue;

            var group = db.СъёмочнаяГруппаs.FirstOrDefault(g => g.ПроектГруппы == projectId);

            if (group == null)
            {
                group = new СъёмочнаяГруппа { ПроектГруппы = projectId };
                db.СъёмочнаяГруппаs.Add(group);
                db.SaveChanges(); 
            }

            var ploshadkaProekta = db.СъёмочнаяПлощадкаПроектаs
                .FirstOrDefault(p => p.Проект == projectId && p.СъёмочнаяПлощадка == ploshadkaId);

            if (ploshadkaProekta == null)
            {
                ploshadkaProekta = new СъёмочнаяПлощадкаПроекта
                {
                    Проект = projectId,
                    СъёмочнаяПлощадка = ploshadkaId
                };
                db.СъёмочнаяПлощадкаПроектаs.Add(ploshadkaProekta);
                db.SaveChanges(); 
            }

            var newGrafik = new ГрафикСъёмок
            {
                Дата = DateOnly.FromDateTime(DateGrafik.SelectedDate.Value),
                Время = selectedTime,
                Площадка = ploshadkaProekta.КодСъёмочнойПлощадкиПроекта, 
                СъёмочнаяГруппа = group.КодСъёмочнойГруппы,              
                Сцена = TBScena.Text.Trim()
            };

            db.ГрафикСъёмокs.Add(newGrafik);
            db.SaveChanges();

            MessageBox.Show("Запись в график успешно добавлена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close(); 
        }

    }
    
}
