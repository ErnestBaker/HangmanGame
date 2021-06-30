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
    static class ImportCountriesAndCapitals
    {
        private static Country[] GetCountries()
        {
            Country[] countries = { };

            var FileStream = new FileStream(path: ConfigurationManager.AppSettings["CountriesAndCapitalsPath"], FileMode.Open, FileAccess.Read);
            var StreamReader = new StreamReader(FileStream, Encoding.UTF8);
            string line;

            while ((line = StreamReader.ReadLine()) != null)
            {
                string[] splitLine;
                splitLine = line.Split("|");

                countries.Append(new Country { Name = splitLine[0], CapitalName = splitLine[1] });
            }

            return countries;
        }    

	}
}
