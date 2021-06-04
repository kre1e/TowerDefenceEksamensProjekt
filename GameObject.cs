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
        public float layerdef;
        protected Texture2D sprite;
        public Vector2 position;
        protected Color color = Color.White;
        protected SpriteBatch _spriteBatch;
        public static ContentManager content;

        public void SetSprite(string spriteName)
        {
            sprite = content.Load<Texture2D>(spriteName);
        }

        public virtual void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(sprite, position, null, color, 0f, Vector2.Zero, 1f, SpriteEffects.None, layerdef);
        }

        public virtual void Update(GameTime gameTime)
        {
        }
    }
}