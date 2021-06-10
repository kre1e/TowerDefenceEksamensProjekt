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

        //EnemyPackBuild bliver brugt til at lave en pack af enemys, dette gør den ved at adde en enemy ved at bruge EnemyFactory create funtion og heraf navnet pa funktionen
        //Der efter sætter jeg positionen.
        //Og Adder den til en list af task.
        //Efter sætter jeg et delay ind for at enemysne ikke skal gå oven pa hindannen.
        //Efter ventes der pa at alle task er færdige, hvor den så forsatter med at køre det rekursive loop.
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