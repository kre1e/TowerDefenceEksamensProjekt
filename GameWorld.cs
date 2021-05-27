using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using TowerDefenceEksamensProjekt.Levels;

namespace TowerDefenceEksamensProjekt
{
    public class GameWorld : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public static SpriteFont userfont;

        public static KeyboardState currentKeyState;
        public static KeyboardState previousKeyState;
        public static HighScore[] highscorearray;

        public static List<Enemy> listEnemy = new List<Enemy>();
        public static List<Projectile> projectilelist = new List<Projectile>();
        private List<GameObject> gameObjects = new List<GameObject>();
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
        public float DeltaTime { get; set; }

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

            GameObject go = new GameObject();

            go.AddComponent(new SpriteRenderer());

            gameObjects.Add(go);

            gameObjects.Add(EnemyFactory.Instance.Create("Blue"));
            gameObjects.Add(EnemyFactory.Instance.Create("Black"));

            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Awake();
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
            userfont = Content.Load<SpriteFont>("File");
            Tile.content = Content;
            Button.content = Content;
            Building.content = Content;
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

            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Update(gameTime);
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            currrentLevel.Draw(_spriteBatch);

            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Draw(_spriteBatch);
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}