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
        private int speed = 300;

        public Projectile(Enemy enemy, int dmg, Vector2 towerPosition)
        {
            this.enemy = enemy;
            this.dmg = dmg;
            sprite = GameWorld.content.Load<Texture2D>("Tile1");
            this.position = towerPosition;
        }

        //En move funktion som bevæger sig til enemy position.
        public bool Move(GameTime gameTime)
        {
            Vector2 direction = position - enemy.position;

            if (direction != Vector2.Zero)
                direction.Normalize();

            position -= (direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds);

            if (Vector2.Distance(position, enemy.position) > 15)
            {
                return false;
            }
            else
                return true;
        }

        public override void Update(GameTime gameTime)
        {
            if (Move(gameTime))
            {
                enemy.hp -= dmg;
                GameWorld.DestroyProjectile(this);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Rectangle projectileRectagle = new Rectangle((int)position.X, (int)position.Y, 10, 10);
            spriteBatch.Draw(sprite, projectileRectagle, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1f);
        }
    }
}