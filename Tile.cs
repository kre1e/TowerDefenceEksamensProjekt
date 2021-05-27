using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefenceEksamensProjekt
{
    public class Tile : GameObject
    {
        public Texture2D tile;

        public Rectangle Rectangle;
        public static ContentManager content;
        private GameObject containTower;

        public GameObject ContainTower
        {
            get
            {
                return containTower;
            }
            set
            {
                containTower = value;
                if (containTower != null)
                    containTower.position = new Vector2(Rectangle.X, Rectangle.Y);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tile, Rectangle, color);
            if (ContainTower != null)
                ContainTower.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            if (ContainTower != null)
                ContainTower.Update(gameTime);
        }
    }

    public class CoolTile : Tile
    {
        public CoolTile(int i, Rectangle newRectangle)
        {
            tile = content.Load<Texture2D>("Tile" + i);
            this.Rectangle = newRectangle;
        }
    }
}