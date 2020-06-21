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
        private static Dictionary<IUser, IInvite> invites = null;

        [Command("createinvite")]
        public async Task createInvite()
        {
            if (invites == null)
            {
                try
                {
                    invites = JsonSerializer.Deserialize<Dictionary<IUser, IInvite>>(File.ReadAllText("invites"));
                }
                catch
                {
                    invites = new Dictionary<IUser, IInvite>();
                }
            }

            IUser user = Context.User as IUser;
            ITextChannel chnl = Context.Client.GetChannel(Context.Channel.Id) as ITextChannel;
            IInvite invite = await chnl.CreateInviteAsync(maxAge: null);
            invites.Add(user, invite);

            var jsonString = JsonSerializer.Serialize(invites);
            File.WriteAllText("invites", jsonString);

            await Context.Channel.SendMessageAsync(invite.Url);
        }


        [Command("invites")]
        public async Task getInvites()
        {
            if (invites == null)
            {
                try
                {
                    invites = JsonSerializer.Deserialize<Dictionary<IUser, IInvite>>(File.ReadAllText("invites"));
                }
                catch
                {
                    invites = new Dictionary<IUser, IInvite>();
                }
            }
            if(invites.ContainsKey(Context.User as IUser))
            {
                IInvite invite = invites[Context.User as IUser];
                ITextChannel chnl = Context.Client.GetChannel(Context.Channel.Id) as ITextChannel;
                var thing = await chnl.GetInvitesAsync();
                var realInvite = thing.Where(x => x.Id == invite.Id).FirstOrDefault();
                await Context.Channel.SendMessageAsync(realInvite.Uses.ToString());
            }
            else
            {
                await Context.Channel.SendMessageAsync("0");
            }
        }
    }
}
