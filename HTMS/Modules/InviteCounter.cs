using Discord;
using Discord.Commands;
using Discord.WebSocket;
using F23.StringSimilarity;
using Newtonsoft.Json.Linq;
using org.mariuszgromada.math.mxparser;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace HTMS.Modules
{
    public class InviteCounter : ModuleBase<SocketCommandContext>
    {
        [Command("create")]
        public async Task createInvite(string hi)
        {
            if (hi != "invite")
                return;

            IUser user = Context.User as IUser;

            if(DataManager.LandingChannel == 0)
            {
                await Context.Channel.SendMessageAsync(@"You must specify a landing channel first");
                return;
            }
            
            ITextChannel chnl = Context.Guild.GetChannel(DataManager.LandingChannel) as ITextChannel;
            IInvite invite = await chnl.CreateInviteAsync(maxAge: null);
            await Context.Channel.SendMessageAsync(
@"ᴡᴇʟᴄᴏᴍᴇ ᴛᴏ Lynx Modz Community
ʜᴇʀᴇ ɪꜱ ᴡʜᴀᴛ ᴏᴜʀ ꜱᴇʀᴠᴇʀ ʜᴀꜱ ᴛᴏ ᴏꜰꜰᴇʀ:

:sparkles:𝐆𝐚𝐦𝐢𝐧𝐠 𝐂𝐨𝐦𝐦𝐮𝐧𝐢𝐭𝐲
:sparkles:𝐃𝐚𝐢𝐥𝐲 𝐅𝐫𝐞𝐞 𝐆𝐭𝐚 𝐌𝐨𝐧𝐞𝐲 𝐃𝐫𝐨𝐩𝐬
:sparkles:𝐀𝐜𝐭𝐢𝐯𝐞 𝐎𝐰𝐧𝐞𝐫 𝐚𝐧𝐝 𝐒𝐭𝐚𝐟𝐟 
:sparkles:𝐂𝐡𝐞𝐚𝐩 𝐑𝐞𝐜𝐨𝐯𝐞𝐫𝐲𝐬
:sparkles:𝐅𝐚𝐬𝐭 𝐀𝐧𝐝 𝐒𝐞𝐜𝐮𝐫𝐞 
:sparkles:𝐅𝐫𝐞𝐪𝐮𝐞𝐧𝐭 𝐆𝐢𝐯𝐞𝐚𝐰𝐚𝐲𝐬
:sparkles:𝐒𝐢𝐦𝐩𝐥𝐞 𝐑𝐮𝐥𝐞𝐬
:sparkles:𝐏𝐫𝐨𝐨𝐟 𝐎𝐟 𝐎𝐮𝐫 𝐖𝐨𝐫𝐤
:sparkles:𝐆𝐓𝐀 𝐌𝐨𝐧𝐞𝐲 𝐟𝐨𝐫 𝐈𝐧𝐯𝐢𝐭𝐞𝐬 ");
            if (DataManager.InviteMap.ContainsKey(user.Id.ToString()))
            {
                var inviteId = DataManager.InviteMap[Context.User.Id.ToString()];
                var thing = await chnl.GetInvitesAsync();
                var realInvite = thing.Where(x => x.Id == inviteId).FirstOrDefault();
                await Context.Channel.SendMessageAsync(realInvite.Url);
            }
            else
            {
                DataManager.InviteMap.Add(user.Id.ToString(), invite.Id);
                await Context.Channel.SendMessageAsync(invite.Url);
            }
            await Context.Channel.SendMessageAsync(@"https://tenor.com/view/line-rainbow-bar-gif-14589887");
        }
        [Command("invites")]
        public async Task getInvites()
        {
            if(DataManager.InviteMap.ContainsKey(Context.User.Id.ToString()))
            {
                var inviteId = DataManager.InviteMap[Context.User.Id.ToString()];
                ITextChannel chnl = Context.Client.GetChannel(Context.Channel.Id) as ITextChannel;
                var thing = await chnl.GetInvitesAsync();
                var realInvite = thing.Where(x => x.Id == inviteId).FirstOrDefault();
                await Context.Channel.SendMessageAsync($" {(Context.User as SocketGuildUser).Mention} , you have {realInvite.Uses} invites! ");


                var inv = DataManager.TrackedInvites.FirstOrDefault(x => x.userId == Context.User.Id);
                if(inv != null)
                {
                    await Context.Channel.SendMessageAsync("------Users Invited-----");
                    foreach (var user in inv.usersInvited)
                    {
                        await Context.Channel.SendMessageAsync(Context.Guild.GetUser(user).Nickname);
                    }
                }
            }
            else
            {
                await Context.Channel.SendMessageAsync("You haven't created an invite");
            }
        }
    }
}
