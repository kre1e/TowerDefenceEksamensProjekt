using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefenceEksamensProjekt
{
    public class Enemy : GameObject
    {
        public int dmg;
        public int hp;
        public int lv;

        public Enemy(int dmg, int hp, int lv)
        {
            this.dmg = dmg;
            this.hp = hp;
            this.lv = lv;
            this.layerdef = 0.22f;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            this.position += new Vector2(0, (float)(10f * gameTime.ElapsedGameTime.TotalSeconds));
            if (this.hp <= 0)
            {
                GameWorld.DestroyEnemy(this);
            }
        }
    }
}