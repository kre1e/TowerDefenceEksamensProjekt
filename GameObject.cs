﻿using Microsoft.Xna.Framework;
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

        public void SetSprite(string spriteName)
        {
            sprite = content.Load<Texture2D>(spriteName);
        }

        public virtual void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(sprite, position, null, Color.White, 0f, new Vector2(sprite.Width / 2, sprite.Height / 2), 1f, SpriteEffects.None, 1);
        }

        public virtual void Update(GameTime gameTime)
        {
        }
    }
}