using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Discord;
using Discord.WebSocket;

namespace discordBot
{
    

    class Program
    {
        private DiscordSocketClient _client;

        static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            var _config = new DiscordSocketConfig
            {
                GatewayIntents = GatewayIntents.All
            };
            _client = new DiscordSocketClient(_config);

            await _client.LoginAsync(TokenType.Bot, "MTEwNTA4ODQzNTgyMTIzMjE1OA.GjvsIM.S8Y7npLy7Mqx1wHRvHXaqJugMwOXdYn6zz69pE");
            await _client.StartAsync();

            _client.MessageUpdated += MessageUpdated;
            _client.MessageReceived += MessageReceived;
            _client.Ready += () =>
            {
                Console.WriteLine("Bot is connected!");
                return Task.CompletedTask;
            };

            await Task.Delay(-1);
        }

        bool startBot = true;
        Game game = new Game();

        private Task MessageReceived(SocketMessage arg)
        {
            if (startBot)
            {
                startBot = false;
                arg.Channel.SendMessageAsync("Enter 'GameStart' if you want to start");
            }

            string userMessage = arg.Content;

            if ((!arg.Author.IsBot) && (userMessage == "GameStart"))
            {
                arg.Channel.SendMessageAsync(game.StartGame());
            }
            if ((!arg.Author.IsBot) && (userMessage == "GameEnd"))
            {
                arg.Channel.SendMessageAsync(game.EndGame());
            }
            if ((!arg.Author.IsBot) && game.StartEnd && !(userMessage == "GameStart"))
            {
                arg.Channel.SendMessageAsync(game.PlayGame(userMessage));
            }

            return Task.CompletedTask;
        }

        private async Task MessageUpdated(Cacheable<IMessage, ulong> before, SocketMessage after, ISocketMessageChannel channel)
        {
            var message = await before.GetOrDownloadAsync();
            Console.WriteLine($"{message} -> {after}");
        }
    }
}
