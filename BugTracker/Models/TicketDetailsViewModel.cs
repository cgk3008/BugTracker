using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class TicketDetailsViewModel
    {
        public IEnumerable<TicketHistory> HistoryData { get; set; }
        public IEnumerable<TicketComment> CommentData { get; set; }
    }
}