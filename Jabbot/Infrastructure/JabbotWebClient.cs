using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Jabbot.Models;

namespace Jabbot.Infrastructure
{

    public class JabbotWebClient : WebClient
    {
        /// <summary>
        /// Copy of the ChatMessage passed to ProcessMatch (Used later when Ansyc call is complete)
        /// </summary>
        public ChatMessage Message
        {
            get;
            set;
        }

        /// <summary>
        /// Copy of the Bot passed to ProcessMatch (Used later when Ansyc call is complete)
        /// </summary>
        public Bot Bot
        {
            get;
            set;
        }
    }
}
