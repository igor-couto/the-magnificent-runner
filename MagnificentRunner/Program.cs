using System;

namespace MagnificentRunner
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using var game = new MyGame();
            game.Run();
        }
    }
}