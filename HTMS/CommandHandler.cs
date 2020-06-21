using Discord.Commands;
using Discord.WebSocket;
using System.Reflection;
using System.Threading.Tasks;

namespace HTMS
{
    internal class CommandHandler
    {
        private DiscordSocketClient client;
        private CommandService service;
        public CommandHandler(DiscordSocketClient client)
        {
            this.client = client;
            service = new CommandService();
            service.AddModulesAsync(Assembly.GetEntryAssembly(), null);

            client.MessageReceived += HandleCommandAsync;
        }

        private async Task HandleCommandAsync(SocketMessage s)
        {
            var msg = s as SocketUserMessage;
            if (msg == null)
                return;

            var context = new SocketCommandContext(client, msg);

            int argPos = 0;
            if(msg.HasCharPrefix( '!', ref argPos))
            {
                var result = await service.ExecuteAsync(context, argPos, null);

                if(!result.IsSuccess)
                {
                    await context.Channel.SendMessageAsync(result.ErrorReason);
                }
            }
            
        }
    }
}