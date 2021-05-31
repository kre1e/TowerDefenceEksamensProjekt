using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefenceEksamensProjekt
{
    public class Enemy : GameObject
    {
        public static Vector2 CurrentPosition = new Vector2(224, 224);
        public int dmg;
        public int hp;

        public Enemy(int dmg, int hp, int lv)
        {
            position = CurrentPosition;
            this.dmg = dmg;
            this.hp = hp;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}