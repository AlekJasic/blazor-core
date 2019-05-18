using System;
using System.Collections.Generic;

namespace Blazor_CORE.Shared.Models
{
    public partial class ConcertDetails
    {
        public int ConcertDetailNo { get; set; }
        public int ConcertNo { get; set; }
        public string ArtistName { get; set; }
        public string Notes { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
    }
}
