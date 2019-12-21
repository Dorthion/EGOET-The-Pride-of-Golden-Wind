using MahApps.Metro.Controls;
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

namespace EGOET.Options
{
    /// <summary>
    /// Interaction logic for OptionWindow.xaml
    /// </summary>
    public partial class OptionWindow : MetroWindow
    {
        private bool CzyZmienionoOpcje = false;
        private int RenderDistanceOldValu;
        public OptionWindow()
        {
            InitializeComponent();
        }

        private void OptDialog_Loaded(object sender, RoutedEventArgs e)
        {
            SliderRenderDistance.Value = Properties.Settings.Default.RenderDistance;
            RenderDistanceOldValu = Properties.Settings.Default.RenderDistance;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            if(CzyZmienionoOpcje == true)
            {
                if (MessageBox.Show("Zapisać aktualne ustawienia?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    Properties.Settings.Default["RenderDistance"] = SliderRenderDistance.Value;
                    
                }
                else
                {
                    //do yes stuff
                }
               

            }
        }
    }
}
