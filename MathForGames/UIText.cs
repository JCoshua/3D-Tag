using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;

namespace MathForGames
{
    class UIText : Actor
    {
        public string Text;
        public int Width;
        public int Height;
        public int FontSize;
        public Font Font;
        public Color FontColor;

        /// <summary>
        /// The UIText Constructor
        /// </summary>
        /// <param name="x">The X Position for the Text Box</param>
        /// <param name="y">The Y Position for the Text Box</param>
        /// <param name="name">The Name of the box</param>
        /// <param name="color">The Text Color</param>
        /// <param name="width">The length of the text box</param>
        /// <param name="height">the width</param>
        /// <param name="text">the text within the box</param>
        public UIText(float x, float y, float z, string name, Color color, int width, int height, int fontSize, string text = "")
            : base(x, y, z, name)
        {
            Text = text;
            Width = width;
            Height = height;
            Font = Raylib.LoadFont("resources/fonts/alagard.png");
            FontSize = fontSize;
            FontColor = color;
        }

        public override void Update(float deltaTime)
        {
            if (Name == "Timer")
            {
                Text = "Time: " + (int)SceneManager.TimeLeft;
                if(SceneManager.TimeLeft <= 0 && SceneManager.Allies[0].IsTagger && !SceneManager.GameOver)
                {
                    SceneManager.CurrentScene.RemoveUIElement(this);
                    UIText lose = new UIText(120, 100, 50, "Lose", Color.BLACK, 400, 400, 50, "You Lose");
                    SceneManager.CurrentScene.AddUIElement(lose);
                    SceneManager.GameOver = true;
                }
                else if(SceneManager.TimeLeft <= 0 && !SceneManager.Allies[0].IsTagger && !SceneManager.GameOver)
                {
                    SceneManager.CurrentScene.RemoveUIElement(this);
                    UIText win = new UIText(120, 100, 50, "Win", Color.BLACK, 400, 400, 50, "You Win");
                    SceneManager.CurrentScene.AddUIElement(win);
                    SceneManager.GameOver = true;
                }
            }
            else if(Name == "Team Remaining")
            {
                if(SceneManager.Allies[0].IsTagger)
                    Text = SceneManager.Enemies.Length + " Remaining!";
                else
                    Text = Text = SceneManager.Allies.Length + " Remaining!";

                if (SceneManager.Allies.Length <= 0 && !SceneManager.GameOver)
                {
                    SceneManager.CurrentScene.RemoveUIElement(this);
                    UIText lose = new UIText(120, 100, 50, "Lose", Color.BLACK, 400, 400, 50, "You Lose");
                    SceneManager.CurrentScene.AddUIElement(lose);
                    SceneManager.TimeLeft = 0;
                    SceneManager.GameOver = true;
                }
                else if(SceneManager.Enemies.Length <= 0 && !SceneManager.GameOver)
                {
                    SceneManager.CurrentScene.RemoveUIElement(this);
                    UIText win = new UIText(120, 100, 50, "Win", Color.BLACK, 400, 400, 50, "You Win");
                    SceneManager.CurrentScene.AddUIElement(win);
                    SceneManager.TimeLeft = 0;
                    SceneManager.GameOver = true;
                }
            }
            base.Update(deltaTime);
        }

        public override void Draw()
        {
            Rectangle textBox = new Rectangle(LocalPosition.X, LocalPosition.Y, Width, Height);
            Raylib.DrawTextRec(Font, Text, textBox, FontSize, 1, true, FontColor);
        }
    }
}
