using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanGameModels
{
    public class Score : IScore
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public double GuessingTime { get; set; }
        public int GuessingTries { get; set; }
        public string GuessedWord { get; set; }
    }
}
