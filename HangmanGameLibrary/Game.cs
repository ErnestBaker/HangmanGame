using HangmanGameData;
using HangmanGameModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanGameLibrary
{
    public class Game
    {
        public Game()
        {
            Country = GetRandomCountry();
            LifePoints = GetLifePoints();
            LettersInCapital = GetLettersInCapital();

            Moves = 0;
            LettersNotInWord = new();

            SetAllLettersAsInvisible();
            StartCountingTime();
        }

        private Country Country { get; set; }
        private int Moves { get; set; }
        private int LifePoints { get; set; }
        private List<Letter> LettersInCapital { get; set; }
        private List<String> LettersNotInWord { get; set; }
        public Stopwatch GuessingTime { get; set; }

        public string GetWord()
        {
            string outputString = "";

            foreach (var letter in Country.CapitalName.ToLower().ToArray())
            {
                Letter LetterData = LettersInCapital.Where(x => x.label.Equals(letter.ToString())).First();
                if (LetterData.IsVisible)
                {
                    outputString += $" {LetterData.label.ToUpper()} ";
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
            return $"Hi Player,{System.Environment.NewLine}Welcome to Hangman game. You have only {LifePoints} chances to guess a letter so be careful.{System.Environment.NewLine}Try to guess the capital of the country.{System.Environment.NewLine}GOOD LUCK!{System.Environment.NewLine}";
        }

        public int GetLeftLifePoints()
        {
            return LifePoints;
        }

        public bool PlayNextRound()
        {
            if (CheckIfAllLettersAreVisible())
            {
                return false;
            }
            else if (LifePoints <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void NextGuess(RoundType roundType, string text)
        {
            if (roundType == RoundType.OneLetter)
            {
                if (LettersInCapital.Any(x => x.label.Equals(text.ToString())))
                {
                    int letterIndex = LettersInCapital.FindIndex(x => x.label.Equals(text.ToString().ToLower()));
                    LettersInCapital[letterIndex].IsVisible = true;
                }
                else
                {
                    LifePoints -= 1;
                    LettersNotInWord.Add(text);
                }

                Moves++;
            }
            else if (roundType == RoundType.WholeWord)
            {
                if (text.ToLower() == this.Country.CapitalName.ToLower())
                {
                    foreach (var letter in LettersInCapital)
                    {
                        letter.IsVisible = true;
                    }
                }
                else
                {
                    LifePoints -= 2;
                }
            }
        }

        public bool CheckToSaveResult()
        {
            if (CheckIfAllLettersAreVisible())
            {
                return true;
            }

            return false;
        }

        public void SaveScore(string name)
        {
            List<string> actualScore = new() { name, DateTime.UtcNow.ToString("dd.MM.yyyy HH:mm"), GuessingTime.Elapsed.TotalSeconds.ToString(), Moves.ToString(), this.Country.CapitalName };

            ScoreData.SaveScoreToFile(String.Join(" | ", actualScore.ToArray()));
        }

        public string GetResultMessage()
        {
            StopCountingTime();

            if (CheckIfAllLettersAreVisible())
            {
                return $"CONGRATULATIONS!!!{Environment.NewLine}You win!{Environment.NewLine}You guessed the capital after {Moves} letters. It took you {GuessingTime.Elapsed.TotalSeconds} seconds.{ Environment.NewLine}{this.Country.CapitalName} is the capital of {this.Country.Name}.";
            }
            else
            {
                return $"YOU LOSE :({Environment.NewLine}Guessing word is capital of {this.Country.Name}{Environment.NewLine}Try agin. Maybe you'll have better luck next time";
            }
        }

        public List<Score> GetTopScore()
        {
            List<Score> scores = ScoreData.GetAllScores();

            scores = scores.OrderBy(x => x.GuessingTime).Take(10).ToList();

            return scores;
        }

        public string GetLettersNotInWord()
        {
            String stringToReturn = String.Empty;
            stringToReturn = String.Join(",", LettersNotInWord);

            return stringToReturn.ToUpper();
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
                ListToReturn.Add(new Letter() { label = letter.Key.ToString(), RepeatCount = letter.Value});
            }
            return ListToReturn;
        }

        private void SetAllLettersAsInvisible()
        {
            foreach (var letter in LettersInCapital)
            {
                if (letter.label.ToString() == " ")
                {
                    letter.IsVisible = true;
                }
                else
                {
                    letter.IsVisible = false;
                }
            }
        }

        private bool CheckIfAllLettersAreVisible()
        {
            foreach (var letter in LettersInCapital)
            {
                if (!letter.IsVisible)
                {
                    return false;
                }
            }
            return true;
        }


        private void StartCountingTime()
        {
            GuessingTime = new();
            GuessingTime.Start();
        }

        private void StopCountingTime()
        {
            GuessingTime.Stop();
        }

    }
}
