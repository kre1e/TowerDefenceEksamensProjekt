using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using TowerDefenceEksamensProjekt.FactoryPattern;

namespace TowerDefenceEksamensProjekt
{
    public class EnemyPacks
    {
        private List<Enemy> enemeyList = new List<Enemy>();

        public void EnemyPackBuilder(string EnemyPackName, int PackSize, Vector2 EnemySpawnLocation)
        {
            enemeyList = GameWorld.enemyList;

            for (int i = 0; i < PackSize; i++)
            {
                enemeyList.Add(EnemyFactory.Instance.Create(EnemyPackName));
                enemeyList[i].position = EnemySpawnLocation;
            }
        }
    }
}