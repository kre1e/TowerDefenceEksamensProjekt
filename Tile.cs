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
        private GameObject containTower;

        //Et gameobject hvor buildings bliver sat pa nar de bliver bygget.
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
            spriteBatch.Draw(tile, Rectangle, null, color, 0f, Vector2.Zero, SpriteEffects.None, 0f);
            if (ContainTower != null)
                ContainTower.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            if (ContainTower != null)
                ContainTower.Update(gameTime);
        }
    }

    //En tile som man kan andre spriten og storelsen pa den rectangle.
    public class CoolTile : Tile
    {
        public CoolTile(int i, Rectangle newRectangle)
        {
            tile = content.Load<Texture2D>("Tile" + i);
            this.Rectangle = newRectangle;
        }
    }
}