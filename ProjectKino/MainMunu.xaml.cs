using Microsoft.EntityFrameworkCore;
using ProjectKino.Models;
using System;
using System.Linq;
using System.Windows;

namespace ProjectKino
{
   
    public partial class MainMunu : Window
    {
        KinoStudioContext db = new KinoStudioContext();

        public MainMunu(string sotrudnikName)
        {
            InitializeComponent();
            TBUserName.Text = sotrudnikName;

            CheckBudgetExceeded();
        }

        private void CheckBudgetExceeded()
        {
            try
            {
                var projects = db.Проектs
                    .Include(p => p.ЗатратыNavigation)
                    .ToList();

                var exceededProjects = projects.Where(p =>
                {
                    if (p.ЗатратыNavigation == null) return false;

                    decimal totalCosts = (p.ЗатратыNavigation.Зарплаты ?? 0) +
                                         (p.ЗатратыNavigation.Аренда ?? 0) +
                                         (p.ЗатратыNavigation.Оборудование ?? 0) +
                                         (p.ЗатратыNavigation.Постобработка ?? 0);

                    return totalCosts > p.Бюджет;
                }).ToList();

                if (exceededProjects.Count > 0)
                {
                    string message = "Внимание! В следующих проектах затраты превысили бюджет:\n\n";

                    foreach (var project in exceededProjects)
                    {
                        decimal totalCosts = (project.ЗатратыNavigation.Зарплаты ?? 0) +
                                             (project.ЗатратыNavigation.Аренда ?? 0) +
                                             (project.ЗатратыNavigation.Оборудование ?? 0) +
                                             (project.ЗатратыNavigation.Постобработка ?? 0);

                        message += $"• {project.Название} (Бюджет: {project.Бюджет:C}, Затраты: {totalCosts:C})\n";
                    }

                    MessageBox.Show(message, "Превышение бюджета", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch
            {
              
            }
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            var main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void ButtonSotrudniki_Click(object sender, RoutedEventArgs e)
        {
            var sotridnikPerehod = new SotrudnikWindow();
            sotridnikPerehod.Show();
            this.Close();
        }

        private void ButtonProject_Click(object sender, RoutedEventArgs e)
        {
            var WindowProject = new ProjectWindow();
            WindowProject.Show();
            this.Close();
        }

        private void ButtonPloshadka_Click(object sender, RoutedEventArgs e)
        {
            var WindowPloshadka = new PloshadkaWindow();
            WindowPloshadka.Show();
            this.Close();
        }

        private void ButtonGrafik_Click(object sender, RoutedEventArgs e)
        {
            var WindowGrafik = new GrafikWindow();
            WindowGrafik.Show();
            this.Close();
        }

        private void ButtonGruppa_Click(object sender, RoutedEventArgs e)
        {
            var WindowOperatorGroup = new OperatorGruppWindow();
            WindowOperatorGroup.Show();
            this.Close();
        }

        private void ButtonZatrat_Click(object sender, RoutedEventArgs e)
        {
            var WindowZatraty = new ZatratyWindow();
            WindowZatraty.Show();
            this.Close();
        }

        private void ButtonProkat_Click(object sender, RoutedEventArgs e)
        {
            var WidnowProkat = new ProkatWindow();
            WidnowProkat.Show();
            this.Close();
        }
    }
}