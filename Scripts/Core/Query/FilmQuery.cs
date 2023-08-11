using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data;

namespace Core.Query
{ 
    /// <summary>
    /// Класс, предназначенный для быстрого и компактного использования запросов
    /// для различных нужд.
    /// </summary>
    sealed class Film
    {
        private static readonly Dictionary<string, string> TableAttributes = new Dictionary<string, string>()
        {
            { "FilmID", "[ID фильма]" },
            { "Title", "Название" },
            { "Director", "Режиссёр" },
            { "Genre", "[Жанр фильма]" },
            { "Country", "Страна" },
            { "Description", "Описание" },
            { "PremiereDate", "[Дата премьеры]" },
            { "Rating", "Рейтинг" },
        };

        private static IList<string> Attributes
        {
            get
            {
                return TableAttributes.ToList().ConvertAll(x => $"{Constants.Tables.FilmTable}.{x.Key} as {x.Value}");
            }
        }

        /// <summary>
        /// Возвращает перечень топ-100 фильмов.
        /// </summary>
        /// <returns></returns>
        public static async Task<DataTable> TopFilmsAsync(byte count)
        {
            return await Task.Run(() => Generic.SelectFrom("Top100Films"));
        }

        /// <summary>
        /// Возвращает список премьер на ближайшую неделю.
        /// </summary>
        /// <returns></returns>
        public static async Task<DataTable> WeekPremiereAsync()
        {
            return await Task.Run(() =>
            {
                GenericAdvancedSearchArgs searchArgs = new GenericAdvancedSearchArgs();
                searchArgs.Join(Constants.Tables.FilmTable);
                searchArgs.SelectType = SelectType.USUAL;

                // Outputing result columns
                IList<string> output = Attributes;
                output.RemoveAt(0);
                searchArgs.Output(output);

                searchArgs.AddCondition("PremiereDate BETWEEN ", "DATEADD(WEEK, -1, GETDATE()) ", "AND GETDATE()");
                return Generic.AdvancedSearch(searchArgs);
            });
        }

        /// <summary>
        /// Добавляет новый фильм в базу данных.
        /// </summary>
        public static async Task AddFilmAsync(FilmInfo filmInfo)
        {
            await Task.Run(() =>
            {
                QueryParametersBuilder builder = new QueryParametersBuilder();
                builder.Add(new QueryParameter { Name = "@title", Type = SqlDbType.VarChar, Size = 100, Value = filmInfo.Title });
                builder.Add(new QueryParameter { Name = "@director", Type = SqlDbType.VarChar, Size = 100, Value = filmInfo.Director });
                builder.Add(new QueryParameter { Name = "@genre", Type = SqlDbType.VarChar, Size = 100, Value = filmInfo.Genre });
                builder.Add(new QueryParameter { Name = "@country", Type = SqlDbType.VarChar, Size = 100, Value = filmInfo.Country });
                builder.Add(new QueryParameter { Name = "@description", Type = SqlDbType.VarChar, Size = 300, Value = filmInfo.Description });
                builder.Add(new QueryParameter { Name = "@premiereDate", Type = SqlDbType.Date, Value = filmInfo.PremiereDate });
                builder.Add(new QueryParameter { Name = "@rating", Type = SqlDbType.Decimal, Value = filmInfo.Rating });
                Generic.ExecuteNonQuery("dbo.AddFilm", CommandType.StoredProcedure, builder);
            });
        }

        /// <summary>
        /// Удаляет фильм из базы данных.
        /// </summary>
        public static async Task<bool> RemoveFilm(int filmID)
        {
            Task task = Task.Run(() =>
            {
                QueryParametersBuilder builder = new QueryParametersBuilder();
                builder.Add(new QueryParameter { Name = "@filmID", Type = SqlDbType.Int, Value = filmID });
                Generic.ExecuteNonQuery("dbo.RemoveFilm", CommandType.StoredProcedure, builder);
            });
            await task;
            return task.IsCompleted;
        }

        public static async Task<DataTable> AdvancedSearchAsync(FilmAdvancedSearchArgs args)
        {
            return await Task.Run(() =>
            {
                GenericAdvancedSearchArgs searchArgs = new GenericAdvancedSearchArgs();
                searchArgs.Join(Constants.Tables.FilmTable);
                searchArgs.SelectType = SelectType.USUAL;
          
                // Outputing result columns
                searchArgs.Output(Attributes);

                // Joining additional tables
                if (args.IncludeActing)
                {
                    searchArgs.Join(Constants.Tables.ActingTable, Constants.Tables.ActorTable);
                    searchArgs.AddCondition(string.Empty, "Actor.ActorID = Acting.ActorID AND Acting.FilmID = Film.FilmID");
                }

                // Country
                if (!string.IsNullOrEmpty(args.Title)) searchArgs.AddCondition("Film.Title LIKE '%", args.Title, "%'");

                // Country
                if (!string.IsNullOrEmpty(args.Country)) searchArgs.AddCondition("Film.Country LIKE '%", args.Country, "%'");

                // Genre
                if (args.Genres.Length > 0) searchArgs.AddCondition("Film.Genre LIKE '%", args.Genres, "%'");

                // Director
                if (!string.IsNullOrEmpty(args.Director)) searchArgs.AddCondition("Film.Director LIKE '%", args.Director, "%'");

                // Acting
                if (args.Acting.Length > 0) searchArgs.AddCondition("Actor.FIO LIKE '%", args.Acting, "%'");

                // PremiereDate
                string premiereDate = Extensions.ParseDateTime(args.PremiereDate);
                switch (args.PremiereDateComparison)
                {
                    case ComparisonType.LessThenOrEqual:
                        searchArgs.AddCondition("PremiereDate <= '", premiereDate, "'");
                        break;
                    case ComparisonType.GreaterThenOrEqual:
                        searchArgs.AddCondition("PremiereDate >= '", premiereDate, "'");
                        break;
                    case ComparisonType.Equal:
                        searchArgs.AddCondition("PremiereDate = '", premiereDate, "'");
                        break;
                }

                // Rating
                string rating = args.Rating.ToString().Replace(',', '.');
                switch (args.RatingComparison)
                {
                    case ComparisonType.LessThenOrEqual:
                        searchArgs.AddCondition("Film.Rating <= ", rating);
                        break;
                    case ComparisonType.GreaterThenOrEqual:
                        searchArgs.AddCondition("Film.Rating >= ", rating);
                        break;
                    case ComparisonType.Equal:
                        searchArgs.AddCondition("Film.Rating = ", rating);
                        break;
                }
                return Generic.AdvancedSearch(searchArgs);
            });
        }

        public static async Task<DataTable> FindFilm(string query)
        {
            return await Task.Run(() =>
            {
                GenericAdvancedSearchArgs searchArgs = new GenericAdvancedSearchArgs();
                searchArgs.Join(Constants.Tables.FilmTable);

                // Outputing all columns
                searchArgs.Output(Attributes);

                searchArgs.AddCondition("Title LIKE '%", query, "%'");

                return Generic.SelectFrom(searchArgs);
            });
        }

        /// <summary>
        /// Возвращает максимальный FilmID из таблицы "Film".
        /// </summary>
        public static async Task<int> GetMaxIDAsync()
        {
            return await Task.Run(() => Generic.ExecuteScalar<int>("GetFilmMax"));
        }     
        
        public static async Task UpdateAsync(FilmInfo filmInfo)
        {
            GenericAdvancedSearchArgs args = new GenericAdvancedSearchArgs();

            args.Join(Constants.Tables.FilmTable);
            args.Output("Title = '" + filmInfo.Title + "'");
            args.Output("Director = '" + filmInfo.Director + "'");
            args.Output("Genre = '" + filmInfo.Genre + "'");
            args.Output("Country = '" + filmInfo.Country + "'");
            args.Output("Description = '" + filmInfo.Description + "'");
            args.Output("PremiereDate = '" + Extensions.ParseDateTime(filmInfo.PremiereDate) + "'");
            args.Output("Rating = " + filmInfo.Rating.ToString().Replace(',', '.'));
            args.AddCondition("FilmID = ", filmInfo.FilmID.ToString());
            await Task.Run(() => Generic.Update(args));
        }
    }
}