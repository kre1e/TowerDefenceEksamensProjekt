using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace TowerDefenceEksamensProjekt
{
    public class Enemy : GameObject
    {
        public int dmg;
        public int hp;
        public int lv;
        public List<Vector2> pointsToMoves;
        public double Oldtime;


        public Enemy(int dmg, int hp, int lv)
        {
            this.dmg = dmg;
            this.hp = hp;
            this.lv = lv;

            this.layerdef = 0.22f;
            this.position = new Vector2(100, 100);
            pointsToMoves = new List<Vector2> { new Vector2(100, 100), new Vector2(1000, 100), new Vector2(1000, 1000), new Vector2(100, 1000) };
        }

        public bool Move(double gameTime)
        {
            if (pointsToMoves.Count > 1)
            {
                Vector2 direction = (pointsToMoves[1] - pointsToMoves[0]);
                direction.Normalize();
                this.position += direction * (float)(100f * gameTime);
                if (((direction.X > 0 && this.position.X > pointsToMoves[1].X) || (direction.Y > 0 && this.position.Y > pointsToMoves[1].Y)) || ((direction.X < 0 && this.position.X < pointsToMoves[1].X) || (direction.Y < 0 && this.position.Y < pointsToMoves[1].Y)))
                {
                    pointsToMoves.RemoveAt(0);
                }
                return true;
            }
            else
            {
                GameWorld.DestroyEnemy(this);
                return false;
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }


        public async Task Working()
        {
            double deltaTime = 0;
            Oldtime = GameWorld.gameTime1;
            while (Move(deltaTime))
            {
                deltaTime = GameWorld.gameTime1 - Oldtime;
                Oldtime = GameWorld.gameTime1;
                if (this.hp <= 0)
                {
                    GameWorld.DestroyEnemy(this);
                    return;
                }
                await Task.Delay(10);
            }
            return;

        }
    }
}