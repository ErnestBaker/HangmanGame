using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using HangmanGameModels;

namespace HangmanGameData
{
    public static class ImportCountriesAndCapitals
    {
        public static List<Country> GetCountries()
        {
            List<Country> countries = new List<Country>();

            var mainDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());
            var FileStream = new FileStream(path: Path.Combine(mainDirectory.Parent.Parent.Parent.Parent.FullName, ConfigurationManager.AppSettings["CountriesAndCapitalsPath"]), FileMode.Open, FileAccess.Read);
            var StreamReader = new StreamReader(FileStream, Encoding.UTF8);
            string line;

            while ((line = StreamReader.ReadLine()) != null)
            {
                string[] splitLine;
                splitLine = line.Split("|");

                countries.Add(new Country { Name = splitLine[0].Trim(), CapitalName = splitLine[1].Trim() });
            }

            return countries;
        }    

	}
}
