using System;

namespace Core.Query
{
    public struct ActorAdvancedSearchArgs
    {
        public bool IncludeActing;
        public DateTime Birthday;
        public string Country;
        public string[] Acting;
        public string[] Genres;
        public ComparisonType BirthdayComparison;
    }
}