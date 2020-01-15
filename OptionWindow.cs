using MahApps.Metro.Controls;
using System.Windows;

namespace EGOET.Options
{
    public partial class OptionWindow : MetroWindow
    {
        private bool CzyZmienionoOpcje = false;
        private MainWindow mw;

        public OptionWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            mw = mainWindow;
        }

        private void OptDialog_Loaded(object sender, RoutedEventArgs e)
        {
            SliderRenderDistance.Value = Properties.Settings.Default.RenderDistance;

            if (Properties.Settings.Default.DynamicCamera)
                DynamicCamera.IsChecked = true;
            else StaticCamera.IsChecked = true;

        }

        private void SliderRenderDistance_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            CzyZmienionoOpcje = true;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            if(CzyZmienionoOpcje == true)
            {
                if (MessageBox.Show("Zapisać aktualne ustawienia?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.No)
                {
                    Properties.Settings.Default["RenderDistance"] = (int)SliderRenderDistance.Value;

                    if (DynamicCamera.IsChecked == true)
                        Properties.Settings.Default.DynamicCamera = true;
                    else   Properties.Settings.Default["DynamicCamera"] = false;

                    Properties.Settings.Default.Save();

                    mw.UpdateRenderScreenSettings();
                }
            }
            this.Close();
        }
    }
}