using System;

namespace Core.Query
{
    public struct UserAdvancedSearchArgs
    {
        public string FIO;
        public AccessType AccessType;
        public UserStatus UserStatus;
        public DateTime DateTime;
        public ComparisonType DateComparison;
    }
}