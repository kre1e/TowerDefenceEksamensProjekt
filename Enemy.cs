using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefenceEksamensProjekt
{
    public class Enemy : GameObject
    {
        public static Vector2 CurrentPosition = new Vector2(200, 200);
        public Vector2 Position
        {
            get
            {
                return position;
            }
        }
        public Enemy()
        {
            position = CurrentPosition;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

        }

        public override void Update(GameTime gameTime)
        {

        }


    }
}