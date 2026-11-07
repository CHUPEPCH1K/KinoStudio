using Microsoft.Win32;
using ProjectKino.Models;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ProjectKino
{
    public partial class ChangeSotrudnikWindow : Window
    {
        KinoStudioContext db = new KinoStudioContext();
        private Сотрудник _sotrudnik;

        public ChangeSotrudnikWindow(Сотрудник SotrudnikSelected)
        {
            InitializeComponent();
            CBSpecialization.ItemsSource = db.Специализацияs.ToList();
            _sotrudnik = SotrudnikSelected;
            FillFields();
        }

        private void FillFields()
        {
            var contact = db.КонтактныеДанныеs.FirstOrDefault(x => x.КодКонтактныхДанных == _sotrudnik.КонтактныеДанные);

            TBSurname.Text = _sotrudnik.Фамилия;
            TBName.Text = _sotrudnik.Имя;
            TBLogin.Text = contact.Логин;
            TBPassword.Text = contact.Пароль;
            TBPhone.Text = contact.Телефон;
            TBEmail.Text = contact.Почта;
            TBStavka.Text = _sotrudnik.Ставка.ToString();

            CBSpecialization.SelectedValue = _sotrudnik.Специализация;

            if (!string.IsNullOrEmpty(_sotrudnik.Фото) && File.Exists(_sotrudnik.Фото))
            {
                TBPhotoPath.Text = _sotrudnik.Фото;

                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(_sotrudnik.Фото, UriKind.Absolute);
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
                ImgPreview.Source = bitmap;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TBName.Text) ||
               string.IsNullOrWhiteSpace(TBSurname.Text) ||
               string.IsNullOrWhiteSpace(TBLogin.Text) ||
               string.IsNullOrWhiteSpace(TBPassword.Text) ||
               string.IsNullOrWhiteSpace(TBEmail.Text) ||
               string.IsNullOrWhiteSpace(TBPhone.Text) ||
               string.IsNullOrWhiteSpace(TBStavka.Text) ||
               CBSpecialization.SelectedValue == null)
            {
                MessageBox.Show("Заполните все поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string phone = TBPhone.Text.Trim();
            if (phone.Length != 11)
            {
                MessageBox.Show("Телефон должен содержать 11 цифр", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (phone[0] != '8')
            {
                MessageBox.Show("Телефон должен начинаться с 8", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!phone.All(char.IsDigit))
            {
                MessageBox.Show("Телефон должен состоять только из цифр", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!TBEmail.Text.Contains("@"))
            {
                MessageBox.Show("Почта должна содержать символ @ ", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!int.TryParse(TBStavka.Text, out int stavka) || stavka <= 0)
            {
                MessageBox.Show("Ставка должна быть числом больше нуля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var sotrudnikDb = db.Сотрудникs
               .First(s => s.ТабельныйНомер == _sotrudnik.ТабельныйНомер);

            var contactDb = db.КонтактныеДанныеs
                .First(c => c.КодКонтактныхДанных == sotrudnikDb.КонтактныеДанные);

            sotrudnikDb.Фамилия = TBSurname.Text.Trim();
            sotrudnikDb.Имя = TBName.Text.Trim();
            sotrudnikDb.Специализация = (int)CBSpecialization.SelectedValue;
            sotrudnikDb.Ставка = stavka;

            sotrudnikDb.Фото = string.IsNullOrWhiteSpace(TBPhotoPath.Text) ? null : TBPhotoPath.Text;

            contactDb.Телефон = phone;
            contactDb.Почта = TBEmail.Text.Trim();
            contactDb.Логин = TBLogin.Text.Trim();
            contactDb.Пароль = TBPassword.Text;

            db.SaveChanges();

            MessageBox.Show("Данные сотрудника изменены", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        private void ButtonLoadPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                string selectedFilePath = openFileDialog.FileName;

                string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string imagesDirectory = System.IO.Path.Combine(appDirectory, "Images");

                if (!System.IO.Directory.Exists(imagesDirectory))
                {
                    System.IO.Directory.CreateDirectory(imagesDirectory);
                }

                string fileName = System.IO.Path.GetFileName(selectedFilePath);
                string destinationPath = System.IO.Path.Combine(imagesDirectory, fileName);

               
                System.IO.File.Copy(selectedFilePath, destinationPath, true);

                string relativePath = System.IO.Path.Combine("Images", fileName);
                TBPhotoPath.Text = relativePath;

                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(destinationPath, UriKind.Absolute);
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
                ImgPreview.Source = bitmap;
            }
        }
    }
}