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
        public int health = 50;
        public int PlayerCurrency;

        public List<Enemy> enemyList;
        public List<Projectile> projectilelist;

        public Building(string name)
        {
            this.name = name;
            sprite = content.Load<Texture2D>(name);
        }

        public void setBuilding()
        {
            (GameWorld.currrentLevel as GameLevel).towerMenu.n.ContainTower = (Building)this.MemberwiseClone();
        }

        public void Attack(GameTime gametime, List<Enemy> enemyList, List<Projectile> projectilelist)
        {
            if (enemyList != null)
            {
                foreach (Enemy enemy in enemyList)
                {
                    if ((int)Math.Sqrt(Math.Pow(this.position.X - enemy.position.X, 2) + Math.Pow(this.position.Y - enemy.position.Y, 2)) <= range)
                    {
                        if ((gametime.ElapsedGameTime.TotalSeconds - cooldown) > attackspeed)
                        {
                            projectilelist.Add(new Projectile(enemy, dmg));
                            cooldown = gametime.ElapsedGameTime.TotalSeconds;
                        }
                        var distance = enemy.position - this.position;
                        rotatetion = (float)Math.Atan2(distance.Y, distance.X);
                    }
                }
            }
        }

        public void BananaFarm(GameTime gametime)
        {
            if ((gametime.ElapsedGameTime.TotalSeconds - cooldown) > attackspeed)
            {
                PlayerCurrency++;
                cooldown = gametime.ElapsedGameTime.TotalSeconds;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position + new Vector2(sprite.Width / 4, sprite.Height / 4), null, Color.White, rotatetion, new Vector2(sprite.Width / 2, sprite.Height / 2), 0.5f, SpriteEffects.None, 1);
        }

        public override void Update(GameTime gameTime)
        {
            switch (name)
            {
                case "Base":
                    health = 1000;
                    return;

                case "Tower":
                    Attack(gameTime, enemyList, projectilelist);
                    return;

                case "IceTower":
                    Attack(gameTime, enemyList, projectilelist);
                    return;

                case "Wall":
                    health = 500;
                    return;

                case "BananaFarm":
                    BananaFarm(gameTime);
                    return;

                default:
                    return;
            }
        }

        public Object Clone()
        {
            return (Building)this.MemberwiseClone();
        }
    }
}