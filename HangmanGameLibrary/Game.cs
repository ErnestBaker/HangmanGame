using HangmanGameData;
using HangmanGameModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanGameLibrary
{
    public class Game
    {
        private Country Country { get; set; }
        private int Moves { get; set; }
        private int LifePoints { get; set; }
        public List<Letter> LettersInCapital { get; set; }


        public Game()
        {
            Country = GetRandomCountry();
            LifePoints = GetLifePoints();
            LettersInCapital = GetLettersInCapital();

            Moves = 0;

            SetAllLettersAsInvisible();
        }

        private Country GetRandomCountry()
        {
            List<Country> ListOfAllCountries = new List<Country>();
            ListOfAllCountries = ImportCountriesAndCapitals.GetCountries();
            Random random = new();

            return ListOfAllCountries[random.Next(ListOfAllCountries.Count)];
        }

        private int GetLifePoints()
        {
            return int.Parse(ConfigurationManager.AppSettings["LifePoints"]);
        }

        private List<Letter> GetLettersInCapital()
        {
            List<Letter> ListToReturn = new List<Letter>();
            Dictionary<char, int> LettersDictionary = new();

            foreach (var letter in Country.CapitalName.ToLower().ToArray())
            {
                if (LettersDictionary.ContainsKey(letter))
                {
                    LettersDictionary[letter] = 1;
                }
                else
                {
                    LettersDictionary.Add(letter, 1);
                }
            }

            foreach (var letter in LettersDictionary)
            {
                ListToReturn.Add(new Letter() { label = letter.Key.ToString(), RepeatCount = letter.Value });
            }
            return ListToReturn;
        }

        private void SetAllLettersAsInvisible()
        {
            foreach (var letter in LettersInCapital)
            {
                letter.IsVisible = false;
            }
        }

        public string GetWord()
        {
            string outputString = "";

            foreach (var letter in Country.CapitalName.ToLower().ToArray())
            {
                Letter LetterData = LettersInCapital.Where(x => x.label.Equals(letter.ToString())).First();
                if (LetterData.IsVisible)
                {
                    outputString += $" {LetterData.label} ";
                }
                else
                {
                    outputString += " _ ";
                }
            }
            return outputString;
        }

        public string ShowInitialMessage()
        {
            return $"Hi Player,{System.Environment.NewLine}Welcome to Hangman game. You have only {LifePoints} chances to guess a letter so be careful.{System.Environment.NewLine}GOOD LUCK!{System.Environment.NewLine}";
        }

        public int GetLeftLifePoints()
        {
            return LifePoints;
        }


    }
}
