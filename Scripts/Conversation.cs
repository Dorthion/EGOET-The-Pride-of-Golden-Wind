using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.IO;

namespace EGOET.Scripts.Conversations
{
    class Conversation
    {
        private Font font;
        private Text ActualTextToDisplay;
        private RectangleShape Prostokat;

        private int EndDialogAt;
        private int NumberOfDialog;
        private bool IfPressedButton = false;
        private string[] TextToDisplay;

        internal bool DisableConv = false;

        public Conversation(int startConv, int endConv)
        {
            var textToDisplay = File.ReadAllLines(@"D:\Programowanie\EGOET\EGOET-The-Pride-of-Golden-Wind\Scripts\Conversations\GuildGuy.txt");
            font = new Font(@"D:\Programowanie\EGOET\EGOET-The-Pride-of-Golden-Wind\Scripts\Conversations\Font.ttf");
            Prostokat = new RectangleShape()
            {
                Size = new Vector2f(1200, 200),
                FillColor = Color.Red,
                Position = new Vector2f(10000,10000)
            };
            TextToDisplay = new string[textToDisplay.Length];
            TextToDisplay = textToDisplay;
            NumberOfDialog = startConv-1;
            EndDialogAt = endConv;
            ActualTextToDisplay = new Text()
            {
                Font = font,
                DisplayedString = TextToDisplay[NumberOfDialog]
            };
        }

        public void Update(float posX, float posY)
        {
            Prostokat.Position = new Vector2f(posX - 600, posY + 200);
            ActualTextToDisplay.Position = new Vector2f(posX - 600, posY + 200);
            ActualTextToDisplay.DisplayedString = TextToDisplay[NumberOfDialog];
            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                if (EndDialogAt > NumberOfDialog && !IfPressedButton)
                {
                    NumberOfDialog++;
                    IfPressedButton = true;
                    if(EndDialogAt == NumberOfDialog)
                        DisableConv = true;
                } 
            }
            else
            {
                IfPressedButton = false;
            }
        }

        public void Draw(RenderWindow window)
        {
            window.Draw(drawable: Prostokat);
            window.Draw(drawable: ActualTextToDisplay);
        }
    }
}