using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTMS.Modules
{
    public class Administrative : ModuleBase<SocketCommandContext>
    {
        [Command("test")]
        public async Task test()
        {
            var user = Context.User as SocketGuildUser;
            if (!user.GuildPermissions.Administrator)
            {
                await Context.Channel.SendMessageAsync("You must be admin to do this");
                return;
            }

            await Context.Channel.SendMessageAsync(Context.Guild.GetChannel(DataManager.LandingChannel).ToString());
            await Context.Channel.SendMessageAsync(Context.Guild.GetChannel(DataManager.WelcomeChannel).ToString());
        }
        [Command("setwelcome")]
        public async Task SetWelcome(SocketChannel channel)
        {
            var user = Context.User as SocketGuildUser;
            if (!user.GuildPermissions.Administrator)
            {
                await Context.Channel.SendMessageAsync("You must be admin to do this");
                return;
            }

            DataManager.WelcomeChannel = channel.Id;
            await Context.Channel.SendMessageAsync("Successfully set welcome channel to " + (Context.Guild.GetChannel(DataManager.WelcomeChannel) as SocketTextChannel).Mention);
        }
        [Command("setwelcome")]
        public async Task SetWelcome()
        {
            await SetWelcome(Context.Channel as SocketChannel);
        }

        [Command("setlanding")]
        public async Task SetLanding(SocketChannel channel)
        {
            var user = Context.User as SocketGuildUser;
            if (!user.GuildPermissions.Administrator)
            {
                await Context.Channel.SendMessageAsync("You must be admin to do this");
                return;
            }

            DataManager.LandingChannel = channel.Id;
            await Context.Channel.SendMessageAsync("Successfully set landing channel to " + (Context.Guild.GetChannel(DataManager.LandingChannel) as SocketTextChannel).Mention);
        }
        [Command("setlanding")]
        public async Task SetLanding()
        {
            await SetLanding(Context.Channel as SocketChannel);
        }


        [Command("ban"), Alias("ban"), Summary("Bans a user from the server")]
        public async Task Ban(SocketGuildUser userAccount, string reason)
        {
            var user = Context.User as SocketGuildUser;
            var role = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == "Admin");
            if (!userAccount.Roles.Contains(role))
            {
                if (user.GuildPermissions.BanMembers)
                {
                    await userAccount.BanAsync(0, reason, null);
                    await Context.Channel.SendMessageAsync($" `{userAccount}` has been banned, for {reason}");
                }
                else
                {
                    await Context.Channel.SendMessageAsync("No permissions for banning a user.");
                }
            }
            else
            {
                await Context.Channel.SendMessageAsync("This User can't be banned, because the user has a admin role.");
            }
        }
        [Command("kick"), Alias("Kick"), Summary("Kicks a user from the server")]
        public async Task Kick(SocketGuildUser userAccount, string reason)
        {
            var user = Context.User as SocketGuildUser;
            var role = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == "Admin");
            if (!userAccount.Roles.Contains(role))
            {
                if (user.GuildPermissions.KickMembers)
                {
                    await userAccount.KickAsync(reason);
                    await Context.Channel.SendMessageAsync($" `{userAccount}` has been kicked, for {reason}");
                }
                else
                {
                    await Context.Channel.SendMessageAsync("No permissions for kicking a user.");
                }
            }
            else
            {
                await Context.Channel.SendMessageAsync("This User can't be kicked, because the user has a admin role.");
            }
        }
    }
}
