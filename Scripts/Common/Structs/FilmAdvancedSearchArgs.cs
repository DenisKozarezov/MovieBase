using System;

namespace Core.Query
{
    public struct FilmAdvancedSearchArgs
    {
        public bool IncludeActing;
        public string Title;         
        public string Country;
        public string Director;
        public string[] Genres;
        public string[] Acting;
        public decimal Rating;
        public DateTime PremiereDate;   
        public ComparisonType PremiereDateComparison;
        public ComparisonType RatingComparison;
    }
}