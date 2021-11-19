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
        /// <param name="color">The color of the Text</param>
        /// <param name="width">The Width of the Text Box</param>
        /// <param name="height">The Height of the Text Box</param>
        /// <param name="fontSize">The Size of the Text</param>
        /// <param name="text">The Text to be diplayed</param>
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

        /// <summary>
        /// Updates the UIText
        /// </summary>
        public override void Update(float deltaTime)
        {
            //Updates the Timer Ui
            if (Name == "Timer")
            {
                //Show the Time Left
                Text = "Time: " + (int)SceneManager.TimeLeft;
                //If there is no TimeLeft, the Runner Team Wins
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
            //Updates the Team Remaining UI
            else if(Name == "Team Remaining")
            {
                //Diplays the Ruuner's Team Status
                if(SceneManager.Allies[0].IsTagger)
                    Text = SceneManager.Enemies.Length + " Remaining!";
                else
                    Text = Text = SceneManager.Allies.Length + " Remaining!";

                //If all Allies have been caught, Enemies Win
                if (SceneManager.Allies.Length <= 0 && !SceneManager.GameOver)
                {
                    SceneManager.CurrentScene.RemoveUIElement(this);
                    UIText lose = new UIText(120, 100, 50, "Lose", Color.BLACK, 400, 400, 50, "You Lose");
                    SceneManager.CurrentScene.AddUIElement(lose);
                    SceneManager.TimeLeft = 0;
                    SceneManager.GameOver = true;
                }
                //If all Enemies have been caught, Allies Win
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

        /// <summary>
        /// Draws The Text Box
        /// </summary>
        public override void Draw()
        {
            Rectangle textBox = new Rectangle(LocalPosition.X, LocalPosition.Y, Width, Height);
            Raylib.DrawTextRec(Font, Text, textBox, FontSize, 1, true, FontColor);
        }
    }
}
