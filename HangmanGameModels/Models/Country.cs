using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HangmanGameModels;

namespace HangmanGameModels
{
    public class Country : ICountry
    {
        public string Name { get; set; }
        public string CapitalName { get; set; }
    }
}
