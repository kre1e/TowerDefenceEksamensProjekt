﻿//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Input;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace TowerDefenceEksamensProjekt
//{
//    public class TowerButton : GameObject
//    {
//        public new Action<Tile> action;
//        private bool release = true;
//        public Texture2D loginTexture;
//        public static ContentManager content;
//        public Rectangle Rectangle;
//        public string name;
//        public SpriteFont userfont;

//        public TowerButton(Rectangle newRectangle, string name, Action<Tile> action)
//        {
//            loginTexture = content.Load<Texture2D>("Tile1");
//            userfont = GameWorld.content.Load<SpriteFont>("File");
//            this.Rectangle = newRectangle;
//            this.name = name;
//            this.action = action;
//        }

//        public override void Draw(SpriteBatch spriteBatch)
//        {
//            spriteBatch.Draw(loginTexture, Rectangle, Color.Gray);
//            spriteBatch.DrawString(userfont, name, new Vector2(Rectangle.X + 20, Rectangle.Y), Color.White);
//        }

//        public override void Update(GameTime gameTime)
//        {
//            MouseState mouseState = Mouse.GetState();

//            if (Rectangle.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed && release)
//            {
//                action();
//                release = false;
//            }

//            if (mouseState.LeftButton == ButtonState.Released)
//                release = true;
//        }
//    }
//}