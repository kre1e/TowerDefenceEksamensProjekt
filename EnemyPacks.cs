using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using TowerDefenceEksamensProjekt.FactoryPattern;

namespace TowerDefenceEksamensProjekt
{
    public class EnemyPacks
    {
        private static EnemyPacks instance;

        public static EnemyPacks Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EnemyPacks();
                }

                return instance;
            }
        }

        public async void EnemyPackBuilder(string EnemyPackName, int PackSize, Vector2 EnemySpawnLocation)
        {
            List<Task> listOfTask = new List<Task>();

            for (int i = 0; i < PackSize; i++)
            {
                GameWorld.Instance.enemyToAdd.Add(EnemyFactory.Instance.Create(EnemyPackName));
                GameWorld.Instance.enemyToAdd[GameWorld.Instance.enemyToAdd.Count - 1].position = EnemySpawnLocation;
                listOfTask.Add(GameWorld.Instance.enemyToAdd[GameWorld.Instance.enemyToAdd.Count - 1].Working());
                await Task.Delay(200);
            }
            await Task.WhenAll(listOfTask);

            EnemyPackBuilder(EnemyPackName, (int)(PackSize * 1.5f), EnemySpawnLocation);
        }
    }
}