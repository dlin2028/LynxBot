using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace HTMS
{
    class Program
    {
        static void Main(string[] args)
            => new Program().StartAsync().GetAwaiter().GetResult();

        private DiscordSocketClient client;

        private async Task StartAsync()
        {
            client = new DiscordSocketClient();

            new CommandHandler(client);

            client.Log += Log;
            client.MessageReceived += MsgRecieved;

            await client.LoginAsync(TokenType.Bot, "NzI0MTgyODQyNjQ0NDk2NDY0.Xu8pug.8-gWy76J6aAUBp3x6wmj3TiSWoQ");
            await client.StartAsync();
            await Task.Delay(-1);
        }
        private Task MsgRecieved(SocketMessage message)
        {
            Console.WriteLine(message.ToString());
            return Task.CompletedTask;
        }
        private Task Log(LogMessage message)
        {
            Console.WriteLine(message.ToString());
            return Task.CompletedTask;
        }
    }
}
