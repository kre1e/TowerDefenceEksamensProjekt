using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefenceEksamensProjekt
{
    public class Enemy : Component
    {
        private float speed;

        private Vector2 velocity;

        public Vector2 position;

        public Enemy(float speed, Vector2 velocity)
        {
            this.speed = speed;
            this.velocity = velocity;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            Move();
        }
        private void Move()
        {
            GameObject.Transform.Translate(velocity * speed * GameWorld.Instance.DeltaTime);
        }
    }
}