using Discord;
using Discord.Commands;
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
        private static Dictionary<string, string> invites = null;

        [Command("createinvite")]
        public async Task createInvite()
        {
            if (invites == null)
            {
                try
                {
                    invites = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText("invites"));
                }
                catch
                {
                    invites = new Dictionary<string, string>();
                }
            }

            IUser user = Context.User as IUser;
            ITextChannel chnl = Context.Client.GetChannel(Context.Channel.Id) as ITextChannel;
            IInvite invite = await chnl.CreateInviteAsync(maxAge: null);

            if(invites.ContainsKey(user.Id.ToString()))
            {
                var inviteId = invites[Context.User.Id.ToString()];
                var thing = await chnl.GetInvitesAsync();
                var realInvite = thing.Where(x => x.Id == inviteId).FirstOrDefault();
                await Context.Channel.SendMessageAsync(realInvite.Url);
            }
            else
            {
                invites.Add(user.Id.ToString(), invite.Id);
                await Context.Channel.SendMessageAsync(invite.Url);
            }

            var jsonString = JsonSerializer.Serialize(invites);
            File.WriteAllText("invites", jsonString);

        }


        [Command("invites")]
        public async Task getInvites()
        {
            if (invites == null)
            {
                try
                {
                    invites = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText("invites"));
                }
                catch
                {
                    invites = new Dictionary<string, string>();
                }
            }
            if(invites.ContainsKey(Context.User.Id.ToString()))
            {
                var inviteId = invites[Context.User.Id.ToString()];
                ITextChannel chnl = Context.Client.GetChannel(Context.Channel.Id) as ITextChannel;
                var thing = await chnl.GetInvitesAsync();
                var realInvite = thing.Where(x => x.Id == inviteId).FirstOrDefault();
                await Context.Channel.SendMessageAsync(realInvite.Uses.ToString());
            }
            else
            {
                await Context.Channel.SendMessageAsync("0");
            }
        }
    }
}
