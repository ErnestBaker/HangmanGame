using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HangmanGameModels;

namespace HangmanGameData
{
    public static class ScoreData
    {
        public static void SaveScoreToFile(string score)
        {

            var mainDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());
            TextWriter textWriter = new StreamWriter(path: Path.Combine(mainDirectory.Parent.Parent.Parent.Parent.FullName, ConfigurationManager.AppSettings["ScoreFilePath"]), append: true);
            textWriter.WriteLine(score);
            textWriter.Close();
        }

        public static List<Score> GetAllScores()
        {
            List<Score> scores = new List<Score>();

            var mainDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());
            var FileStream = new FileStream(path: Path.Combine(mainDirectory.Parent.Parent.Parent.Parent.FullName, ConfigurationManager.AppSettings["ScoreFilePath"]), FileMode.Open, FileAccess.Read);
            var StreamReader = new StreamReader(FileStream, Encoding.UTF8);
            string line;

            while ((line = StreamReader.ReadLine()) != null)
            {
                string[] splitLine;
                splitLine = line.Split(" | ");

                DateTime datetime = DateTime.Parse(splitLine[1]);
                scores.Add(new Score { Name = splitLine[0], Date = datetime , GuessingTime = double.Parse(splitLine[2]), GuessingTries = int.Parse(splitLine[3]), GuessedWord = splitLine[4] });
            }

            StreamReader.Close();

            return scores;
        }
    }
}
