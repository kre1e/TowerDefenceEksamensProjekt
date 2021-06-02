using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefenceEksamensProjekt
{
    enum TileType { Base, Tower, FlameTower, Wall, BananaFarm, Empty}
    public class Tile : GameObject
    {
        public Texture2D tile;

        private Point myPos;

        private bool walkAble;
        TileType mytype = TileType.Empty;

        public Rectangle Rectangle;
        public static ContentManager content;
        private GameObject containTower;

        public bool WalkAble
        {
            get { return walkAble; }
            set { walkAble = value; }
        }

        private Node myNode;

        internal Node MyNode
        {
            get { return myNode; }
            set { myNode = value; }
        }

        private Color myColor;
        public Color MyColor
        {
            get { return myColor; }
            set { myColor = value; }
        }

        public Point MyPos
        {
            get { return myPos; }
            set { myPos = value; }
        }

        public Tile(Point pos)
        {
            this.myPos = pos;
            walkAble = true;
        }

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