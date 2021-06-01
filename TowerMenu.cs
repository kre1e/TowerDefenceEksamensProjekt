using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
        public Tile n;
        public List<Building> TowerList = new List<Building>();
        public Rectangle rectangle;

        public TowerMenu()
        {
            TowerList.Add(new Building("Base"));
            TowerList.Add(new Building("Tower"));
            TowerList.Add(new Building("FlameTower"));
            TowerList.Add(new Building("Wall"));
            TowerList.Add(new Building("BananaFarm"));

            buttons = new List<Button>();
            for (int i = 0; i < TowerList.Count; i++)
            {
                buttons.Add(new Button(new Rectangle((int)this.position.X, (int)this.position.Y + i * 35, 130, 35), TowerList[i].name, TowerList[i].SetBuilding));
                buttons[i].layerdef = 0.2f;
            }

            rectangle = new Rectangle((int)this.position.X, (int)this.position.Y, 100, 30 * buttons.Count);
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

        public void OpenMenu(Tile n)
        {
            this.n = n;
            this.position = Mouse.GetState().Position.ToVector2() + new Vector2(5, 5);
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].Rectangle.X = (int)this.position.X;
                buttons[i].Rectangle.Y = (int)this.position.Y + i * 30;
            }
            show = true;
            rectangle.X = (int)this.position.X;
            rectangle.Y = (int)this.position.Y;
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