using ConsoleTables;
using HangmanGameLibrary;
using HangmanGameModels;
using System;
using System.Collections.Generic;

namespace HangmanGameUI
{
    class Program
    {
        public static Game Game { get; set; }

        static void Main(string[] args)
        {
            Console.WriteLine(ASCIDrawer.DrawASCIHangman(1));
            while (true)
            {
                StartNewGame();

                if (AskAboutNewGame())
                {
                    continue;
                }
                else
                {
                    break;
                }
            }
        }

        private static void StartNewGame()
        {
            Console.Clear();
            CreateNewGame();

            ShowMessage(Game.ShowInitialMessage());
            ShowWord(Game.GetWord(), Game.GetLeftLifePoints(), Game.GetLettersNotInWord());

            while (Game.PlayNextRound())
            {
                RoundType roundType = AskGuessLetterOfWholeWord();
                Game.NextGuess(roundType, GetAnswer(roundType));
                ShowWord(Game.GetWord(), Game.GetLeftLifePoints(), Game.GetLettersNotInWord());
            }

            Console.WriteLine(Game.GetResultMessage());

            if (Game.CheckToSaveResult())
            {
                Game.SaveScore(AskAboutName());
            }

            ShowTopScore();
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

        private static bool AskAboutNewGame()
        {
            Console.WriteLine($"Do you want to play again? (Y/N)");
            Console.Write("Type Y if yes: ");

            if (Console.ReadKey().KeyChar.ToString().ToUpper() == "Y")
            {
                return true;            
            }

            return false;
        }

        private static string AskAboutName()
        {
            Console.Write("Please enter your name to record the result: ");

            return Console.ReadLine();
        }

        private static void ShowTopScore()
        {
            List<Score> topScore= Game.GetTopScore();

            Console.WriteLine($"Top Score List:{Environment.NewLine}");

            ConsoleTable Table = new("Player Name", "Date", "Guessing time", "Guessing tries", "Guessed word");
            foreach (var record in topScore)
            {
                Table.AddRow(record.Name, record.Date.ToString("dd.MM.yyyy HH:mm"), record.GuessingTime, record.GuessingTries, record.GuessedWord);
            }

            Table.Write();
        }
    }
}
