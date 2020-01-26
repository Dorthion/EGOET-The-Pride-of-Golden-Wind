using MahApps.Metro.Controls;
using System.Windows;
using EGOET.Informations;

namespace EGOET.Stats
{
    public partial class StatsWindow : MetroWindow
    {
        private MainWindow mainWindow;
        public StatsWindow(PlayerClass playerClass, MainWindow window)
        {
            InitializeComponent();
            mainWindow = window;
            NameHero.Content = playerClass.Hero.Name;
            Level.Content = playerClass.Hero.Poziom;
            Exp.Content = playerClass.Hero.ExpNow + "/" + playerClass.Hero.ExpToNextLvl;
            Hp.Content = playerClass.Hero.Hp + "/"+ playerClass.Hero.HpMax;
            Strengh.Content = playerClass.Hero.Sila;
            Magic.Content = playerClass.Hero.Magia;
            Deff.Content = playerClass.Hero.Obrona;
            Points.Content = playerClass.Hero.PunktyUmiejetnosci;
            Cash.Content = playerClass.Hero.Money;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.UpdateRenderScreenSettings();
            this.Close();
        }
    }
}
