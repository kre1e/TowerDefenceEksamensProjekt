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
        private int range = 200;
        private int dmg = 5;
        private float attackspeed = 1;
        private double cooldown = 0;
        public Vector2 origin;
        public float rotatetion;
        public int health = 50;
        public int PlayerCurrency;

        public List<Enemy> enemyList;

        public Building(string name)
        {
            this.name = name;
            sprite = content.Load<Texture2D>(name);
        }

        public void SetBuilding()
        {
            (GameWorld.currrentLevel as GameLevel).towerMenu.n.ContainTower = (Building)this.MemberwiseClone();
        }

        public void Attack(GameTime gametime, List<Enemy> enemyList)
        {
            if (enemyList != null)
            {
                foreach (Enemy enemy in enemyList)
                {
                    if ((int)Math.Sqrt(Math.Pow(this.position.X - enemy.position.X, 2) + Math.Pow(this.position.Y - enemy.position.Y, 2)) <= range)
                    {
                        if ((gametime.TotalGameTime.TotalSeconds - cooldown) > attackspeed)
                        {
                            GameWorld.projectilelist.Add(new Projectile(enemy, dmg, this.position));

                            cooldown = gametime.TotalGameTime.TotalSeconds;
                        }
                        Vector2 currentposition = position + new Vector2(sprite.Width / 4, sprite.Height / 4);
                        var distance = enemy.position - currentposition;
                        rotatetion = (float)Math.Atan2(distance.Y, distance.X) + (float)Math.PI / 2;
                        break;
                    }
                }
            }
        }

        public void BananaFarm(GameTime gametime)
        {
            if ((gametime.TotalGameTime.TotalSeconds - cooldown) > attackspeed)
            {
                PlayerCurrency++;
                cooldown = gametime.TotalGameTime.TotalSeconds;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position + new Vector2(sprite.Width / 4, sprite.Height / 4), null, Color.White, rotatetion, new Vector2(sprite.Width / 2, sprite.Height / 2), 0.5f, SpriteEffects.None, 0.1f);
        }

        public override void Update(GameTime gameTime)
        {
            switch (name)
            {
                case "Base":
                    health = 1000;
                    return;

                case "Tower":
                    Attack(gameTime, GameWorld.enemyList);
                    return;

                case "FlameTower":
                    Attack(gameTime, GameWorld.enemyList);
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