using System.ComponentModel.DataAnnotations;
using CoreCSVImport.Lib.Attributes;

namespace CoreCSVImport.Models
{
    public class Session
    {
        public Session() { }

        [Key]
        [SourceNames("session_ticketPrice")]
        public int SessionId { get; set; }
        [SourceNames("session_ticketPrice")]
        public string Title { get; set; }
        [SourceNames("session_conferenceId")]
        public int ConferenceId { get; set; }
        public Conference Conference{ get; set;}  

    }
}