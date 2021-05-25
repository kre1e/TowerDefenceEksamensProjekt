using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TowerDefenceEksamensProjekt.Levels;

namespace TowerDefenceEksamensProjekt
{
    public class Building : GameObject, ICloneable
    {
        public string name;
        private int cost = 20;
        private int range = 100;
        private int dmg = 5;
        private float attackspeed;
        private double cooldown = 0;
        public static ContentManager content;
        public Vector2 origin;
        public float rotatetion;

        public Building(string name)
        {
            this.name = name;
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

            foreach (Enemy enemy in enemyList)
            {
                if ((int)Math.Sqrt(Math.Pow(this.position.X - enemy.position.X, 2) + Math.Pow(this.position.Y - enemy.position.Y, 2)) <= range)
                {
                    var distance = enemy.position - this.position;
                    rotatetion = (float)Math.Atan2(distance.Y, distance.X);
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            sprite = content.Load<Texture2D>("Tower1");
            //spriteBatch.Draw(sprite, new Rectangle((int)position.X, (int)position.Y, 64, 64), color);
            spriteBatch.Draw(sprite, position + new Vector2(sprite.Width / 4, sprite.Height / 4), null, Color.White, 1f, new Vector2(sprite.Width / 2, sprite.Height / 2), 0.5f, SpriteEffects.None, 1);
        }

        public override void Update(GameTime gameTime)
        {
        }

        public Object Clone()
        {
            return (Building)this.MemberwiseClone();
        }
    }
}