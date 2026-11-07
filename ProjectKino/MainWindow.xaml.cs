using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ProjectKino.Models;

namespace ProjectKino
{
   
    public partial class MainWindow : Window
    {
        KinoStudioContext db = new KinoStudioContext();


        public MainWindow()
        {
            InitializeComponent();

        }

        private void ButtonVhod_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TBLogin.Text)) {

                MessageBox.Show("Введите корректный логин", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(TBPassword.Text))
            {
                MessageBox.Show("Введите корректный пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            string login = TBLogin.Text;
            string password = TBPassword.Text;

            var user = db.КонтактныеДанныеs.FirstOrDefault(x => x.Логин == login && x.Пароль == password );

            if (user == null)
            {
                MessageBox.Show("Неверный логин или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var sotrudnik = db.Сотрудникs.FirstOrDefault(s => s.КонтактныеДанные == user.КодКонтактныхДанных);
            string sotrudnikName;

            if (sotrudnik != null)
            {
                sotrudnikName = $"{sotrudnik.Фамилия} {sotrudnik.Имя}";
            }
            else
            {
                sotrudnikName = login;
            }

            var menu = new MainMunu(sotrudnikName);
            menu.Show();
            this.Close();

        }

        
    }
}