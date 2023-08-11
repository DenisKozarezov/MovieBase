using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Core.Query
{
    public class GenericAdvancedSearchArgs
    {       
        private Dictionary<int, string> Headers { set; get; } = new Dictionary<int, string>();
        private Dictionary<int, string> Conditions { set; get; } = new Dictionary<int, string>();
        private Dictionary<int, string> Tables { set; get; } = new Dictionary<int, string>();
        public SelectType SelectType { set; get; } = SelectType.DISTINCT;
        public int ConditionsCount => Conditions.Count;
        public int TablesCount => Tables.Count;

        public bool HasJoined(string table) => Tables.ContainsValue(table);
        public bool HasJoined(params string[] tables) => tables.All(x => HasJoined(x));

        /// <summary>
        /// Вывод указанного столбца/столбцов.
        /// </summary>
        /// <param name="headers">Выводимые стобцы.</param>
        public void Output(params string[] headers)
        {
            if (headers == null || headers.Length == 0) return;
            foreach (string item in headers)
            {
                if (!string.IsNullOrEmpty(item) && !Headers.ContainsValue(item)) Headers.Add(Headers.Count, item);
            }
        }
        /// <summary>
        /// Вывод указанного столбца/столбцов.
        /// </summary>
        /// <param name="headers">Выводимые стобцы.</param>
        public void Output(IEnumerable<string> headers) => Output(headers.ToArray());

        /// <summary>
        /// Присоединяет указанную таблицу к запросу.
        /// </summary>
        /// <param name="tables">Таблицы.</param>
        public void Join(params string[] tables)
        {
            if (tables == null || tables.Length == 0) return;
            foreach (string item in tables)
            {
                if (!string.IsNullOrEmpty(item) && !Tables.ContainsValue(item)) Tables.Add(TablesCount, $"dbo.[{item}]");
            }
        }
        /// <summary>
        /// Присоединяет указанную таблицу к запросу.
        /// </summary>
        /// <param name="tables">Таблицы.</param>
        public void Join(IEnumerable<string> tables) => Join(tables.ToArray());

        /// <summary>
        /// Добавляет к запросу указанное условие.
        /// </summary>
        /// <param name="prefix">Префикс условия, задающий начало предиката.</param>
        /// <param name="condition">Значение внутри предиката.</param>
        public void AddCondition(string prefix, string condition) => AddCondition(prefix, condition, string.Empty);

        /// <summary>
        /// Добавляет к запросу указанное условие.
        /// </summary>
        /// <param name="prefix">Префикс условия, задающий начало предиката.</param>
        /// <param name="condition">Значение внутри предиката.</param>
        /// <param name="suffix">Суффикс условия, задающий конец предиката.</param>
        public void AddCondition(string prefix, string condition, string suffix)
        {
            if (string.IsNullOrEmpty(condition)) return;

            string value = string.Concat(prefix, condition, suffix);
            if (!Conditions.ContainsValue(value)) Conditions.Add(Conditions.Count, value);
        }

        /// <summary>
        /// Добавляет к запросу коллекцию с условиями.
        /// </summary>
        /// <param name="prefix">Префикс условия, задающий начало предиката.</param>
        /// <param name="conditions">Коллекция, содержащая набор условий.</param>
        public void AddCondition(string prefix, IEnumerable<string> conditions) => AddCondition(prefix, conditions, string.Empty);

        /// <summary>
        /// Добавляет к запросу коллекцию с условиями.
        /// </summary>
        /// <param name="prefix">Префикс условия, задающий начало предиката.</param>
        /// <param name="conditions">Коллекция, содержащая набор условий.</param>
         /// <param name="suffix">Суффикс условия, задающий конец предиката.</param>
        public void AddCondition(string prefix, IEnumerable<string> conditions, string suffix)
        {
            if (conditions.Count() == 0) return;
            foreach (var item in conditions)
            {
                if (string.IsNullOrEmpty(item)) continue;

                string condition = string.Concat(prefix, item, suffix);
                if (!Conditions.ContainsValue(condition)) Conditions.Add(Conditions.Count, condition);
            }
        }

        /// <summary>
        /// Удаляет условие из запроса во указанному индексу.
        /// </summary>
        public void RemoveCondition(int index)
        {
            if (index < ConditionsCount) Conditions.Remove(index);
            else throw new ArgumentOutOfRangeException();
        }

        public string GetOutput()
        {
            if (Headers.Count == 0) return string.Empty;
            return string.Join(",", Headers.Select(x => x.Value));
        }
        public string GetConditions()
        {
            if (ConditionsCount == 0) return string.Empty;
            return string.Join(" AND ", Conditions.Select(x => x.Value));
        }
        public string GetTables()
        {
            if (TablesCount == 0) return string.Empty;
            return string.Join(",", Tables.Select(x => x.Value));
        }
    }
}