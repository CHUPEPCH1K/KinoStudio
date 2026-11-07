using Microsoft.Win32;
using ProjectKino.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ProjectKino
{
    public partial class AddSotrudnikWindow : Window
    {
        KinoStudioContext db = new KinoStudioContext();

        public AddSotrudnikWindow()
        {
            InitializeComponent();

            CBSpecialization.ItemsSource = db.Специализацияs.ToList();
            CBSpecialization.SelectedIndex = 0;
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
                MessageBox.Show("Заполните все обязательные поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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

            var contact = new КонтактныеДанные
            {
                Телефон = phone,
                Почта = TBEmail.Text,
                Логин = TBLogin.Text,
                Пароль = TBPassword.Text
            };

            db.КонтактныеДанныеs.Add(contact);
            db.SaveChanges();

            var sotrudnik = new Сотрудник
            {
                Фамилия = TBSurname.Text,
                Имя = TBName.Text,
                Специализация = (int)CBSpecialization.SelectedValue,
                Ставка = stavka,
                КонтактныеДанные = contact.КодКонтактныхДанных,

                Фото = string.IsNullOrWhiteSpace(TBPhotoPath.Text) ? null : TBPhotoPath.Text
            };

            db.Сотрудникs.Add(sotrudnik);
            db.SaveChanges();

            MessageBox.Show("Сотрудник добавлен");
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