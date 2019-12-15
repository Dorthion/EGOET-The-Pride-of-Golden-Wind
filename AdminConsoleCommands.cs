﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace EGOET
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
                    Consola.Text += "\nPozycja gracza: " + Kur.player.Xpos + "/" + Kur.player.Ypos;
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
