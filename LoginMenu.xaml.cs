using System;
using System.IO;
using System.Windows;
using MahApps.Metro;
using MahApps.Metro.Controls;
using Newtonsoft.Json;
using EGOET.Informations;
using System.Reflection;

namespace EGOET
{
    public partial class LoginMenu : MetroWindow
    {
        public LoginMenu()
        {
            InitializeComponent();
            ThemeManager.ChangeAppStyle(this, ThemeManager.GetAccent("Purple"), ThemeManager.GetAppTheme("BaseDark"));
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            PlayerClass Dane;
            try
            {
                Dane = JsonConvert.DeserializeObject<PlayerClass>(File.ReadAllText(@"..\..\Resources\Profiles\" + LoginBox.Text + ".json"));
                if (!Check_Settings())
                    return;
                if (Dane.Username == LoginBox.Text && Dane.Password == PasswordBox.Password)
                {
                    MainWindow Gra = new MainWindow(Dane);
                    this.Close();
                    Gra.Show();
                }
                else
                {
                    LoadDesc.Visibility = Visibility.Visible;
                    LoadDesc.Content = "Wprowadzono błędne dane!";
                }
            }
            catch (Exception)
            {
                LoadDesc.Visibility = Visibility.Visible;
                LoadDesc.Content = "Wprowadzono błędne dane!";
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            if(!File.Exists(@"..\..\Resources\Profiles\" + LoginBox.Text + ".json") && 
                LoginBox.Text != "" && 
                PasswordBox.Password != "")
            {
                try
                {
                    PlayerClass NewAcc = new PlayerClass
                    {
                        Username = LoginBox.Text,
                        Password = PasswordBox.Password
                    };

                    for (int i = 1; i <= 33; i++)
                        NewAcc.Items.Add(new Item() { IdInv = i });

                    NewAcc.Hero.Name = NewAcc.Username;

                    File.WriteAllText(@"..\..\Resources\Profiles\" + LoginBox.Text + ".json", JsonConvert.SerializeObject(NewAcc));
                    LoadDesc.Visibility = Visibility.Visible;
                    LoadDesc.Content = "Registered Account!";
                }
                catch (Exception w)
                {
                    LoadDesc.Content = w.Message;
                    return;
                }
            } else
            {
                LoadDesc.Visibility = Visibility.Visible;
                LoadDesc.Content = "Wprowadzono błędne dane!";
            }
        }

        private bool Check_Settings()
        {
            var dpiXProperty = typeof(SystemParameters).GetProperty("DpiX", BindingFlags.NonPublic | BindingFlags.Static);
            var dpiX = (int)dpiXProperty.GetValue(null, null);

            if (dpiX != 96 || SystemParameters.PrimaryScreenWidth != 1920 || SystemParameters.PrimaryScreenHeight != 1080)
            {
                MessageBox.Show("Bledne ustawienia monitora!");
                return false;
            }
            return true;
        }
    }
}