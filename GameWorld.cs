using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace TowerDefenceEksamensProjekt
{
    public class GameWorld : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Map map;
        private int score;
        private readonly Random _random = new Random();
        private string passValue = string.Empty;
        private string userValue = string.Empty;
        public SpriteFont userfont;
        public Button userlog;
        public Button passlog;
        public Button userlogin;
        public Button buidlingLoadOut;
        public Texture2D userbackground;
        public textfied currenttexfied;
        public bool hidelogin = true;

        public KeyboardState currentKeyState;
        public KeyboardState previousKeyState;
        public Realm realm;
        private Keys keyValue;
        public HighScore[] highscorearray;

        private bool ShowScoreBoard = false;
        private bool dropDownMenu = false;
        private SpriteFont font;
        private SpriteFont headLine;
        public bool showBuildingLoadOut = false;
        public List<Building> TowerList = new List<Building>();
        public List<Button> buildingMenuButton = new List<Button>();
        public List<Button> deleteButtons;

        public static List<Enemy> listEnemy = new List<Enemy>();
        public static List<Projectile> projectilelist = new List<Projectile>();
        public List<Enemy> deleteEnemyList;
        public buildplace currentbuilding;
        public List<Building> BuildingList = new List<Building>();
        public bool show = true;

        public Item[] menuArray;

        public enum textfied
        {
            none,
            user,
            pass,
            buildingMenuButton,
        }

        public enum buildplace
        {
            none,
            placeing,
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
            map = new Map();
            realm = new Realm();
            Database.DatabaseSetup();

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
            realm.Map3(map);
            userbackground = Content.Load<Texture2D>("Tile1");
            userlog = new Button(new Rectangle(880, 450, 100, 30));
            passlog = new Button(new Rectangle(880, 500, 100, 30));
            userlogin = new Button(new Rectangle(880, 550, 100, 30));
            TowerList.Add(new Building());
            TowerList.Add(new Building());
            TowerList.Add(new Building());
            TowerList.Add(new Building());
            deleteButtons = new List<Button>();

            highscorearray = Database.Loadhighscore();
        }

        private bool KeypressTest(Keys theKey)
        {
            if (currentKeyState.IsKeyUp(theKey) && previousKeyState.IsKeyDown(theKey))
                return true;

            return false;
        }

        public void Destroy(Button go)
        {
            deleteButtons.Add(go);
        }

        protected override void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            currentKeyState = Keyboard.GetState();
            var keys = currentKeyState.GetPressedKeys();

            if (currentKeyState.IsKeyDown(Keys.Escape))
                Exit();

            if (userlog.Rectangle.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed)
                currenttexfied = textfied.user;
            else if (passlog.Rectangle.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed)
                currenttexfied = textfied.pass;
            else if (mouseState.LeftButton == ButtonState.Pressed)
            {
                currenttexfied = textfied.none;
                currentbuilding = buildplace.none;
            }

            if (currenttexfied == textfied.user)
            {
                if (KeypressTest(keyValue))
                {
                    userValue += keyValue.ToString();
                    if (previousKeyState.IsKeyDown(Keys.Back))
                    {
                        if (userValue.Length == 4)
                            userValue = userValue.Remove(userValue.Length - 4, 4);
                        else
                            userValue = userValue.Remove(userValue.Length - 5, 5);
                    }
                }
                else if (keys.Length > 0)
                    keyValue = keys[0];
            }

            if (currenttexfied == textfied.pass)
            {
                if (KeypressTest(keyValue))
                {
                    passValue += keyValue.ToString();
                    if (previousKeyState.IsKeyDown(Keys.Back))
                    {
                        if (passValue.Length == 4)
                            passValue = passValue.Remove(passValue.Length - 4, 4);
                        else
                            passValue = passValue.Remove(passValue.Length - 5, 5);
                    }
                }
                else if (keys.Length > 0)
                    keyValue = keys[0];
            }

            if (hidelogin == false)
            {
                if (currentKeyState.IsKeyDown(Keys.Tab))
                    ShowScoreBoard = true;
                else
                    ShowScoreBoard = false;

                foreach (Tile n in map.Tiles)
                {
                    if (n.Rectangle.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed)
                    {
                        if (n.containTower)
                        {
                            //Tower upgrade
                        }
                        else
                        {
                            deleteButtons.AddRange(buildingMenuButton);
                            currentbuilding = buildplace.placeing;
                            //Place building
                            List<Item> itemList = new List<Item>();
                            for (int i = 0; i < TowerList.Count; i++)
                            {
                                var itemMenu = new Item();
                                itemMenu.button = new Button(new Rectangle(mouseState.X, mouseState.Y + i * 30, 100, 30)); ;
                                itemMenu.building = TowerList[i];
                                itemList.Add(itemMenu);
                            }

                            menuArray = itemList.ToArray();
                        }
                    }
                    if (menuArray != null && menuArray.Length > 0)
                        for (int i = 0; i < menuArray.Length; i++)
                        {
                            if (menuArray[i].button.Rectangle.Contains(mouseState.Position) && mouseState.RightButton == ButtonState.Pressed)
                            {
                                BuildingList.Add(menuArray[i].building);
                                currentbuilding = buildplace.none;

                                //n.containTower = true;
                            }
                        }
                }

                if (currentbuilding == buildplace.placeing)
                    showBuildingLoadOut = true;

                if (currentbuilding == buildplace.none)
                    showBuildingLoadOut = false;
            }

            if (userlogin.Rectangle.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed)
            {
                if (Database.Userlogin(userValue, passValue))
                {
                    hidelogin = false;
                }
            }

            foreach (var go in deleteButtons)
            {
                buildingMenuButton.Remove(go);
            }

            previousKeyState = currentKeyState;

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            if (hidelogin == true)
            {
                _spriteBatch.Draw(userbackground, new Rectangle(userlog.Rectangle.X - 100, userlog.Rectangle.Y - 20, 300, 180), Color.White);
                userlog.Draw(_spriteBatch);
                passlog.Draw(_spriteBatch);
                userlogin.Draw(_spriteBatch);
                _spriteBatch.DrawString(userfont, "Username:  " + userValue, new Vector2(userlog.Rectangle.X - 80, userlog.Rectangle.Y + 10), Color.Black);
                _spriteBatch.DrawString(userfont, "Password:  " + passValue, new Vector2(passlog.Rectangle.X - 80, passlog.Rectangle.Y + 10), Color.Black);
                _spriteBatch.DrawString(userfont, "Login  ", new Vector2(userlogin.Rectangle.X + 30, userlogin.Rectangle.Y + 5), Color.Black);
            }
            else
            {
                map.Draw(_spriteBatch);
                _spriteBatch.DrawString(userfont, "Username: " + userValue, new Vector2(100, 1), Color.Black);
                _spriteBatch.DrawString(userfont, "Score:  " + score, new Vector2(1, 1), Color.Black);

                if (showBuildingLoadOut)
                {
                    for (int i = 0; i < menuArray.Length; i++)
                    {
                        menuArray[i].button.Draw(_spriteBatch);
                    }
                }

                if (ShowScoreBoard == true)
                {
                    for (int i = 0; i < highscorearray.Length; i++)
                    {
                        _spriteBatch.DrawString(userfont, highscorearray[i].user.ToString(), new Vector2(0, 380 + 20 * i), Color.Black);
                        _spriteBatch.DrawString(userfont, highscorearray[i].realm.ToString(), new Vector2(70, 380 + 20 * i), Color.Black);
                        _spriteBatch.DrawString(userfont, highscorearray[i].highscore.ToString(), new Vector2(152, 380 + 20 * i), Color.Black);
                    }
                    _spriteBatch.DrawString(userfont, "User:        Realm:         Highscore", new Vector2(0, 360), Color.Black);
                }
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}