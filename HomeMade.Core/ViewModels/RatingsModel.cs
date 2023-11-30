using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMade.Core.ViewModels
{
    public class RatingsModel
    {
        public int RatingId { get; set; }
        public short Rating { get; set; }
        public string Review { get; set; }
        public int SubOrderId { get; set; }
    }
}
