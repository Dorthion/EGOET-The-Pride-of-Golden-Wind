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
        private RectangleShape ConvRectangle;

        private int EndDialogAt;
        private int NumberOfDialog;
        private bool IfPressedButton = false;
        private string[] TextToDisplay;

        internal bool DisableConv = false;

        public Conversation(int startConv, int endConv)
        {
            var textToDisplay = File.ReadAllLines(@"D:\Programowanie\EGOET\EGOET-The-Pride-of-Golden-Wind\Scripts\Conversations\GuildGuy.txt");
            Texture texture = new Texture(@"..\..\Resources\Fight.png");
            font = new Font(@"..\..\Resources\Fonts\Font.ttf");
            ConvRectangle = new RectangleShape()
            {
                Texture = texture,
                Size = new Vector2f(1200, 200),
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
            ConvRectangle.Position = new Vector2f(posX - 600, posY + 200);
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
            window.Draw(drawable: ConvRectangle);
            window.Draw(drawable: ActualTextToDisplay);
        }
    }
}