using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefenceEksamensProjekt
{
    public class Button : GameObject
    {
        public Action action;
        private bool release = true;
        public Texture2D loginTexture;
        public Rectangle Rectangle;
        public string name;
        public SpriteFont userfont;

        public Button(Rectangle newRectangle, string name, Action action)
        {
            loginTexture = content.Load<Texture2D>("Tile2");
            userfont = GameWorld.content.Load<SpriteFont>("File");
            this.Rectangle = newRectangle;
            this.name = name;
            this.action = action;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(loginTexture, Rectangle, null, color, 0, Vector2.Zero, SpriteEffects.None, layerdef);
            spriteBatch.DrawString(userfont, name, new Vector2(Rectangle.X + 20, Rectangle.Y), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, layerdef + 0.01f);
        }

        public override void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();

            if (Rectangle.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed && release)
            {
                action();
                release = false;
            }

            if (mouseState.LeftButton == ButtonState.Released)
                release = true;
        }
    }
}