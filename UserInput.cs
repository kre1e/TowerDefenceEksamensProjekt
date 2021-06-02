using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using TowerDefenceEksamensProjekt.Levels;

namespace TowerDefenceEksamensProjekt
{
    public class UserInput : GameObject
    {
        public Texture2D loginTexture;
        public static ContentManager content;
        public Rectangle Rectangle;
        private LoginLevel parrentUserLogin;
        public string currentInput;
        public string informationText;
        public SpriteFont userfont;
        private Keys keyValue;

        public UserInput(Rectangle newRectangle, LoginLevel parrentUserLogin, string informationText)
        {
            currentInput = "";
            this.informationText = informationText;
            this.parrentUserLogin = parrentUserLogin;
            loginTexture = GameWorld.content.Load<Texture2D>("Tile2");
            userfont = GameWorld.content.Load<SpriteFont>("File");
            this.Rectangle = newRectangle;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(loginTexture, Rectangle, Color.Gray);
            spriteBatch.DrawString(userfont, informationText + currentInput, new Vector2(Rectangle.X - 80, Rectangle.Y + 10), Color.Black);
        }

        private bool KeypressTest(Keys theKey)
        {
            if (GameWorld.currentKeyState.IsKeyUp(theKey) && GameWorld.previousKeyState.IsKeyDown(theKey))
                return true;

            return false;
        }

        public override void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            if (Rectangle.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed)
            {
                parrentUserLogin.activeUserInput = this;
            }

            if (parrentUserLogin.activeUserInput == this)
            {
                Keys[] keys = GameWorld.currentKeyState.GetPressedKeys();
                if (KeypressTest(keyValue))
                {
                    currentInput += keyValue.ToString();
                    if (GameWorld.previousKeyState.IsKeyDown(Keys.Back))
                    {
                        if (currentInput.Length == 4)
                            currentInput = currentInput.Remove(currentInput.Length - 4, 4);
                        else
                            currentInput = currentInput.Remove(currentInput.Length - 5, 5);
                    }
                }
                else if (keys.Length > 0)
                    keyValue = keys[0];
            }
        }
    }
}