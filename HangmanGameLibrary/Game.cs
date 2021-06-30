using HangmanGameData;
using HangmanGameModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanGameLibrary
{
    public class Game
    {
        private Country Country { get; set; }
        public int Moves { get; set; }
        public Game()
        {
            Country = GetRandomCountry();
        }

        private static Country GetRandomCountry()
        {
            List<Country> ListOfAllCountries = new List<Country>();
            ListOfAllCountries = ImportCountriesAndCapitals.GetCountries();
            Random random = new();

            return ListOfAllCountries[random.Next(ListOfAllCountries.Count)];
        }

        public Country ShowCounty()
        {
            return GetRandomCountry();
        }
    }
}
