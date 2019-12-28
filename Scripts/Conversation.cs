using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.IO;

namespace EGOET.Scripts.Conversations
{
    class Conversation
    {
        private RectangleShape Prostokat;
        private int NumberOfDialog;
        private int EndDialogAt;
        private Font font;
        private Text ActualTextToDisplay;
        private string[] TextToDisplay;
        //private string ActualText;
        public void LoadConversation(RenderWindow window, int startConv, int endConv/*int idNPC*/)
        {
            int i = 0;
            StreamReader reader = new StreamReader(@"D:\Programowanie\EGOET\EGOET-The-Pride-of-Golden-Wind\Scripts\Conversations\GuildGuy.txt");
             Prostokat = new RectangleShape()
             {
                 Size = new Vector2f(1700, 200),
                 Position = new Vector2f(0, 650),
                 FillColor = Color.Red
             };
            font = new Font(@"D:\Programowanie\EGOET\EGOET-The-Pride-of-Golden-Wind\Scripts\Conversations\Font.ttf");
            while (reader.Peek() >= 0)
            {
                TextToDisplay[i] = reader.ReadLine();
                i++;
            }
            EndDialogAt = endConv;
            ActualTextToDisplay = new Text()
            {
                Font = font,
                DisplayedString = TextToDisplay[startConv]
            };
        }

        public void Update()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
            {
                if(EndDialogAt == NumberOfDialog)
                NumberOfDialog++;
                ActualTextToDisplay.DisplayedString = TextToDisplay[NumberOfDialog];
            }
        }

        public void Draw(RenderWindow window/*, Text textToDisplay*/)
        {
            window.Draw(drawable: Prostokat);
            window.Draw(drawable: ActualTextToDisplay);
        }
    }
}
