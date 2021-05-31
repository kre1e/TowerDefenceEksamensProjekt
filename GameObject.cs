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

        public void SetSprite(string spriteName)
        {
            sprite = GameWorld.Instance.Content.Load<Texture2D>(spriteName);
        }

        public virtual void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(sprite, position + new Vector2(sprite.Width / 4, sprite.Height / 4), null, Color.White, 1f, new Vector2(sprite.Width / 2, sprite.Height / 2), 0.5f, SpriteEffects.None, 1);
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        
    }
}