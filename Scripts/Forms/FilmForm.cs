using System.Windows.Forms;
using Core.Query;

namespace Core.UI.Forms
{
    public partial class FilmForm : Form
    {
        TableItemAction Action { set; get; }

        TextField TitleField;
        TextField DirectorField;
        TextField DescriptionField;

        public FilmForm(FilmInfo filmInfo, TableItemAction action)
        {
            InitializeComponent();
            Action = action;

            InitilizeForm(action);
            ResetForm();

            filmIDNumeric.Value = filmInfo.FilmID; 
            TitleField.SetText(filmInfo.Title);
            DirectorField.SetText(filmInfo.Director);
            genreComboBox.Text = filmInfo.Genre;
            countryComboBox.Text = filmInfo.Country;
            DescriptionField.SetText(filmInfo.Description);
            datePicker.Value = filmInfo.PremiereDate;
            ratingNumeric.Value = filmInfo.Rating;
        }
        public FilmForm(TableItemAction action)
        {
            InitializeComponent();
            Action = action;

            InitilizeForm(action);
            ResetForm();
        }

        private void InitilizeForm(TableItemAction action)
        {
            switch (action)
            {
                case TableItemAction.ADD:
                    this.Text = "Добавить";
                    button.Text = "ДОБАВИТЬ";
                    break;
                case TableItemAction.MODIFY:
                    this.Text = "Изменить";
                    button.Text = "ИЗМЕНИТЬ";
                    break;
            }

            TitleField = new TextField(titleTextBox);
            DirectorField = new TextField(directorTextBox);
            DescriptionField = new TextField(descriptionTextBox);
        }
        private void ResetForm()
        {
            TitleField.DefaultText = "Введите название";
            TitleField.ResetText();

            DirectorField.DefaultText = "Введите режиссёра";
            DirectorField.ResetText();

            DescriptionField.DefaultText = "Введите описание";
            DescriptionField.ResetText();
        }

        private async void button_Click(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(TitleField.Text))
            {
                MessageBox.Show("Обязательные поля не могут быть пустыми.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            FilmInfo filmInfo = new FilmInfo
            {
                FilmID = (int)filmIDNumeric.Value,
                Title = TitleField.Text,
                Director = DirectorField.Text,
                Genre = genreComboBox.Text,
                Country = countryComboBox.Text,
                Description = DescriptionField.Text,
                PremiereDate = datePicker.Value,
                Rating = ratingNumeric.Value
            };

            switch (Action)
            {
                case TableItemAction.ADD:
                    await Film.AddFilmAsync(filmInfo);
                    break;
                case TableItemAction.MODIFY:
                    await Film.UpdateAsync(filmInfo);
                    break;
            }
            Close();
        }
    }
}