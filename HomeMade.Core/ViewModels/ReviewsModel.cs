using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMade.Core.ViewModels
{
    public class ReviewsModel
    {
        public double Rating { get; set; }
        public int NumberOfReviews { get; set; }
        public List<ReviewModel> Reviews { get; set; }
    }

    public class ReviewModel
    {
        public int RatingId { get; set; }
        public string CustomerUsername { get; set; }
        public double Rating { get; set; }
        public string Review { get; set; }
    }
}
