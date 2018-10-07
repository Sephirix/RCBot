using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RCBot.Models
{
    

    public class MemberProfile
    {
        public int Id { get; set; }
        public ulong UserId { get; set; }
        public string inGameName { get; set; }
        public string inviteID { get; set; }
        public string forTrade { get; set; }
        public string stock { get; set; }
   


    }
}
