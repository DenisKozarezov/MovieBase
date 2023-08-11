using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;

namespace Core.Query
{ 
    /// <summary>
    /// Класс, предназначенный для быстрого и компактного использования запросов
    /// для различных нужд.
    /// </summary>
    sealed class Actor
    {
        private static readonly Dictionary<string, string> TableAttributes = new Dictionary<string, string>()
        {
            { "ActorID", "[ID актёра]" },
            { "FIO", "ФИО" },
            { "Birthday", "[Дата рождения]" },
            { "Country", "Страна" },
        };

        private static IList<string> Attributes
        {
            get
            {
                return TableAttributes.ToList().ConvertAll(x => $"{Constants.Tables.ActorTable}.{x.Key} as {x.Value}");
            }
        }

        /// <summary>
        /// Возвращает ID актёра по ФИО.
        /// </summary>
        /// <returns></returns>
        public static async Task<int> GetIDAsync(string FIO)
        {
            return await Task.Run(() =>
            {
                QueryParametersBuilder builder = new QueryParametersBuilder();
                builder.Add(new QueryParameter { Name = "@FIO", Type = SqlDbType.VarChar, Size = 30, Value = FIO });
                return Generic.ExecuteScalar<int>("GetActorIDByFIO", builder);
            });
        }

        /// <summary>
        /// Возвращает перечень фильмов, в которых снимался актёр.
        /// </summary>
        /// <returns></returns>
        public static async Task<DataTable> GetActingFilmsAsync(int actorID)
        {
            return await Task.Run(() =>
            {
                QueryParametersBuilder builder = new QueryParametersBuilder();
                builder.Add(new QueryParameter { Name = "@actorID", Type = SqlDbType.Int, Value = actorID });
                return Generic.SelectFrom("GetActingFilms", builder);
            });
        }

        /// <summary>
        /// Возвращает список актёров, удовлетворяющих условию запроса.
        /// </summary>
        /// <param name="query">Текст запроса.</param>
        /// <returns></returns>
        public static async Task<DataTable> FindPersonsAsync(string query)
        {
            return await Task.Run(() =>
            {
                QueryParametersBuilder builder = new QueryParametersBuilder();
                builder.Add(new QueryParameter { Name = "@actorID", Type = SqlDbType.VarChar, Size = 100, Value = query });
                return Generic.SelectFrom("FindPersons", builder);
            });
        }

        public static async Task<DataTable> AdvancedSearchAsync(ActorAdvancedSearchArgs args)
        {
            return await Task.Run(() =>
            {
                GenericAdvancedSearchArgs searchArgs = new GenericAdvancedSearchArgs();
                searchArgs.Join(Constants.Tables.ActorTable);
            
                // Outputing result columns
                searchArgs.Output(Attributes[0]);
                searchArgs.Output(Attributes[1]);
                searchArgs.Output("dbo.ConvertBirthdayToString(Actor.Birthday) as [Дата рождения]");
                searchArgs.Output(Attributes[3]);

                // Joining additional tables
                if (args.IncludeActing)
                {
                    searchArgs.Join(Constants.Tables.ActingTable, Constants.Tables.FilmTable);
                    searchArgs.AddCondition(string.Empty, "Actor.ActorID = Acting.ActorID AND Acting.FilmID = Film.FilmID");
                }

                // Birthday
                string birthday = Extensions.ParseDateTime(args.Birthday);
                switch (args.BirthdayComparison)
                {
                    case ComparisonType.LessThenOrEqual:
                        searchArgs.AddCondition("Actor.Birthday <= '", birthday, "'");
                        break;
                    case ComparisonType.GreaterThenOrEqual:
                        searchArgs.AddCondition("Actor.Birthday >= '", birthday, "'");
                        break;
                    case ComparisonType.Equal:
                        searchArgs.AddCondition("Actor.Birthday = '", birthday, "'");
                        break;
                }

                // Country
                if (!string.IsNullOrEmpty(args.Country)) searchArgs.AddCondition("Actor.Country LIKE '%", args.Country, "%'");

                // Acting
                if (args.Acting.Length > 0) searchArgs.AddCondition("Film.Title LIKE '%", args.Acting, "%'");

                // Genres
                if (args.Genres.Length > 0) searchArgs.AddCondition("Film.Genre LIKE '%", args.Genres, "%'");

                return Generic.AdvancedSearch(searchArgs);
            });
        }

        public static async Task AddActor(string fio, string birthday, string country)
        {
            await Task.Run(() =>
            {
                QueryParametersBuilder builder = new QueryParametersBuilder();
                builder.Add(new QueryParameter { Name = "@fio", Type = SqlDbType.VarChar, Size = 100, Value = fio });
                builder.Add(new QueryParameter { Name = "@birthday", Type = SqlDbType.Date, Value = birthday });
                builder.Add(new QueryParameter { Name = "@country", Type = SqlDbType.VarChar, Size = 70, Value = country });
                Generic.ExecuteNonQuery("dbo.AddActor", CommandType.StoredProcedure, builder);
            });
        }

        public static async Task<bool> RemoveActor(int actorID)
        {
            Task task = Task.Run(() =>
            {
                QueryParametersBuilder builder = new QueryParametersBuilder();
                builder.Add(new QueryParameter { Name = "@actorID", Type = SqlDbType.Int, Value = actorID });
                Generic.ExecuteNonQuery("dbo.RemoveActor", CommandType.StoredProcedure, builder);
            });
            await task;
            return task.IsCompleted;
        }

        public static async Task<int> GetMaxIDAsync()
        {
            return await Task.Run(() => Generic.ExecuteScalar<int>("GetActorMax"));
        }
    }
}