using ConsoleTables;
using HangmanGameLibrary;
using HangmanGameModels;
using System;

namespace HangmanGameUI
{
    class Program
    {
        public static Game Game { get; set; }

        static void Main(string[] args)
        {
            CreateNewGame();

            ShowMessage(Game.ShowInitialMessage());
            ShowWord(Game.GetWord(), Game.GetLeftLifePoints(), Game.GetLettersNotInWord());

            while (Game.PlayNextRound())
            {
                RoundType roundType = AskGuessLetterOfWholeWord();
                Game.NextGuess(roundType, GetAnswer(roundType));
                ShowWord(Game.GetWord(), Game.GetLeftLifePoints(), Game.GetLettersNotInWord());
            }
        }

        private static void ShowWord(string word, int lifePoints, string lettersNotInWord)
        {
            ConsoleTable Table = new("Word To Guess", "Life Points", "Letters not in word");
            Table.AddRow(word, lifePoints, lettersNotInWord);
            Table.Options.EnableCount = false;
            Table.Write();
        }

        private static void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        private static void CreateNewGame()
        {
            Game = new();
        }

        private static RoundType AskGuessLetterOfWholeWord()
        {
            ConsoleKeyInfo input;
            
            while(true)
            {
                Console.WriteLine("Do you want to guess a letter [1] or a whole word [2]?");
                Console.Write("Type 1 or 2: ");

                input = Console.ReadKey();
                Console.WriteLine($"{Environment.NewLine}");

                if (input.KeyChar.ToString() == "1")
                {
                    return RoundType.OneLetter;
                }
                else if (input.KeyChar.ToString() == "2")
                {
                    return RoundType.WholeWord;
                }
                else
                {
                    Console.WriteLine($"The character you entered is invalid. Please try again.{Environment.NewLine}");
                }
            }
        }

        private static string GetAnswer(RoundType roundType)
        {
            string valueToReturn = "";

            if (roundType == RoundType.OneLetter)
            {
                Console.Write("Type you letter: ");
                valueToReturn = Console.ReadKey().KeyChar.ToString();
            }
            else
            {
                Console.Write("Type you word: ");
                valueToReturn = Console.ReadLine().ToString();
            }

            Console.WriteLine($"{Environment.NewLine}");

            return valueToReturn;
        }

    }
}
