using System;

namespace TowerDefenceEksamensProjekt
{
    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            using (var game = GameWorld.Instance)
                game.Run();
        }
    }
}