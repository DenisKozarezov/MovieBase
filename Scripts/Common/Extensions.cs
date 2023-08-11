using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Windows.Forms;
using System.Data;

namespace Core
{
    static class Extensions
    {
        public static bool IsNullOrEmpty(params string[] strings)
        {
            return strings.Where(x => string.IsNullOrEmpty(x)).Any();
        }
        public static string ParseDateTime(DateTime dateTime) => dateTime.ToString(Constants.ShortDateFormat);
        public static string ParseMonth(int monthIndex) => CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(monthIndex);
        public static string ParseVideoURL(string url)
        {
            var regex = new Regex(@"http(?:s?)://(?:www\.)?youtu(?:be\.com/watch\?v=|\.be/)([\w\-]+)(&(amp;)?[\w\?=]*)?").Match(url);
            return regex.Success ? regex.Groups[1].Value : string.Empty;
        }
        public static string GetCellByName(string name, DataRow row)
        {
            for (int i = 0; i < row.Table.Columns.Count; i++)
            {
                if (row.Table.Columns[i].ColumnName == name) return row.ItemArray[i].ToString();
            }
            throw new ArgumentException("В таблице нет столбца с таким наименованием.");
        }
        public static T GetCellByName<T>(string name, DataRow row)
        {
            for (int i = 0; i < row.Table.Columns.Count; i++)
            {
                if (row.Table.Columns[i].ColumnName == name) return (T)row.ItemArray[i];
            }
            throw new ArgumentException("В таблице нет столбца с таким наименованием.");
        }
        public static DataGridViewCell GetCellByName(string name, DataGridViewRow row)
        {
            foreach (DataGridViewCell cell in row.Cells)
            {
                if (cell.OwningColumn.Name == name) return cell;
            }
            throw new ArgumentException("В таблице нет столбца с таким наименованием.");       
        }
        public static void Enumerate(this DataGridView view, DataGridViewColumnSortMode sortMode = DataGridViewColumnSortMode.Automatic)
        {
            foreach (DataGridViewRow row in view.Rows)
            {
                row.HeaderCell.Value = (row.Index + 1).ToString();
                foreach (DataGridViewCell cell in row.Cells) cell.OwningColumn.SortMode = sortMode;
            }
        }
    }
}