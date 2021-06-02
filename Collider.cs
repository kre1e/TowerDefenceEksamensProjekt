using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenceEksamensProjekt
{
    public class Collider : GameObject
    {
        public bool CheckCollisionEvents { get; set; }

        private Vector2 size;

        private Vector2 origin;

        private Texture2D texture;

        public Rectangle CollisionBox
        {
            get
            {
                return new Rectangle
                (
                    (int)(this.position.X - origin.X),

                    (int)(this.position.Y - origin.Y),

                    (int)size.X,
                    (int)size.Y
                );
            }
        }

        public Collider()
        {
            texture = GameWorld.content.Load<Texture2D>("CollisionBox");
            this.size = new Vector2(texture.Width, texture.Height);
        }

        public void OnCollisionEnter(Collider other)
        {
            if (CheckCollisionEvents)
            {
                if (other != this)
                {
                    if (CollisionBox.Intersects(other.CollisionBox))
                    {
                        //Trigger collision
                    }
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, CollisionBox, null, Color.Red, 0, origin, SpriteEffects.None, 0);
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}