using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefenceEksamensProjekt.FactoryPattern
{
    public class EnemyFactory : Factory
    {
        private static EnemyFactory instance;

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

        public override GameObject Create(string type)
        {
            Enemy enemy = new Enemy();

            switch (type)
            {
                case "Warrior":
                    enemy.SetSprite("enemy1");
                    break;

                case "Mage":
                    enemy.SetSprite("enemy2");
                    break;


            }
            return enemy;
        }
    }
}
