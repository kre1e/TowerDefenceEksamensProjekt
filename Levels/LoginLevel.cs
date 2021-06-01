using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefenceEksamensProjekt.Levels
{
    public class LoginLevel : Level
    {
        public UserInput activeUserInput;
        public UserInput userLogin;
        public UserInput passLogin;
        public SpriteFont userfont;

        public LoginLevel()
        {
            gameObjects = new List<GameObject>();
            userfont = GameWorld.content.Load<SpriteFont>("File");
            userLogin = new UserInput(new Rectangle(880, 450, 100, 30), this, "Username: ");
            passLogin = new UserInput(new Rectangle(880, 500, 100, 30), this, "Password: ");
            gameObjects.Add(userLogin);
            gameObjects.Add(passLogin);

            userLogin.layerdef = 0.98f;
            passLogin.layerdef = 0.98f;
            Button button = new Button(new Rectangle(880, 550, 100, 30), "Login: ", Login);
            button.layerdef = 0.90f;
            gameObjects.Add(button);
        }

        public void Login()
        {
            if (Database.Userlogin(userLogin.currentInput, passLogin.currentInput))
            {
                Map map = new Map();
                GameWorld.currentPlayer = userLogin.currentInput;
                GameWorld.currrentLevel = new GameLevel(Realm.RandomMap(map));
            }
        }

        public override void Update(GameTime gameTime)
        {
            MouseState mouse = Mouse.GetState();
            if (Mouse.GetState().LeftButton == ButtonState.Pressed && !userLogin.Rectangle.Contains(mouse.Position) && !passLogin.Rectangle.Contains(mouse.Position))
                activeUserInput = null;
            foreach (GameObject obj in gameObjects)
            {
                obj.Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (GameObject obj in gameObjects)
            {
                obj.Draw(spriteBatch);
            }
        }
    }
}