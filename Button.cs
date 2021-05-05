using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefenceEksamensProjekt
{
    public class Button : GameObject
    {
        public Texture2D loginTexture;
        public static ContentManager content;
        public Rectangle Rectangle;

        public Button(Rectangle newRectangle)
        {
            loginTexture = content.Load<Texture2D>("Tile1");
            this.Rectangle = newRectangle;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(loginTexture, Rectangle, Color.Gray);
        }
    }
}