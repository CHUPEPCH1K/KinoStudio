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
  
    public partial class ProjectWindow : Window
    {
        KinoStudioContext db = new KinoStudioContext();
        public ProjectWindow()
        {
            InitializeComponent();
            CBFiltr.ItemsSource = db.Статусs.ToList();
            LoadProject();
        }
        private void LoadProject()
        {
            var projects = db.Проектs.Include(x => x.ЖанрNavigation).Include(x => x.СтатусNavigation).Include(x => x.ЦелеваяАудиторияNavigation).ToList();
            ListProject.ItemsSource = projects;
        }

        private void ButtonAddProject_Click(object sender, RoutedEventArgs e)
        {
            var WindowAddProjects = new AddProjectWindow();
            WindowAddProjects.ShowDialog();
            LoadProject();
        }

        private void ButtonFiltr_Click(object sender, RoutedEventArgs e)
        {
            db.ChangeTracker.Clear();
            var query = db.Проектs.Include(x => x.ЖанрNavigation).Include(x => x.СтатусNavigation).Include(x => x.ЦелеваяАудиторияNavigation).AsQueryable();
            if (CBFiltr.SelectedValue != null)
            {
                int SelectedStatus = (int)CBFiltr.SelectedValue;
                query = query.Where(x => x.Статус == SelectedStatus);

                ListProject.ItemsSource = query.ToList();


            }


        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            var mainmenuwindow = new MainMunu("");

            mainmenuwindow.Show();
            this.Close();
        }
    }
}
