using System.Threading.Tasks;
using System.Windows.Controls;

namespace EGOET.AdminConsole
{
    public class AdminConsoleCommands
    {
        internal bool GetMoveDzialaj = false;

        public async void GetMove(int typ, TextBlock Consola, ScrollViewer ScrollAdmin, MainWindow Kur)
        {
            if (typ == 1)
            {
                while (true)
                {
                    Consola.Text += "\nPozycja gracza: " + Kur.gM.player.Xpos + "/" + Kur.gM.player.Ypos;
                    ScrollAdmin.ScrollToBottom();
                    await Task.Delay(100);
                    if(GetMoveDzialaj == true)
                    {
                        GetMoveDzialaj = false;
                        return;
                    }
                }
            }
            return;
        }

        public async void ZatrzymajWCzasie(int Time)
        {
            await Task.Delay(Time);
            GetMoveDzialaj = true;
            return;
        }
    }
}