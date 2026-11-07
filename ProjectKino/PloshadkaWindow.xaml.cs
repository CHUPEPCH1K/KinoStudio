using Microsoft.EntityFrameworkCore;
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
    
    public partial class PloshadkaWindow : Window
    {
        KinoStudioContext db = new KinoStudioContext();
        public PloshadkaWindow()
        {
            InitializeComponent();
            LoadPloshadka();
        }
        private void LoadPloshadka()
        { 
            var ploshadka = db.СъёмочнаяПлощадкаs.Include(x => x.ТипПлощадкиNavigation).ToList();
            ListPloshadka.ItemsSource = ploshadka;
        }

        private void ButtonAddPloshadka_Click(object sender, RoutedEventArgs e)
        {
           var WindowPloshadkaadd = new AddPloshadkaWindow();
            WindowPloshadkaadd.ShowDialog();
            LoadPloshadka();
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            var mainmenuwindow = new MainMunu("");

            mainmenuwindow.Show();
            this.Close();
        }
    }
}
