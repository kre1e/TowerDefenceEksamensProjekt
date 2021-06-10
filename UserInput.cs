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
            spriteBatch.Draw(loginTexture, Rectangle, null, Color.Gray, 0, Vector2.Zero, SpriteEffects.None, layerdef);
            spriteBatch.DrawString(userfont, informationText + currentInput, new Vector2(Rectangle.X - 80, Rectangle.Y + 10), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, layerdef + 0.01f);
        }

        //En funktion der returnere true hvis man trykker og slipper en knap.
        private bool KeypressTest(Keys theKey)
        {
            if (GameWorld.currentKeyState.IsKeyUp(theKey) && GameWorld.previousKeyState.IsKeyDown(theKey))
                return true;

            return false;
        }

        //Et update loop som tjekker om man skriver i userInput rectangle, hvis man er i rectanglen og man skriver bliver der tilfojet keyvalues til en streng.
        //Der er blandt andet ogsa lavet en delete sa man kan fjerne en del af strengen fra currentInput.
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