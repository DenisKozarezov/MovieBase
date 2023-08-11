using System.Drawing;
using System.Windows.Forms;

namespace Core.UI
{
    static class Styles
    {
        public static readonly DataGridViewCellStyle FilmRatingStyle = new DataGridViewCellStyle()
        {
            Font = Fonts.ArialBold,
            BackColor = Color.DarkMagenta,
            ForeColor = Color.Yellow,
            Alignment = DataGridViewContentAlignment.TopCenter   
        };
        public static readonly DataGridViewCellStyle CollectionAddCellStyle = new DataGridViewCellStyle()
        {
            Font = Fonts.ArialBold,
            BackColor = Color.DarkOrange,
            ForeColor = Color.Green,
            Alignment = DataGridViewContentAlignment.MiddleCenter
        };
        public static readonly DataGridViewCellStyle CollectionRemoveCellStyle = new DataGridViewCellStyle()
        {
            Font = Fonts.ArialBold,
            BackColor = Color.DarkRed,
            ForeColor = Color.Yellow,
            Alignment = DataGridViewContentAlignment.MiddleCenter
        };
        public static readonly DataGridViewCellStyle BlockedUserStyle = new DataGridViewCellStyle()
        {
            BackColor = Color.DarkRed,
            ForeColor = Color.White,
            Alignment = DataGridViewContentAlignment.MiddleLeft
        };
        public static readonly DataGridViewCellStyle AdminUserStyle = new DataGridViewCellStyle()
        {
            BackColor = Color.DarkBlue,
            ForeColor = Color.Yellow,
            Alignment = DataGridViewContentAlignment.MiddleLeft
        };
        public static readonly DataGridViewCellStyle DefaultCellStyle = new DataGridViewCellStyle()
        {
            BackColor = Color.White,
            ForeColor = Color.Black,
            Alignment = DataGridViewContentAlignment.MiddleLeft
        };
    }
}