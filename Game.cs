using System;
using System.Collections.Generic;
using System.Text;

namespace discordBot
{
    class Game
    {
        enum Result
        {
            SUCCESS,
            DUPLICATED_WORD,
            INVALID_CHAIN
        }
        public Bot bot;
        bool startEnd;
        public List<string> usedWords;
        string lastTyped;

        public Game()
        {
            this.bot = new Bot();
            this.startEnd = false;
            this.usedWords = new List<string>();
            this.lastTyped = null;
        }

        public bool StartEnd
        {
            get => startEnd;
        }

        public string StartGame()
        {
            startEnd = true;
            return "Start Shiritori\n" +
                "Enter 'GameEnd' if you want to exit";
        }

        public string EndGame()
        {
            startEnd = false;
            usedWords.Clear();
            lastTyped = null;
            return "End Shiritori\n" +
                "Enter 'GameStart' if you want to start";
        }

        public string PlayGame(string current)
        {
            if (lastTyped == null)
            {
                lastTyped = ShiritoriBot(current, bot);
                return lastTyped;
            }
            else
            {
                Result userCheck = ShiritoriUser(lastTyped, current);
                switch(userCheck)
                {
                    case Result.SUCCESS :
                        lastTyped = ShiritoriBot(current, bot);
                        if(lastTyped == null)
                        {
                            startEnd = false;
                            usedWords.Clear();
                            lastTyped = null;
                            return "you win\n" +
                                "Enter 'GameStart' if you want to start";
                        }
                        return lastTyped;
                    case Result.DUPLICATED_WORD :
                        return "you entered duplicated word\n" +
                            "Please enter another word";
                    case Result.INVALID_CHAIN :
                        return "you entered an invalid word\n" +
                            "Please enter another word";

                }
            }
            return null;
        }

        string ShiritoriBot(string user, Bot bot)
        {
            Console.WriteLine("ShiritoriBot");
            string botWord = bot.Words.Find(element =>
            {
                if (usedWords.Find(uw => uw == element) == null)
                {
                    char[] botStr = element.ToCharArray();
                    char[] userStr = user.ToCharArray();
                    bool next = botStr[0] == userStr[userStr.Length - 1];
                    return (element.Length >= 2) && (next == true);
                }
                return false;
            });
            usedWords.Add(botWord);
            return botWord;
        }

        Result ShiritoriUser(string last, string current)
        {
            Console.WriteLine("ShiritoriUser");
            char[] lastStr = last.ToCharArray();
            char[] userStr = current.ToCharArray();

            if (lastStr[lastStr.Length - 1] != userStr[0])
            {
                return Result.INVALID_CHAIN;
            }

            if (usedWords.Find(uw => uw == current) != null)
            {
                return Result.DUPLICATED_WORD;
            }
            usedWords.Add(current);
            return Result.SUCCESS;
        }
    }
}
