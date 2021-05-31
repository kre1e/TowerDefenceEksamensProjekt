using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefenceEksamensProjekt.FactoryPattern
{
    public class EnemyFactory : Factory
    {
        private static EnemyFactory instance;
        private EnemyDB[] enemyDBs;

        public static EnemyFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EnemyFactory();
                }
                return instance;
            }
        }

        public override Enemy Create(string type)
        {
            enemyDBs = GameWorld.enemyarray;
            Enemy enemy;
            switch (type)
            {
                case "Org":
                    enemy = new Enemy(enemyDBs[0].Dmg, enemyDBs[0].HP, enemyDBs[0].Lv);
                    enemy.SetSprite(enemyDBs[0].Name);
                    break;

                case "Golem":
                    enemy = new Enemy(enemyDBs[1].Dmg, enemyDBs[1].HP, enemyDBs[1].Lv);
                    enemy.SetSprite(enemyDBs[1].Name);
                    break;

                case "Shadow Knight":
                    enemy = new Enemy(enemyDBs[2].Dmg, enemyDBs[2].HP, enemyDBs[2].Lv);
                    enemy.SetSprite(enemyDBs[2].Name);
                    break;

                case "Shadow Lord":
                    enemy = new Enemy(enemyDBs[3].Dmg, enemyDBs[3].HP, enemyDBs[3].Lv);
                    enemy.SetSprite(enemyDBs[3].Name);
                    break;

                case "Royal Knight":
                    enemy = new Enemy(enemyDBs[4].Dmg, enemyDBs[4].HP, enemyDBs[4].Lv);
                    enemy.SetSprite(enemyDBs[4].Name);
                    break;

                case "Org King":
                    enemy = new Enemy(enemyDBs[5].Dmg, enemyDBs[5].HP, enemyDBs[5].Lv);
                    enemy.SetSprite(enemyDBs[5].Name);
                    break;

                case "Dark Prince":
                    enemy = new Enemy(enemyDBs[6].Dmg, enemyDBs[6].HP, enemyDBs[6].Lv);
                    enemy.SetSprite(enemyDBs[6].Name);
                    break;

                case "Boss Golem":
                    enemy = new Enemy(enemyDBs[7].Dmg, enemyDBs[7].HP, enemyDBs[7].Lv);
                    enemy.SetSprite(enemyDBs[7].Name);
                    break;

                case "Maō":
                    enemy = new Enemy(enemyDBs[8].Dmg, enemyDBs[8].HP, enemyDBs[8].Lv);
                    enemy.SetSprite(enemyDBs[8].Name);
                    break;

                default:
                    enemy = new Enemy(enemyDBs[1].Dmg, enemyDBs[1].HP, enemyDBs[1].Lv);
                    enemy.SetSprite(enemyDBs[1].Name);
                    break;
            }
            return enemy;
        }
    }
}