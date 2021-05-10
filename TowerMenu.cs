using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefenceEksamensProjekt
{
    public class TowerMenu : GameObject
    {
        public List<Button> buttons;
        public bool show = false;
        public List<Building> TowerList = new List<Building>();

        public TowerMenu()
        {
            TowerList.Add(new Building("Tower1"));
            TowerList.Add(new Building("Tower2"));
            buttons = new List<Button>();
            for (int i = 0; i < TowerList.Count; i++)
            {
                buttons.Add(new Button(new Rectangle((int)this.position.X, (int)this.position.Y + i * 30, 100, 30), TowerList[i].name, null));
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (show)
            {
                foreach (GameObject go in buttons)
                {
                    go.Draw(spriteBatch);
                }
            }
        }

        public void OpenMenu()
        {
            this.position = Mouse.GetState().Position.ToVector2();
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].Rectangle.X = (int)this.position.X;
                buttons[i].Rectangle.Y = (int)this.position.Y + i * 30;
            }
            show = true;
        }

        public override void Update(GameTime gameTime)
        {
            if (show)
            {
                foreach (GameObject go in buttons)
                {
                    go.Update(gameTime);
                }
            }
        }
    }
}