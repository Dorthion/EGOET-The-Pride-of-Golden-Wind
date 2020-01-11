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
        private string[] TextToDisplay;

        public Conversation(int startConv, int endConv)
        {
             Prostokat = new RectangleShape()
             {
                 Size = new Vector2f(1700, 200),
                 Position = new Vector2f(0, 650),
                 FillColor = Color.Red
             };
            font = new Font(@"D:\Programowanie\EGOET\EGOET-The-Pride-of-Golden-Wind\Scripts\Conversations\Font.ttf");
            var textToDisplay = File.ReadAllLines(@"D:\Programowanie\EGOET\EGOET-The-Pride-of-Golden-Wind\Scripts\Conversations\GuildGuy.txt");
            TextToDisplay = new string[textToDisplay.Length];
            TextToDisplay = textToDisplay;
            EndDialogAt = endConv;
            ActualTextToDisplay = new Text()
            {
                Font = font,
                DisplayedString = TextToDisplay[startConv]
            };
        }

        public void Update()
        {
            //if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
            //{
            //    if(EndDialogAt == NumberOfDialog)
            //    NumberOfDialog++;
            //    ActualTextToDisplay.DisplayedString = TextToDisplay[NumberOfDialog];
            //}
        }

        public void Draw(RenderWindow window)
        {
            window.Draw(drawable: Prostokat);
            window.Draw(drawable: ActualTextToDisplay);
        }
    }
}
