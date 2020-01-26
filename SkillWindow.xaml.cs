using MahApps.Metro.Controls;
using System.Windows;
using EGOET.Informations;
using System;

namespace EGOET.Skills
{
    public partial class SkillWindow : MetroWindow
    {
        private MainWindow mainWindow;
        public SkillWindow(PlayerClass playerClass, MainWindow window)
        {
            InitializeComponent();
            mainWindow = window;
            Pkt.Content = playerClass.Hero.PunktyUmiejetnosci;
            Strength.Content = playerClass.Hero.Sila;
            Magic.Content = playerClass.Hero.Magia;
            Deff.Content = playerClass.Hero.Obrona;
        }

        private void AddToStrength_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt32(Pkt.Content) > 0)
            {
                Strength.Content = (Convert.ToInt32(Strength.Content) + 1).ToString();
                Pkt.Content = (Convert.ToInt32(Pkt.Content) - 1).ToString();
            }
        }

        private void MinusToStrength_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt32(Strength.Content) > 0)
            {
                Strength.Content = (Convert.ToInt32(Strength.Content) - 1).ToString();
                Pkt.Content = (Convert.ToInt32(Pkt.Content) + 1).ToString();
            }
        }

        private void AddToMagic_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt32(Pkt.Content) > 0)
            {
                Magic.Content = (Convert.ToInt32(Magic.Content) + 1).ToString();
                Pkt.Content = (Convert.ToInt32(Pkt.Content) - 1).ToString();
            }
        }

        private void MinusToMagic_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt32(Magic.Content) > 0)
            {
                Magic.Content = (Convert.ToInt32(Magic.Content) - 1).ToString();
                Pkt.Content = (Convert.ToInt32(Pkt.Content) + 1).ToString();
            }
        }

        private void AddToDeff_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt32(Pkt.Content) > 0)
            {
                Deff.Content = (Convert.ToInt32(Deff.Content) + 1).ToString();
                Pkt.Content = (Convert.ToInt32(Pkt.Content) - 1).ToString();
            }
        }

        private void MinusToDeff_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt32(Deff.Content) > 0)
            {
                Deff.Content = (Convert.ToInt32(Deff.Content) - 1).ToString();
                Pkt.Content = (Convert.ToInt32(Pkt.Content) + 1).ToString();
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.UpdateRenderScreenSettings();
            this.Close();
        }
    }
}
