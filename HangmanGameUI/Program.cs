using ConsoleTables;
using HangmanGameLibrary;
using System;

namespace HangmanGameUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new();

            ShowMessage(game.ShowInitialMessage());
            ShowWord(game.GetWord(), game.GetLeftLifePoints());
        }

        private static void ShowWord(string word, int lifePoints)
        {
            ConsoleTable Table = new("Word To Guess", "Life Points");
            Table.AddRow(word, lifePoints);
            Table.Options.EnableCount = false;
            Table.Options.NumberAlignment = Alignment.Right;
            Table.Write();
        }

        private static void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

    }
}
