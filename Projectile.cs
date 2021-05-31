using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefenceEksamensProjekt
{
    public class Projectile : GameObject
    {
        private Enemy enemy;
        private int dmg;
        private int speed = 10;

        public Projectile(Enemy enemy, int dmg)
        {
            this.enemy = enemy;
            this.dmg = dmg;
        }

        public bool Move()
        {
            Vector2 direction = position - enemy.position;

            if (direction != Vector2.Zero)
                direction.Normalize();

            position -= (direction * speed);

            if (Vector2.Distance(position, enemy.position) < 15)
            {
                return true;
            }
            else
                return false;
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }
    }
}