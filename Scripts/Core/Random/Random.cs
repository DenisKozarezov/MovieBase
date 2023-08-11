using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Core.Query;

namespace Core.Random
{
    internal sealed class Randomizer
    { 
        private readonly Dictionary<char, string> Translit = new Dictionary<char, string>()
        {
            //RUS -> ENG
            { 'а', "a" },
            { 'б', "b" },
            { 'в', "v" },
            { 'г', "g" },
            { 'д', "d" },
            { 'е', "e" },
            { 'ё', "yo" },
            { 'ж', "zh" },
            { 'з', "z" },
            { 'и', "i" },
            { 'й', "y" },
            { 'к', "k" },
            { 'л', "l" },
            { 'м', "m" },
            { 'н', "n" },
            { 'о', "o" },
            { 'п', "p" },
            { 'р', "r" },
            { 'с', "s" },
            { 'т', "t" },
            { 'у', "u" },
            { 'ф', "f" },
            { 'х', "h" },
            { 'ц', "tz" },
            { 'ч', "ch" },
            { 'ш', "sh" },
            { 'щ', "tsch" },
            { 'ъ', "" },
            { 'ы', "i" },
            { 'ь', "" },
            { 'э', "ie" },
            { 'ю', "iu" },
            { 'я', "ya" },
        };
        private readonly Dictionary<byte, byte> MonthsAndDays = new Dictionary<byte, byte>()
        {
            //Month -> Last Day
            {1, 31},
            {2, 28},
            {3, 31},
            {4, 30},
            {5, 31},
            {6, 30},
            {7, 31},
            {8, 31},
            {9, 30},
            {10, 31},
            {11, 30},
            {12, 31}
        };

        /// <summary>
        /// Возвращает случайное значение из перечисления.
        /// </summary>
        /// <typeparam name="T">Тип перечисления.</typeparam>
        /// <returns></returns>
        private T GetRandomValueFromEnum<T>() where T : Enum
        {
            System.Random random = new System.Random();
            Array array = Enum.GetValues(typeof(T));
            return (T)array.GetValue(random.Next(array.Length));
        }

        /// <summary>
        /// Посимвольно конвертирует русский текст в английский.
        /// </summary>
        /// <returns></returns>
        public string ConvertToEng(string text)
        {
            if (text == string.Empty) return null;

            StringBuilder builder = new StringBuilder(text);
            for (int i = 0; i < builder.Length; i++)
            {
                Translit.TryGetValue(char.ToLower(builder[i]), out string pare);
                builder.Append(pare);
            }
            return builder.ToString();
        }

        /// <summary>
        /// Генерирует случайным образом ФИО.
        /// </summary>
        /// <returns></returns>
        public string GenerateFIO()
        {
            string secondName = GetRandomValueFromEnum<FirstNames>().ToString();
            string name = GetRandomValueFromEnum<Names>().ToString();
            string vorname = GetRandomValueFromEnum<SecondNames>().ToString();
            return string.Join(" ", secondName, name, vorname);
        }

        /// <summary>
        /// Генерирует случайным образом пароль указанной длины с латинскими буквами.
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GeneratePassword(byte length)
        {
            string result = string.Empty;
            System.Random random = new System.Random();
            for (int i = 0; i < length; i++) result += (char)random.Next('A', 'Z' + 1);
            return result;
        }

        /// <summary>
        /// Генерирует случайным образом страну.
        /// </summary>
        /// <returns></returns>
        public string GenerateCountry()
        {
            string result = string.Empty;
            string country = GetRandomValueFromEnum<Countries>().ToString();
            byte count = 0;
            bool hasNext = false;

            StringBuilder builder = new StringBuilder(country);
            for (int i = 0; i < builder.Length - 1; i++)
            {
                if (builder[i] != '_') result += builder[i];
                else
                {
                    count++;
                    if (builder[i + 1] == '_') hasNext = true;
                    else hasNext = false;

                    if (!hasNext)
                    {
                        if (count == 1) result += ' ';
                        else if (count == 2) result += '-';
                        else result += "'";
                        count = 0;
                    }
                }
            }
            return result + builder[builder.Length - 1];
        }

        /// <summary>
        /// Генерирует случайным образом дату.
        /// </summary>
        /// <returns></returns>
        public string GenerateDate()
        {
            System.Random random = new System.Random();
            byte month = (byte)random.Next(1, 13);
            MonthsAndDays.TryGetValue(month, out byte upperLimit);
            byte day = (byte)random.Next(1, upperLimit + 1);
            short year = (short)random.Next(1945, 2001);
            return string.Format(Constants.ShortDateFormat, string.Join("-", day, month, year));
        }

        /// <summary>
        /// Генерирует случайным образом жанр фильма.
        /// </summary>
        /// <returns></returns>
        public string GenerateGenre() => GetRandomValueFromEnum<Genres>().ToString();

        /// <summary>
        /// Генерирует случайным образом массив пользователей.
        /// </summary>
        public void GenerateUser()
        {
            string fio = GenerateFIO();
            string accessType = GetRandomValueFromEnum<AccessType>().ToString();

            System.Random random = new System.Random();
            int statusRand = random.Next(0, 16);
            UserStatus userStatus = statusRand == 15 ? UserStatus.Заблокирован : UserStatus.Свободен;

            string commandText = "INSERT INTO dbo.[User](Login, Password, FIO, AccessType, UserStatus) VALUES(";
            commandText += "'" + ConvertToEng(fio.Split(' ')[0]) + "', ";
            commandText += "'" + GeneratePassword(8) + "', ";
            commandText += "'" + fio + "', ";
            commandText += "'" + accessType + "', ";
            commandText += "'" + userStatus.ToString() + "')";

            Generic.ExecuteNonQuery(commandText);
            Generic.ExecuteNonQuery("UPDATE [User] SET CollectionID = (SELECT Max(UserID) FROM [User]) WHERE UserID = (SELECT Max(UserID) FROM [User])");
        }

        /// <summary>
        /// Генерирует случайным образом массив актёра.
        /// </summary>
        public void GenerateActor()
        {
            SqlConnection connection = new SqlConnection(Constants.SQLConnectionString);
            connection.Open();

            string commandText = "INSERT INTO dbo.[Actor](FIO, Birthday, Country) VALUES(";
            commandText += "'" + GenerateFIO() + "', ";
            commandText += "'" + GenerateDate() + "', ";
            commandText += "'" + GenerateCountry() + "')";

            Generic.ExecuteNonQuery(commandText);
        }

        public void GenerateFilm()
        {
            SqlConnection connection = new SqlConnection(Constants.SQLConnectionString);
            connection.Open();

            string commandText = "INSERT INTO dbo.[Film] VALUES(";
            commandText += "'" + GenerateFIO() + "', ";
            commandText += "'" + GenerateDate() + "', ";
            commandText += "'" + GenerateCountry() + "')";

            Generic.ExecuteNonQuery(commandText);
        }
    }
}