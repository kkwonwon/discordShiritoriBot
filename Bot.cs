using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace discordBot
{
    class Bot
    {
        private readonly List<string> words;

        public Bot()
        {
            words = fileToWords();
        }

        public List<string> Words
        {
            get => words;
        }

        public static List<string> fileToWords()
        {
            List<string> words = new List<string>();
            StreamReader file = new StreamReader(@"D:\Coding\mentoring\discordBot\words.csv");
            string[] lines = file.ReadToEnd().Split("\n");
            foreach (string line in lines)
            {
                string word = line.Split(",")[1];
                words.Add(word);
            }
            file.Close();
            return words;
        }
    }


}
