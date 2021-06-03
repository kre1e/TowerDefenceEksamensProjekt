﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using TowerDefenceEksamensProjekt.FactoryPattern;

namespace TowerDefenceEksamensProjekt.Levels
{
    public class GameLevel : Level
    {
        public Map currentmap;
        public TowerMenu towerMenu;
        private bool ShowScoreBoard = false;
        public bool release = true;
        private int score;
        public static Vector2 BananaFarm;
        public static Vector2 Base;

        //private Color color;
        //public Color MyColor
        //{
        //    get { return myColor; }
        //    set { myColor = value; }
        //}

        AStar aStar;

        private List<Node> finalPath;

        public void FindPath()
        {
            finalPath = aStar.FindPath(BananaFarm, Base, CreateNodes());


        }

        public GameLevel(Map currentmap)
        {
            this.currentmap = currentmap;
            towerMenu = new TowerMenu();
        }

        public List<Node> CreateNodes()
        {
            List<Node> allNodes = new List<Node>();

            foreach (Tile tile in currentmap.Tiles)
            {
                if (tile.WalkAble)
                {
                    tile.MyNode = new Node(tile.position);
                    allNodes.Add(tile.MyNode);
                }
            }
            return allNodes;
        }
        public override void Draw(SpriteBatch _spriteBatch)
        {
            currentmap.Draw(_spriteBatch);
            _spriteBatch.DrawString(GameWorld.userfont, "Username: " + GameWorld.currentPlayer, new Vector2(100, 1), Color.Black);
            _spriteBatch.DrawString(GameWorld.userfont, "Score:  " + score, new Vector2(1, 1), Color.Black);
            towerMenu.Draw(_spriteBatch);

            if (ShowScoreBoard == true)
            {
                for (int i = 0; i < GameWorld.highscorearray.Length; i++)
                {
                    _spriteBatch.DrawString(GameWorld.userfont, GameWorld.highscorearray[i].user.ToString(), new Vector2(0, 380 + 20 * i), Color.Black);
                    _spriteBatch.DrawString(GameWorld.userfont, GameWorld.highscorearray[i].realm.ToString(), new Vector2(70, 380 + 20 * i), Color.Black);
                    _spriteBatch.DrawString(GameWorld.userfont, GameWorld.highscorearray[i].highscore.ToString(), new Vector2(152, 380 + 20 * i), Color.Black);
                }
                _spriteBatch.DrawString(GameWorld.userfont, "User:        Realm:         Highscore", new Vector2(0, 360), Color.Black);
            }
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState currentKeyState = GameWorld.currentKeyState;
            MouseState mouseState = Mouse.GetState();
            if (currentKeyState.IsKeyDown(Keys.Tab))
                ShowScoreBoard = true;
            else
                ShowScoreBoard = false;

            foreach (Tile n in currentmap.Tiles)
            {
                if (n.Rectangle.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed && release)
                {
                    release = false;
                    if (n.ContainTower != null)
                    {
                        //Tower upgrade
                    }
                    else
                    {
                        if (towerMenu.show && !towerMenu.rectangle.Contains(mouseState.Position))
                            towerMenu.show = false;
                        else if (!towerMenu.rectangle.Contains(mouseState.Position))
                            towerMenu.OpenMenu(n);
                    }
                }
                else if (mouseState.LeftButton == ButtonState.Released)
                {
                    release = true;
                }
            }
            
            towerMenu.Update(gameTime);
            currentmap.Update(gameTime);
        }
    }
}