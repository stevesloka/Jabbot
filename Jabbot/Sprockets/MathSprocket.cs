using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Jabbot.Models;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using Jabbot.Infrastructure;

namespace Jabbot.Sprockets
{
    public class MathSprocket : RegexSprocket
    {
        /// <summary>
        /// Looks for pattern of commands to reply to
        /// </summary>
        public override Regex Pattern
        {
            get { return new Regex("(calc|calculate|convert|math)( me)? (.*)"); }
        }

        protected override void ProcessMatch(Match match, ChatMessage message, Bot bot)
        {
            var client = new JabbotWebClient();
            string uri = "http://www.google.com/ig/calculator?hl=en&q=" + Uri.EscapeDataString(match.Groups[3].Value);
            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

            client.Bot = bot;
            client.Message = message;

            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(client_DownloadStringCompleted);
            client.DownloadStringAsync(new System.Uri(uri));
        }


        /// <summary>
        /// Handles the client complete request
        /// </summary>
        /// <param name="sender">Should be the overriden jabbotclient</param>
        /// <param name="e">The string result</param>
        void client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            JabbotWebClient client = (JabbotWebClient)sender;

            try
            {
                dynamic json = JsonConvert.DeserializeObject(e.Result);
                string solution = json.rhs;
                client.Bot.Reply(client.Message.FromUser, solution ?? "Could not compute.");
            }
            catch (Exception ex)
            {
                client.Bot.Reply(client.Message.FromUser, "Could not compute.");

                throw ex;
            }
        }
    }


}
