using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefenceEksamensProjekt
{
    class EnemyFactory : Factory
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

        private static Random rnd = new Random();

        public override GameObject Create(string type)
        {
            GameObject go = new GameObject();
            SpriteRenderer sr = new SpriteRenderer();
            go.AddComponent(sr);

            switch (type)
            {
                case "Blue":
                    sr.SetSprite("enemy1");
                    go.AddComponent(new Enemy(50, new Vector2(0, 1)));
                    break;
                case "Black":
                    sr.SetSprite("enemy2");
                    go.AddComponent(new Enemy(100, new Vector2(0, 1)));
                    break;

            }

            return go;
        }
    }
}
