using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefenceEksamensProjekt
{
    public class Map : GameObject
    {
        public int width, height;
        public CoolTile[,] Tiles;

        public Map()
        {
        }

        public void Gen(int[,] map, int size)
        {
            Tiles = new CoolTile[map.GetLength(0), map.GetLength(1)];
            for (int i = 0; i < map.GetLength(1); i++)
            {
                for (int j = 0; j < map.GetLength(0); j++)
                {
                    int number = map[j, i];

                    if (number > 0)
                        Tiles[j, i] = new CoolTile(number, new Rectangle(i * size, j * size, size, size));

                    width = (i + 1) * size;
                    height = (j + 1) * size;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (Tile tile in Tiles)
            {
                tile.Draw(spriteBatch);
            }
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Tile tile in Tiles)
            {
                tile.Update(gameTime);
            }
        }
    }
}