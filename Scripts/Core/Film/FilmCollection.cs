using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Core.UI;

namespace Core
{
    internal class FilmCollection
    {
        private readonly int UserID;
        private readonly int CollectionID;
        private readonly DataGridView FilmReview;
        private readonly DataGridView Collection;

        public FilmCollection(User user, DataGridView filmReview, DataGridView collection)
        {
            UserID = user.UserID;
            CollectionID = user.CollectionID;   
            
            filmReview.Sorted += (obj, e) => filmReview.Enumerate();
            filmReview.DataSourceChanged += (obj, e) =>
            {
                ConcatColumns(filmReview);
                filmReview.Enumerate();
            };
            filmReview.CellDoubleClick += OnFilmReviewDoubleClicked;
            FilmReview = filmReview;

            collection.Sorted += (obj, e) => collection.Enumerate();
            collection.DataSourceChanged += (obj, e) =>
            {
                ConcatColumns(collection);
                collection.Enumerate();
            };
            collection.CellDoubleClick += OnCollectionDoubleClicked;
            Collection = collection;
        }

        private void ConcatColumns(DataGridView view)
        {
            DataTable source = view.DataSource as DataTable;

            if (source == null) return;
            view.Columns[0].Visible = false;

            DataColumn ratingColumn = new DataColumn("Оценить", typeof(string));
            ratingColumn.DefaultValue = "✯";
            source.Columns.Add(ratingColumn);

            DataGridViewColumn ratingGridColumn = view.Columns[view.Columns.Count - 1];
            ratingGridColumn.HeaderText = "";
            ratingGridColumn.DefaultCellStyle = Styles.FilmRatingStyle;
            ratingGridColumn.Width = 23;
            ratingGridColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            DataColumn collectionColumn = new DataColumn("Коллекция", typeof(string));
            collectionColumn.DefaultValue = "+";
            source.Columns.Add(collectionColumn);

            DataGridViewColumn collectionGridColumn = view.Columns[view.ColumnCount - 1];
            collectionGridColumn.HeaderText = "";
            collectionGridColumn.DefaultCellStyle = Styles.CollectionAddCellStyle;
            collectionGridColumn.Width = 23;
            collectionGridColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            view.ReadOnly = true;
        }
        private async void OnFilmReviewDoubleClicked(object sender, EventArgs args)
        {
            DataGridViewCell cell = FilmReview.SelectedCells[0];
            int filmID = (int)cell.OwningRow.Cells[0].Value;

            string name = cell.OwningColumn.Name;
            switch (name)
            {
                case "Оценить":
                    MessageBox.Show("123");
                    break;
                case "Коллекция":
                    if (!await Query.User.ContainsFilmAsync(UserID, filmID))
                    {
                        AddFilm(filmID);
                    }
                    else
                    {
                        RemoveFilm(filmID);
                    }
                    break;
            }
        }
        private void OnCollectionDoubleClicked(object sender, EventArgs args)
        {
            DataGridViewCell cell = Collection.SelectedCells[0];
            string name = cell.OwningColumn.Name;
            switch (name)
            {
                case "Оценить":
                    MessageBox.Show("123");
                    break;
                case "Коллекция":
                    Query.User.RemoveFilmFromCollection(CollectionID, (int)cell.OwningRow.Cells[0].Value);
                    Collection.Rows.RemoveAt(cell.RowIndex);
                    break;
            }
        }

        public void AddFilm(int filmID)
        {
            var cell = Extensions.GetCellByName("ID фильма", FilmReview.SelectedRows[0]);

            Query.User.AddFilmInCollection(CollectionID, filmID);
            cell.Style.BackColor = Color.DarkRed;
            cell.Value = "–";
        }
        public void RemoveFilm(int filmID)
        {
            var cell = Extensions.GetCellByName("ID фильма", FilmReview.SelectedRows[0]);

            Query.User.RemoveFilmFromCollection(CollectionID, filmID);
            cell.Style.BackColor = Color.DarkGreen;
            cell.Value = "+";
        }
    }
}