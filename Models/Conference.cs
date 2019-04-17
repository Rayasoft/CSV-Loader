using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CoreCSVImport.Lib.Attributes;
namespace CoreCSVImport.Models
{
    public class Conference
    {
        public Conference() { }
        [Key]
        [SourceNames("confrence_Id")]
        public int ConferenceId { get; set; }
        [SourceNames("conference_Name")]
        public string Name { get; set; }
        [SourceNames("conference_ticketPrice")]
        public decimal TicketPrice { get; set; }
        public List<Session> Sessions { get; set;}

    }
    
}