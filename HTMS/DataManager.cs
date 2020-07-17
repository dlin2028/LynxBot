using Discord;
using Discord.Rest;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Text.Json;

namespace HTMS
{
    public class Invite
    {
        public ulong userId;
        public string inviteId;
        public List<ulong> usersInvited;

        public Invite(ulong userId, string inviteId)
        {
            this.userId = userId;
            this.inviteId = inviteId;
            usersInvited = new List<ulong>();
        }
    }
    public static class DataManager
    {
        private static ulong welcomeChannel = 0;

        public static ulong WelcomeChannel
        {
            get
            {
                if (welcomeChannel == 0)
                {
                    try
                    {
                        welcomeChannel = JsonSerializer.Deserialize<ulong>(File.ReadAllText("welcome"));
                    }
                    catch
                    {
                        welcomeChannel = 0;
                    }
                }
                return welcomeChannel;
            }
            set
            {
                welcomeChannel = value;
                var jsonString = JsonSerializer.Serialize(welcomeChannel);
                File.WriteAllText("welcome", jsonString);
            }
        }

        private static ulong landingChannel = 0;

        public static ulong LandingChannel
        {
            get
            {
                if (landingChannel == 0)
                {
                    try
                    {
                        landingChannel = JsonSerializer.Deserialize<ulong>(File.ReadAllText("landing"));
                    }
                    catch
                    {
                        landingChannel = 0;
                    }
                }
                return landingChannel;
            }
            set
            {
                landingChannel = value;
                var jsonString = JsonSerializer.Serialize(landingChannel);
                File.WriteAllText("landing", jsonString);
            }
        }

        private static List<Invite> trackedInvites;

        public static List<Invite> TrackedInvites
        {
            get
            {
                if (trackedInvites == null)
                {
                    try
                    {
                        trackedInvites = JsonSerializer.Deserialize<List<Invite>>(File.ReadAllText("trackedinvites"));
                    }
                    catch
                    {
                        trackedInvites = new List<Invite>();
                    }
                }
                return trackedInvites;
            }
            set
            {
                var jsonString = JsonSerializer.Serialize(trackedInvites);
                File.WriteAllText("trackedinvites", jsonString);
                trackedInvites = value;
            }
        }


        public static IReadOnlyCollection<RestInviteMetadata> Invites;

        private static Dictionary<string, string> inviteMap;

        public static Dictionary<string, string> InviteMap  //userid , inviteid
        {
            get {
                if (inviteMap == null)
                {
                    try
                    {
                        inviteMap = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText("invites"));
                    }
                    catch
                    {
                        inviteMap = new Dictionary<string, string>();
                    }
                }
                return inviteMap;
            }
            set
            {
                var jsonString = JsonSerializer.Serialize(inviteMap);
                File.WriteAllText("invites", jsonString);
                inviteMap = value;
            }
        }

        public static Dictionary<string, int> LeaveCount;
    }
}
