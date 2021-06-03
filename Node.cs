using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenceEksamensProjekt
{
    public class Node : IComparable<Node>
    {
        private int f;

        public int F
        {
            get { return f; }
            set { f = value; }
        }

        private int g;

        private int h;

        public int H
        {
            get { return h; }
            set { h = value; }
        }

        Vector2 position;

        Node parent;


        public Node Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public int G
        {
            get { return g; }
            set { g = value; }
        }

        public Node(Vector2 position)
        {
            this.position = position;
        }

        public void CalcValues(Node ParentNode, Node goalNode, int cost)
        {
            parent = ParentNode;
            g = ParentNode.G + cost;
            h = (int)((Math.Abs(position.X - goalNode.position.X) + Math.Abs(goalNode.position.Y - position.Y)) * 10);
            f = h + g;
        }

        public int CompareTo(Node other)
        {
            if (f > other.f)
            {
                return 1;
            }
            else if (f < other.f)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
}
