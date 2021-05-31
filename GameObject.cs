using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TowerDefenceEksamensProjekt.FactoryPattern;

namespace TowerDefenceEksamensProjekt
{
    public class GameObject
    {

        protected Texture2D sprite;
        protected Vector2 velocity;
        public Vector2 position;
        protected Color color = Color.White;
        protected SpriteBatch _spriteBatch;
        public static ContentManager content;

        protected int offsetX;
        protected int offsetY;
        protected int sizeX;
        protected int sizeY;

        protected Vector2 scale;
        protected float rotation;

        public virtual Rectangle Collision
        {
            get
            {
                return new Rectangle(
                       (int)position.X + offsetX,
                       (int)position.Y + offsetY,
                       (int)sprite.Width + sizeX,
                       (int)sprite.Height + sizeY
                   );
            }
        }

        public void SetSprite(string spriteName)
        {
            sprite = content.Load<Texture2D>(spriteName);
        }

        public virtual void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(sprite, position, null, color, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 1);
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void OnCollision(GameObject other)
        {
        }

        public void CheckCollision(GameObject other)
        {
            if (Collision.Intersects(other.Collision) && this != other)
            {
                OnCollision(other);
            }
        }


    }
}