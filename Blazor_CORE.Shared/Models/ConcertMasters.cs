using System;
using System.Collections.Generic;

namespace Blazor_CORE.Shared.Models
{
    public partial class ConcertMasters
    {
        public int ConcertNo { get; set; }
        public string HallId { get; set; }
        public string Description { get; set; }
        public DateTime ConcertDate { get; set; }
        public string TicketServiceName { get; set; }
    }
}
