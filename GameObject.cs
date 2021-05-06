using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefenceEksamensProjekt
{
    public abstract class GameObject
    {
        protected Texture2D sprite;
        protected Vector2 velocity;
        public Vector2 position;
        protected Color color = Color.White;
        protected SpriteBatch _spriteBatch;

        public abstract void Draw(SpriteBatch spriteBatch);

        public abstract void Update(GameTime gameTime);
    }
}