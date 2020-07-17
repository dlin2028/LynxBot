using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
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
            client.UserJoined += AnnounceJoinedUser;
            client.GuildAvailable += GuildAvailable;
            
            await client.LoginAsync(TokenType.Bot, "NzI0MTgyODQyNjQ0NDk2NDY0.Xvl1vg.u2yn28sQ695HGSuF9WMlOY2UtRk");
            await client.StartAsync();
            await Task.Delay(-1);
        }

        private async Task GuildAvailable(SocketGuild arg)
        {
            DataManager.Invites = await arg.GetInvitesAsync();
        }

        private Task MsgRecieved(SocketMessage message)
        {
            Console.WriteLine(message.ToString());
            return Task.CompletedTask;
        }

        public async Task AnnounceJoinedUser(SocketGuildUser user) 
        {
            var newInvites = await user.Guild.GetInvitesAsync();
            if (DataManager.Invites != null)
            {
                foreach (var a in DataManager.Invites)
                {
                    foreach (var b in newInvites)
                    {
                        if(a.Id == b.Id && a.Uses != b.Uses)
                        {
                            var invite = DataManager.TrackedInvites.FirstOrDefault(x => x.userId == a.Inviter.Id);
                            if(invite == null)
                            {
                                DataManager.TrackedInvites.Add(new Invite(a.Inviter.Id, a.Id));
                                invite = DataManager.TrackedInvites.FirstOrDefault(x => x.userId == a.Inviter.Id);
                            }
                            invite.usersInvited.Add(user.Id);
                            await (client.GetChannel(DataManager.WelcomeChannel) as ISocketMessageChannel).SendMessageAsync($"{user.Mention} joined, invited by {a.Inviter.Mention} ({invite.usersInvited.Distinct().Count()} invites)");
                            return;
                        }
                    }
                }
            }
            DataManager.Invites = newInvites;

           await (client.GetChannel(DataManager.WelcomeChannel) as ISocketMessageChannel).SendMessageAsync($"{user.Mention} joined");
        }
        private Task Log(LogMessage message)
        {
            Console.WriteLine(message.ToString());
            return Task.CompletedTask;
        }
    }
}
