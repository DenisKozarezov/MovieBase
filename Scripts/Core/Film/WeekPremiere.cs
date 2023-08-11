using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Core.UI;

namespace Core
{
    internal class WeekPremiere
    {
        private readonly System.Windows.Forms.Panel _weekPremierePanel;
        private readonly int ItemHeight = 150;
        private readonly int InfoPanelWidth = 400;      

        public event Action<LinkLabel.Link> LinkClicked;

        public WeekPremiere(System.Windows.Forms.Panel panel)
        {
            _weekPremierePanel = panel;
        }

        private void AddLink(LinkLabel label, string url)
        {
            LinkLabel.Link link = new LinkLabel.Link();
            link.LinkData = url;
            label.Links.Add(link);
        }
        private void OnLinkClicked(object e, LinkLabelLinkClickedEventArgs args)
        {
            LinkClicked?.Invoke(args.Link);
        }

        public void Clear()
        {
            foreach (Control item in _weekPremierePanel.Controls) item.Dispose();
            _weekPremierePanel.Controls.Clear();
        }
        public void OnGUI(DataTable source)
        {
            if (source == null || source.Rows.Count == 0) return;

            for (int i = source.Rows.Count - 1; i >= 0; i--)
            {
                #region Info Panel
                System.Windows.Forms.Panel infoPanel = new System.Windows.Forms.Panel();
                infoPanel.Dock = DockStyle.Left;
                infoPanel.Width = InfoPanelWidth;
                infoPanel.BorderStyle = BorderStyle.FixedSingle;

                Label descriptionLabel = new Label();
                descriptionLabel.Text = Extensions.GetCellByName("Описание", source.Rows[i]);
                descriptionLabel.Dock = DockStyle.Fill;
                descriptionLabel.TextAlign = ContentAlignment.MiddleLeft;
                descriptionLabel.Font = Fonts.ArialRegular;
                descriptionLabel.ForeColor = Color.Gray;
                descriptionLabel.BringToFront();
                infoPanel.Controls.Add(descriptionLabel);

                Label countryDirectorGenreLabel = new Label();
                string country = Extensions.GetCellByName("Страна", source.Rows[i]);
                string director = Extensions.GetCellByName("Режиссёр", source.Rows[i]);
                string genre = "(" + Extensions.GetCellByName("Жанр фильма", source.Rows[i]) + ")";
                countryDirectorGenreLabel.Text = country + ", реж. " + director + "\n" + genre;
                countryDirectorGenreLabel.Dock = DockStyle.Top;
                countryDirectorGenreLabel.TextAlign = ContentAlignment.MiddleLeft;
                countryDirectorGenreLabel.Font = Fonts.ArialRegular;
                countryDirectorGenreLabel.ForeColor = Color.Gray;
                countryDirectorGenreLabel.AutoSize = true;
                countryDirectorGenreLabel.Height = 40;
                countryDirectorGenreLabel.BringToFront();
                infoPanel.Controls.Add(countryDirectorGenreLabel);

                LinkLabel nameLabel = new LinkLabel();
                nameLabel.Text = Extensions.GetCellByName("Название", source.Rows[i]);
                nameLabel.Dock = DockStyle.Top;
                nameLabel.TextAlign = ContentAlignment.MiddleLeft;
                nameLabel.Font = Fonts.ArialRegular;
                nameLabel.ForeColor = Color.DarkBlue;
                AddLink(nameLabel, "http://google.com");
                nameLabel.LinkClicked += OnLinkClicked;
                nameLabel.BringToFront();
                infoPanel.Controls.Add(nameLabel);
                #endregion

                #region Rating Panel
                System.Windows.Forms.Panel ratingPanel = new System.Windows.Forms.Panel();
                ratingPanel.Dock = DockStyle.Left;
                ratingPanel.BorderStyle = BorderStyle.FixedSingle;

                Button button = new Button();
                button.Dock = DockStyle.Bottom;
                button.Text = "Оценить";
                button.Size = new Size((int)(ratingPanel.Width * 0.9f), 20);
                button.TabStop = false;
                ratingPanel.Controls.Add(button);

                Label noteLabel = new Label();
                noteLabel.Text = "✯✯✯✯✯ " + Extensions.GetCellByName("Рейтинг", source.Rows[i]);
                noteLabel.Dock = DockStyle.Top;
                noteLabel.TextAlign = ContentAlignment.MiddleCenter;
                noteLabel.ForeColor = Color.Orange;
                noteLabel.Font = new Font("Arial", 12, FontStyle.Bold);
                noteLabel.BringToFront();
                ratingPanel.Controls.Add(noteLabel);

                Label ratingLabel = new Label();
                ratingLabel.Text = "Рейтинг фильма:";
                ratingLabel.Dock = DockStyle.Top;
                ratingLabel.TextAlign = ContentAlignment.MiddleCenter;
                ratingLabel.BringToFront();
                ratingPanel.Controls.Add(ratingLabel);
                #endregion

                #region Date
                System.Windows.Forms.Panel datePanel = new System.Windows.Forms.Panel();
                datePanel.Dock = DockStyle.Fill;
                datePanel.BorderStyle = BorderStyle.FixedSingle;

                DateTime date = Extensions.GetCellByName<DateTime>("Дата премьеры", source.Rows[i]);
                Label monthLabel = new Label();
                monthLabel.Text = Extensions.ParseMonth(date.Month);
                monthLabel.Text = monthLabel.Text.Remove(monthLabel.Text.Length - 1, 1);
                monthLabel.Text = monthLabel.Text.Insert(monthLabel.Text.Length, "я");
                monthLabel.Dock = DockStyle.Top;
                monthLabel.TextAlign = ContentAlignment.MiddleCenter;
                monthLabel.Font = Fonts.TimesNewRomanItalic;
                monthLabel.BringToFront();
                datePanel.Controls.Add(monthLabel);

                Label dayLabel = new Label();
                dayLabel.Text = date.Day.ToString();
                dayLabel.Dock = DockStyle.Top;
                dayLabel.TextAlign = ContentAlignment.MiddleCenter;
                dayLabel.Font = Fonts.NSimSunBold;
                dayLabel.Height = 45;
                dayLabel.BringToFront();
                datePanel.Controls.Add(dayLabel);
                #endregion

                System.Windows.Forms.Panel itemPanel = new System.Windows.Forms.Panel();
                itemPanel.Dock = DockStyle.Top;
                itemPanel.Height = ItemHeight;
                itemPanel.BorderStyle = BorderStyle.FixedSingle;

                itemPanel.Controls.Add(datePanel);
                itemPanel.Controls.Add(ratingPanel);
                itemPanel.Controls.Add(infoPanel);

                _weekPremierePanel.Controls.Add(itemPanel);
            }
        }
    }
}