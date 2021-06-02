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
    class AStar
    {
        private List<Node> closed;
        private List<Node> open;
        public List<Node> Closed
        {
            get { return closed; }
            set { closed = value; }
        }

        public List<Node> Open
        {
            get { return open; }
            set { open = value; }
        }

        public AStar()
        {
            closed = new List<Node>();
            open = new List<Node>();
        }

        public List<Node> FindPath(Point myStart, Point myGoal, List<Node> nodes)
        {
            Point start = myStart;
            Point goal = myGoal;

            List<Node> finalPath = new List<Node>();

            open.Clear();
            Node currentNode = nodes.Find(x => x.Position == start);
            open.Add(currentNode);

            while (true)
            {
                for (int x = -1; x <= 1; ++x)
                {
                    for (int y = -1; y <= 1; ++y)
                    {
                        if (x != 0 || y != 0)
                        {
                            Node neighbour = nodes.Find(node => node.Position.X == currentNode.Position.X - x && node.Position.Y == currentNode.Position.Y - y);

                            if (neighbour != null)
                            {
                                int gCost = 0;

                                if (Math.Abs(x - y) % 2 == 1)
                                {
                                    gCost = 10;
                                }
                                else
                                {
                                    gCost = 14;
                                }

                                if (open.Exists(n => n == neighbour))
                                {
                                    if (currentNode.G + gCost < neighbour.G)
                                    {
                                        neighbour.CalcValues(currentNode, nodes.Find(node => node.Position == goal), gCost);
                                    }

                                }
                                else if (!closed.Exists(n => n == neighbour))
                                {

                                    if (gCost == 14)
                                    {
                                        if (currentNode.Position.X < neighbour.Position.X && currentNode.Position.Y > neighbour.Position.Y) //Topright
                                        {
                                            if (nodes.Exists(node => node.Position == new Point(currentNode.Position.X, currentNode.Position.Y - 1) && nodes.Exists(node2 => node2.Position == new Point(currentNode.Position.X + 1, currentNode.Position.Y))))
                                            {
                                                open.Add(neighbour);
                                                open.Remove(currentNode);
                                                closed.Add(currentNode);
                                                neighbour.CalcValues(currentNode, nodes.Find(node => node.Position == goal), gCost);
                                            }
                                        }
                                        else if (currentNode.Position.X > neighbour.Position.X && currentNode.Position.Y < neighbour.Position.Y) //Bottomleft
                                        {
                                            if (nodes.Exists(node => node.Position == new Point(currentNode.Position.X, currentNode.Position.Y + 1) && nodes.Exists(node2 => node2.Position == new Point(currentNode.Position.X - 1, currentNode.Position.Y))))
                                            {
                                                open.Add(neighbour);
                                                open.Remove(currentNode);
                                                closed.Add(currentNode);
                                                neighbour.CalcValues(currentNode, nodes.Find(node => node.Position == goal), gCost);
                                            }
                                        }
                                        else if (currentNode.Position.X > neighbour.Position.X && currentNode.Position.Y > neighbour.Position.Y) //Topleft
                                        {
                                            if (nodes.Exists(node => node.Position == new Point(currentNode.Position.X, currentNode.Position.Y - 1) && nodes.Exists(node2 => node2.Position == new Point(currentNode.Position.X - 1, currentNode.Position.Y))))
                                            {
                                                open.Add(neighbour);
                                                open.Remove(currentNode);
                                                closed.Add(currentNode);
                                                neighbour.CalcValues(currentNode, nodes.Find(node => node.Position == goal), gCost);
                                            }
                                        }
                                        else if (currentNode.Position.X < neighbour.Position.X && currentNode.Position.Y < neighbour.Position.Y) //Bottomright
                                        {
                                            if (nodes.Exists(node => node.Position == new Point(currentNode.Position.X + 1, currentNode.Position.Y) && nodes.Exists(node2 => node2.Position == new Point(currentNode.Position.X, currentNode.Position.Y + 1))))
                                            {
                                                open.Add(neighbour);
                                                open.Remove(currentNode);
                                                closed.Add(currentNode);
                                                neighbour.CalcValues(currentNode, nodes.Find(node => node.Position == goal), gCost);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        open.Add(neighbour);
                                        open.Remove(currentNode);
                                        closed.Add(currentNode);
                                        neighbour.CalcValues(currentNode, nodes.Find(node => node.Position == goal), gCost);
                                    }
                                }
                            }

                        }
                    }
                }

                if (open.Count == 0)
                {
                    break;
                }

                open.Sort();

                currentNode = open[0];
                open.Remove(currentNode);
                closed.Add(currentNode);

                if (currentNode.Position == goal)
                {
                    closed.Add(currentNode);
                    break;
                }
            }

            Node tmpNode = closed.Find(x => x.Position == goal);

            if (tmpNode != null)
            {
                while (!finalPath.Exists(x => x.Position == start))
                {
                    finalPath.Add(tmpNode);
                    tmpNode = tmpNode.Parent;
                }
            }

            return finalPath;
        }


    }




    }

