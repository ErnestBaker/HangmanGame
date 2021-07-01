using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanGameModels
{
    public class Letter : ILetter
    {
        public string label { get; set; }
        public int RepeatCount { get; set; }
        public bool IsVisible { get; set; }
    }
}
