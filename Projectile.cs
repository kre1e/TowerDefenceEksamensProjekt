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
        private int speed = 3;

        public Projectile(Enemy enemy, int dmg)
        {
            this.enemy = enemy;
            this.dmg = dmg;
            sprite = GameWorld.content.Load<Texture2D>("Tile1");
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
            while (Move())
            {
                Move();
            }
            //if (!Move())
            //    GameWorld.DestroyProjectile(this);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _spriteBatch.Draw(sprite, position, null, Color.Red, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1);
        }
    }
}