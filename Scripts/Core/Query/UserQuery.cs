using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;

namespace Core.Query
{ 
    /// <summary>
    /// Класс, предназначенный для быстрого и компактного использования запросов
    /// для различных нужд.
    /// </summary>
    sealed class User
    {
        private static readonly Dictionary<string, string> TableAttributes = new Dictionary<string, string>()
        {
            { "UserID", "[ID прльзователя]" },
            { "FIO", "[ФИО пользователя]" },
            { "Login", "Логин" },
            { "Password", "Пароль" },
            { "AccessType", "[Уровень доступа]" },
            { "UserStatus", "Статус" },
        };

        private static IList<string> Attributes
        {
            get
            {
                return TableAttributes.ToList().ConvertAll(x => $"{Constants.Tables.FilmTable}.{x.Key} as {x.Value}");
            }
        }


        /// <summary>
        /// Возвращает ID пользователя по логину.
        /// </summary>
        /// <returns></returns>
        public static async Task<int> GetID(string login)
        {
            return await Task.Run(() =>
            {
                QueryParametersBuilder builder = new QueryParametersBuilder();
                builder.Add(new QueryParameter { Name = "@login", Type = SqlDbType.VarChar, Size = 30, Value = login });
                return Generic.ExecuteScalar<int>("GetUserIDByLogin", builder);
            });
        }

        /// <summary>
        /// Возвращает результат авторизации пользователя по введённым данным.
        /// </summary>
        /// <returns></returns>
        public static async Task<AuthorizationResultArgs> TryAuthorizeAsync(string login, string password)
        {
            TaskCompletionSource<AuthorizationResultArgs> taskCompletionSource = new TaskCompletionSource<AuthorizationResultArgs>();
            AuthorizationResultArgs args = AuthorizationResultArgs.Empty;

            QueryParametersBuilder builder = new QueryParametersBuilder();
            try
            {
                await Task.Run(() =>
                {
                    builder.Add(new QueryParameter { Name = "@login", Type = SqlDbType.VarChar, Size = 30, Value = login });
                    builder.Add(new QueryParameter { Name = "@password", Type = SqlDbType.VarChar, Size = 30, Value = password });
                    builder.Add(new QueryParameter { Name = "@userID", Type = SqlDbType.Int, Direction = ParameterDirection.Output });
                    builder.Add(new QueryParameter { Name = "@FIO", Type = SqlDbType.VarChar, Size = 100, Direction = ParameterDirection.Output });
                    builder.Add(new QueryParameter { Name = "@accessType", Type = SqlDbType.VarChar, Size = 15, Direction = ParameterDirection.Output });
                    builder.Add(new QueryParameter { Name = "@userStatus", Type = SqlDbType.VarChar, Size = 15, Direction = ParameterDirection.Output });
                    builder.Add(new QueryParameter { Name = "@collectionID", Type = SqlDbType.Int, Direction = ParameterDirection.Output });
                    Generic.ExecuteNonQuery("dbo.AuthentificationProc", CommandType.StoredProcedure, builder);
                });
            }

            // Аутентификация провалена: возникла непредвиденная ошибка.
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Аутентификация", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Аутентификация пройдена: пользователь опознан.
                if (int.TryParse(builder["@userID"].Value.ToString(), out int userID))
                {
                    Core.User.TryParseAccessType(builder["@accessType"].Value.ToString(), out AccessType accessType);
                    Core.User.TryParseUserStatus(builder["@userStatus"].Value.ToString(), out UserStatus userStatus);

                    // Процедура авторизации: наделение пользователя правами.
                    if (!int.TryParse(builder["@collectionID"].Value.ToString(), out int collectionID))
                    {
                        collectionID = -1;
                    }

                    args = new AuthorizationResultArgs
                    {
                        UserID = userID,
                        Login = login,
                        Password = password,
                        FIO = builder["@FIO"].Value.ToString(),
                        AccessType = accessType,
                        UserStatus = userStatus,
                        AuthorizationStatus = AuthorizationStatus.Authorized,
                        CollectionID = collectionID
                    };
                }
            }

            taskCompletionSource.SetResult(args);
            return await taskCompletionSource.Task;
        }

        /// <summary>
        /// Добавляет в коллекцию пользователя новый фильм.
        /// </summary>
        public static async void AddFilmInCollection(int collectionID, int filmID)
        {
            await Task.Run(() =>
            {
                QueryParametersBuilder builder = new QueryParametersBuilder();
                builder.Add(new QueryParameter { Name = "@collectionID", Type = SqlDbType.Int, Value = collectionID });
                builder.Add(new QueryParameter { Name = "@filmID", Type = SqlDbType.Int, Value = filmID });
                builder.Add(new QueryParameter { Name = "@success", Type = SqlDbType.Int, Direction = ParameterDirection.Output });
                Generic.ExecuteNonQuery("dbo.AddFilmInCollection", CommandType.StoredProcedure, builder);
                return (int)builder["@success"].Value == 0 ? false : true;
            });
        }
        
        /// <summary>
        /// Удаляет фильм из коллекции.
        /// </summary>
        public static async void RemoveFilmFromCollection(int collectionID, int filmID)
        {
            await Task.Run(() =>
            {
                QueryParametersBuilder builder = new QueryParametersBuilder();
                builder.Add(new QueryParameter { Name = "@collectionID", Type = SqlDbType.Int, Value = collectionID });
                builder.Add(new QueryParameter { Name = "@filmID", Type = SqlDbType.Int, Value = filmID });
                builder.Add(new QueryParameter { Name = "@success", Type = SqlDbType.Int, Direction = ParameterDirection.Output });
                Generic.ExecuteNonQuery("dbo.RemoveFilmInCollection", CommandType.StoredProcedure, builder);
                return (int)builder["@success"].Value == 0 ? false : true;
            });
        }

        /// <summary>
        /// Возвращает результат обладания указанного фильма указанным пользователем.
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> ContainsFilmAsync(int userID, int filmID)
        {
            return await Task.Run(() =>
            {
                QueryParametersBuilder builder = new QueryParametersBuilder();
                builder.Add(new QueryParameter { Name = "@userID", Type = SqlDbType.Int, Value = userID });
                builder.Add(new QueryParameter { Name = "@filmID", Type = SqlDbType.Int, Value = filmID });
                return Generic.ExecuteScalar<int>("UserContainsFilm", builder) > 0;
            });
        }

        /// <summary>
        /// Возвращает список фильмов, которые лежат в коллекции пользователя.
        /// </summary>
        /// <param name="collectionID"></param>
        /// <returns></returns>
        public static async Task<DataTable> CollectionFilmsAsync(int collectionID)
        {
            return await Task.Run(() =>
            {
                QueryParametersBuilder builder = new QueryParametersBuilder();
                builder.Add(new QueryParameter { Name = "@collectionID", Type = SqlDbType.Int, Value = collectionID });
                return Generic.SelectFrom("GetUserFilms", builder);
            });
        }

        /// <summary>
        /// Возвращает список авторизаций.
        /// </summary>
        /// <returns></returns>
        public static async Task<DataTable> AuthorizationListAsync()
        {
            return await Task.Run(() => Generic.SelectFrom("AuthorizationList"));
        }

        /// <summary>
        /// Возвращает список авторизаций.
        /// </summary>
        /// <param name="args">Аргумента поиска.</param>
        /// <returns></returns>
        public static async Task<DataTable> AuthorizationListAsync(UserAdvancedSearchArgs args)
        {
            return await Task.Run(() =>
            {
                GenericAdvancedSearchArgs searchArgs = new GenericAdvancedSearchArgs();
                searchArgs.Join(Constants.Tables.AuthorizationTable, Constants.Tables.UserTable);

                // Outputing result columns
                searchArgs.Output("[User].FIO as [ФИО пользователя]");
                searchArgs.Output("[Authorization].DateTime as [Дата попытки авторизации]");
                searchArgs.Output("[Authorization].AccessType as [Уровень доступа]");
                searchArgs.Output("[Authorization].UserStatus as [Статус]");
                searchArgs.Output("[Authorization].UserID as [ID пользователя]");

                searchArgs.AddCondition(string.Empty, "[Authorization].UserID = [User].UserID");

                // DateTime
                string dateTime = Extensions.ParseDateTime(args.DateTime);
                switch (args.DateComparison)
                {
                    case ComparisonType.LessThenOrEqual:
                        searchArgs.AddCondition("[Authorization].DateTime <= '", dateTime, "'");
                        break;
                    case ComparisonType.GreaterThenOrEqual:
                        searchArgs.AddCondition("[Authorization].DateTime >= '", dateTime, "'");
                        break;
                    case ComparisonType.Equal:
                        searchArgs.AddCondition("[Authorization].DateTime = '", dateTime, "'");
                        break;
                }

                // AccessType
                if (args.AccessType != AccessType.NULL)
                    searchArgs.AddCondition("[Authorization].AccessType = '", args.AccessType.ToString(), "'");

                // UserStatus
                if (args.UserStatus != UserStatus.NULL)
                    searchArgs.AddCondition("[Authorization].UserStatus = '", args.UserStatus.ToString(), "'");

                // FIO
                if (!string.IsNullOrEmpty(args.FIO))
                    searchArgs.AddCondition("[User].FIO LIKE '%", args.FIO, "%'");

                return Generic.AdvancedSearch(searchArgs);
            });
        }

        /// <summary>
        /// Возвращает список заблокированных пользователей.
        /// </summary>
        /// <returns></returns>
        public static async Task<DataTable> BlockingListAsync()
        {
            return await Task.Run(() => Generic.SelectFrom("BlockingList"));
        }
        /// <summary>
        /// Возвращает список заблокированных пользователей.
        /// </summary>
        /// <returns></returns>
        public static async Task<DataTable> BlockingListAsync(UserAdvancedSearchArgs args)
        {
            return await Task.Run(() =>
            {
                GenericAdvancedSearchArgs searchArgs = new GenericAdvancedSearchArgs();
                searchArgs.Join(Constants.Tables.BlockingTable, Constants.Tables.UserTable);

                // Outputing result columns
                searchArgs.Output("[User].FIO as [ФИО пользователя]");
                searchArgs.Output("[Blocking].DateTime as [Дата блокировки]");
                searchArgs.Output("[User].AccessType as [Уровень доступа]");
                searchArgs.Output("dbo.ConvertMinutesToString([Blocking].Period) as [Период (в минутах)]");
                searchArgs.Output("[Blocking].Reason as [Причина]");
                searchArgs.Output("[Blocking].UserID as [ID пользователя]");

                searchArgs.AddCondition(string.Empty, "[Blocking].UserID = [User].UserID");

                // DateTime
                string dateTime = Extensions.ParseDateTime(args.DateTime);
                switch (args.DateComparison)
                {
                    case ComparisonType.LessThenOrEqual:
                        searchArgs.AddCondition("[Blocking].DateTime <= '", dateTime, "'");
                        break;
                    case ComparisonType.GreaterThenOrEqual:
                        searchArgs.AddCondition("[Blocking].DateTime >= '", dateTime, "'");
                        break;
                    case ComparisonType.Equal:
                        searchArgs.AddCondition("[Blocking].DateTime = '", dateTime, "'");
                        break;
                }

                // AccessType
                if (args.AccessType != AccessType.NULL)
                    searchArgs.AddCondition("[User].AccessType = '", args.AccessType.ToString(), "'");

                // FIO
                if (!string.IsNullOrEmpty(args.FIO))
                    searchArgs.AddCondition("[User].FIO LIKE '%", args.FIO, "%'");

                return Generic.AdvancedSearch(searchArgs);
            });
        }


        /// <summary>
        /// Возвращает список пользователей.
        /// </summary>
        /// <returns></returns>
        public static async Task<DataTable> UsersListAsync()
        {
            return await Task.Run(() => Generic.SelectFrom("UsersList"));
        }

        /// <summary>
        /// Блокирует указанного пользователя.
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> BlockUserAsync(int userID, int period, string reason)
        {
            Task task = Task.Run(() =>
            {
                QueryParametersBuilder builder = new QueryParametersBuilder();
                builder.Add(new QueryParameter { Name = "@userID", Type = SqlDbType.Int, Value = userID });
                builder.Add(new QueryParameter { Name = "@period", Type = SqlDbType.Int, Value = period });
                builder.Add(new QueryParameter { Name = "@reason", Type = SqlDbType.VarChar, Size = 200, Value = reason });
                Generic.ExecuteNonQuery("dbo.BlockUser", CommandType.StoredProcedure, builder);
            });
            await task;
            return task.IsCompleted;
        }

        /// <summary>
        /// Снимает блокировку с указанного пользователя.
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> UnblockUserAsync(int userID)
        {
            Task task = Task.Run(() =>
            {
                QueryParametersBuilder builder = new QueryParametersBuilder();
                builder.Add(new QueryParameter { Name = "@userID", Type = SqlDbType.Int, Value = userID });
                Generic.ExecuteNonQuery("dbo.UnblockUser", CommandType.StoredProcedure, builder);
            });
            await task;
            return task.IsCompleted;
        }

        /// <summary>
        /// Удаляет указанного пользователя.
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> RemoveUserAsync(int userID)
        {
            Task task = Task.Run(() =>
            {
                QueryParametersBuilder builder = new QueryParametersBuilder();
                builder.Add(new QueryParameter { Name = "@userID", Type = SqlDbType.Int, Value = userID });
                Generic.ExecuteNonQuery("dbo.RemoveUser", CommandType.StoredProcedure, builder);
            });
            await task;
            return task.IsCompleted;
        }

        /// <summary>
        /// Добавляет нового пользователя.
        /// </summary>
        public static async Task AddUserAsync(UserInfo userInfo)
        {
            await Task.Run(() =>
            {
                QueryParametersBuilder builder = new QueryParametersBuilder();
                builder.Add(new QueryParameter { Name = "@login", Type = SqlDbType.VarChar, Size = 30, Value = userInfo.Login });
                builder.Add(new QueryParameter { Name = "@password", Type = SqlDbType.VarChar, Size = 30, Value = userInfo.Password });
                builder.Add(new QueryParameter { Name = "@fio", Type = SqlDbType.VarChar, Size = 100, Value = userInfo.FIO });
                builder.Add(new QueryParameter { Name = "@accessType", Type = SqlDbType.VarChar, Size = 15, Value = userInfo.AccessType.ToString() });
                builder.Add(new QueryParameter { Name = "@userStatus", Type = SqlDbType.VarChar, Size = 15, Value = userInfo.UserStatus.ToString() });
                Generic.ExecuteNonQuery("dbo.AddUser", CommandType.StoredProcedure, builder);
            });
        }  

        /// <summary>
        /// Изменяет личные данные указанного пользователя.
        /// </summary>
        public static async Task<bool> UpdateProfileInfo(UserInfo userInfo)
        {
            Task task = Task.Run(() =>
            {
                QueryParametersBuilder builder = new QueryParametersBuilder();
                builder.Add(new QueryParameter { Name = "@userID", Type = SqlDbType.Int, Value = userInfo.ID });
                builder.Add(new QueryParameter { Name = "@fio", Type = SqlDbType.VarChar, Size = 100, Value = userInfo.FIO });
                Generic.ExecuteNonQuery("dbo.UpdateUserInfo", CommandType.StoredProcedure, builder);
            });
            await task;
            return task.IsCompleted;
        }

        public static async Task<bool> UpdateAsync(UserInfo userInfo)
        {
            Task task = Task.Run(() =>
            {
                GenericAdvancedSearchArgs args = new GenericAdvancedSearchArgs();
                args.Join(Constants.Tables.UserTable);
                args.Output("Login = '" + userInfo.Login + "'");
                args.Output("Password = '" + userInfo.Password + "'");
                args.Output("FIO = '" + userInfo.FIO + "'");
                args.Output("AccessType = '" + userInfo.AccessType.ToString() + "'");
                args.Output("UserStatus = '" + userInfo.UserStatus.ToString() + "'");

                args.AddCondition("UserID = ", userInfo.ID.ToString());
                Generic.Update(args);
            });
            await task;
            return task.IsCompleted;
        }

        public static async Task<bool> ChangePassword(int userID, string newPassword)
        {
            Task task = Task.Run(() =>
            {
                GenericAdvancedSearchArgs args = new GenericAdvancedSearchArgs();
                args.Join(Constants.Tables.UserTable);
                args.Output("Password = '" + newPassword + "'");
                args.AddCondition("UserID = ", userID.ToString());
                Generic.Update(args);
            });
            await task;
            return task.IsCompleted;
        }
    }
}