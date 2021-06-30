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



            ShowCountryAndCapital(game);
            Console.ReadLine();
        }

        private static void ShowCountryAndCapital(Game game)
        {
            var table = new ConsoleTable("Country", "Capital");
            table.AddRow(game.ShowCounty().Name, game.ShowCounty().CapitalName);

            table.Write();
        }
    }
}
