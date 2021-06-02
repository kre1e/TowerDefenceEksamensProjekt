﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using TowerDefenceEksamensProjekt.FactoryPattern;
using TowerDefenceEksamensProjekt.Levels;

namespace TowerDefenceEksamensProjekt
{
    public class GameWorld : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public static SpriteFont userfont;

        private Texture2D collisionTexture;

        public static KeyboardState currentKeyState;
        public static KeyboardState previousKeyState;
        public static HighScore[] highscorearray;
        public static EnemyDB[] enemyarray;

        public static List<Projectile> projectilelist = new List<Projectile>();
        public static List<Projectile> projectileDeletelist = new List<Projectile>();

        public static List<Enemy> enemyList = new List<Enemy>();
        public List<Enemy> enemyToAdd = new List<Enemy>();
        public static List<Enemy> enemyDeleteList = new List<Enemy>();
        public static List<Building> buildingList = new List<Building>();
        public static ContentManager content;

        public static Level currrentLevel;
        public static string currentPlayer;

        private static GameWorld instance;

        public static double gameTime1;

        public static GameWorld Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameWorld();
                }

                return instance;
            }
        }

        public GameWorld()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Database.DatabaseSetup();
            content = Content;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
            userfont = Content.Load<SpriteFont>("File");
            collisionTexture = Content.Load<Texture2D>("Pixel");
            Tile.content = Content;
            Button.content = Content;
            Building.content = Content;
            TowerMenu.content = Content;
            GameObject.content = Content;
            currrentLevel = new LoginLevel();
            highscorearray = Database.Loadhighscore();
            enemyarray = Database.LoadEnemyDB();
        }

        protected override void Update(GameTime gameTime)
        {
            currentKeyState = Keyboard.GetState();

            currrentLevel.Update(gameTime);

            if (currentKeyState.IsKeyDown(Keys.Escape))
                Exit();

            gameTime1 = gameTime.TotalGameTime.TotalSeconds;
            previousKeyState = currentKeyState;

            foreach (Building gameob in buildingList)
            {
                gameob.Update(gameTime);

                foreach (Enemy enemy in enemyList)
                {
                    gameob.CheckCollision(enemy);
                }
            }

            foreach (var item in projectilelist)
            {
                item.Update(gameTime);
            }

            if (enemyDeleteList.Count > 0)
            {
                enemyList.RemoveAll(x => enemyDeleteList.Contains(x));
                enemyDeleteList.Clear();
            }

            if (projectileDeletelist.Count > 0)
            {
                foreach (var item in projectileDeletelist)
                {
                    projectilelist.Remove(item);
                }
                projectileDeletelist.Clear();
            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        public static void DestroyEnemy(Enemy item)
        {
            enemyDeleteList.Add(item);
        }

        public static void DestroyProjectile(Projectile item)
        {
            projectileDeletelist.Add(item);
        }

        public void DrawCollisionBox(GameObject go)
        {
            //Der laves en streg med tykkelsen 1 for hver side af Collision.
            Rectangle topLine = new Rectangle(go.Collision.X, go.Collision.Y, go.Collision.Width, 1);
            Rectangle bottomLine = new Rectangle(go.Collision.X, go.Collision.Y + go.Collision.Height, go.Collision.Width, 1);
            Rectangle rightLine = new Rectangle(go.Collision.X + go.Collision.Width, go.Collision.Y, 1, go.Collision.Height);
            Rectangle leftLine = new Rectangle(go.Collision.X, go.Collision.Y, 1, go.Collision.Height);
            //Der tegnes en streg med tykkelsen 1 for hver side af Collision med collsionTexture med farven rød.
            _spriteBatch.Draw(collisionTexture, topLine, Color.Red);
            _spriteBatch.Draw(collisionTexture, bottomLine, Color.Red);
            _spriteBatch.Draw(collisionTexture, rightLine, Color.Red);
            _spriteBatch.Draw(collisionTexture, leftLine, Color.Red);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin(SpriteSortMode.FrontToBack);
            currrentLevel.Draw(_spriteBatch);
            enemyList.AddRange(enemyToAdd);
            enemyToAdd.Clear();
            foreach (var go in enemyList)
            {
                go.Draw(_spriteBatch);
            }
            foreach (var go in projectilelist)
            {
                go.Draw(_spriteBatch);
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}