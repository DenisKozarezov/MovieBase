namespace Core
{
    static class Constants
    {
        private const string DataSource = "LAPTOP-T3KI137K";
        private const string DatabaseCatalog = "Course-work";

        /// <summary>
        /// Строка обращения к СУБД.
        /// </summary>
        public static readonly string SQLConnectionString = $@"Data Source={DataSource};Initial Catalog={DatabaseCatalog};Integrated Security=True";

        /// <summary>
        /// Максимальное количество попыток авторизации.
        /// </summary>
        public const byte AuthorizationTriesMax = 3;

        /// <summary>
        /// Используемый для защиты алгоритм хеширования.
        /// </summary>
        public const Security.HashType HashAlgorythm = Security.HashType.MD5;

        /// <summary>
        /// Формат даты по стандарту ISO-8601.
        /// </summary>
        public const string ShortDateFormat = "MM/dd/yyyy";

        public struct Tables
        {
            public const string FilmTable = "Film";
            public const string ActorTable = "Actor";
            public const string UserTable = "User";
            public const string ActingTable = "Acting";
            public const string CollectionTable = "Collection";
            public const string AuthorizationTable = "Authorization";
            public const string BlockingTable = "Blocking";
        }      
    }
}