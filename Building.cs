using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefenceEksamensProjekt
{
    public class Building : GameObject
    {
        private int cost = 20;
        private int range = 100;
        private int dmg = 5;
        private float attackspeed;
        private double cooldown = 0;
        private Texture2D sprite;
        public static ContentManager content;

        public Building()
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            sprite = content.Load<Texture2D>("Tile");
            spriteBatch.Draw(sprite, new Rectangle((int)position.X, (int)position.Y, 64, 64), color);
        }

        public void Attack(GameTime gametime, List<Enemy> enemyList, List<Projectile> projectilelist)
        {
            if ((gametime.ElapsedGameTime.TotalSeconds - cooldown) > attackspeed)
            {
                foreach (Enemy enemy in enemyList)
                {
                    if ((int)Math.Sqrt(Math.Pow(this.position.X - enemy.position.X, 2) + Math.Pow(this.position.Y - enemy.position.Y, 2)) <= range)
                    {
                        projectilelist.Add(new Projectile(enemy, dmg));
                        cooldown = gametime.ElapsedGameTime.TotalSeconds;
                    }
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}