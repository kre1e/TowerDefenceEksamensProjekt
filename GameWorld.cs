using Microsoft.Xna.Framework;
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

        public static List<Projectile> projectilelist = new List<Projectile>();
        public List<GameObject> gameobjects = new List<GameObject>();
        public static ContentManager content;

        public static Level currrentLevel;
        public static string currentPlayer;

        private static GameWorld instance;

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
            gameobjects.Add(EnemyFactory.Instance.Create("Warrior"));
            gameobjects.Add(EnemyFactory.Instance.Create("Mage"));
            currrentLevel = new LoginLevel();
            highscorearray = Database.Loadhighscore();
        }

        protected override void Update(GameTime gameTime)
        {
            currentKeyState = Keyboard.GetState();

            currrentLevel.Update(gameTime);

            if (currentKeyState.IsKeyDown(Keys.Escape))
                Exit();

            previousKeyState = currentKeyState;

            foreach (GameObject gameob in gameobjects)
            {
                gameob.Update(gameTime);
                foreach (GameObject item in gameobjects)
                {
                    gameob.CheckCollision(item);
                }
            }
            // TODO: Add your update logic here

            base.Update(gameTime);
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
            _spriteBatch.Begin();
            currrentLevel.Draw(_spriteBatch);
            foreach (var go in gameobjects)
            {
#if DEBUG
                DrawCollisionBox(go);
#endif
                go.Draw(_spriteBatch);
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}