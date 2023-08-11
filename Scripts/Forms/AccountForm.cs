using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Core.Query;

namespace Core.UI.Forms
{
    public partial class AccountForm : Form
    {
        private User User { set; get; }

        #region Variables

        #region Event Handlers
        private FilmCollection FilmCollection;
        private EventHandler collectionAddAction;
        private EventHandler collectionSortAction;   
        #endregion

        #region Dropdowns
        Dropdown ProfileDropdown;
        #endregion

        #region TextFields
        TextField MainSearchTextField { set; get; }
        TextField PersonsSearchTextField { set; get; }
        TextField FilmsSearchTextField { set; get; }
        TextField FilmsSearchDirectorTextField { set; get; }
        TextField AuthorizationTextField { set; get; }
        #endregion

        #region TabControl
        UI.TabControl MainTabControl { set; get; }
        #endregion

        #region MultiListBoxes
        MultiListBox MainContentListBox { set; get; }
        #endregion

        #region CustomListBoxes
        CustomListBox<TextField> PersonsSearchFilms { set; get; }
        CustomListBox<TextField> PersonsSearchGenres { set; get; }
        CustomListBox<TextField> FilmsSearchGenres { set; get; }
        CustomListBox<TextField> FilmsSearchActing { set; get; }
        #endregion

        private WeekPremiere _weekPremiere;

        #endregion

        public AccountForm(User moderator)
        {
            InitializeComponent();
            User = moderator;

            FilmCollection = new FilmCollection(User, filmsSearchDataGrid, collectionDataGrid);
            collectionAddAction = (obj, e) =>
            {
                DataGridView view = obj as DataGridView;
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

                collectionSortAction(obj, e);
            };
            collectionSortAction = async (obj, e) =>
            {
                DataGridView view = obj as DataGridView;
                foreach (DataGridViewRow row in view.Rows)
                {
                    row.HeaderCell.Value = (row.Index + 1).ToString();

                    if (row.Cells.Count != 10) continue;
                    if (await Query.User.ContainsFilmAsync(User.UserID, (int)row.Cells[0].Value))
                    {
                        row.Cells[row.Cells.Count - 1].Style = Styles.CollectionRemoveCellStyle;
                        row.Cells[row.Cells.Count - 1].Value = "–";
                    }
                }
                view.ReadOnly = true;
            };          
        }

        public void UpdateProfileInfo(string firstName, string name, string secondName)
        {            
            string fio = string.Join(" ", firstName, name, secondName);
            User.FIO = fio;
            FIOLabel.Text = fio;
        }

        private void ClearGridView(DataGridView view)
        {      
            view.DataSource = null;   
            if (view.RowCount > 0) view.Rows.Clear();
            if (view.ColumnCount > 0) view.Columns.Clear();
        }
       
        #region Form Events
        private void optionsButton_Click(object sender, EventArgs e)
        {
            UserInfo userInfo = new UserInfo
            {
                ID = User.UserID,
                FIO = User.FIO,
                Password = User.Password
            };

            Form form = new OptionsForm(userInfo);
            form.Show();
        }
        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {

        }
        private void dateTimeTimer_Tick(object sender, EventArgs e)
        {
            dateTimeLabel.Text = DateTime.Now.ToString();
        }
        #endregion
        private void Account_Load(object sender, EventArgs e)
        {
            #region Form Initialization
            #region Profile Info
            FIOLabel.Text = User.FIO;
            accessTypeLabel.Text = "Привилегия: " + User.AccessType.ToString();
            dateTimeLabel.Text = DateTime.Now.ToString();

            ProfileDropdown = new Dropdown(profileButton);
            ProfileDropdown.ItemWidth = 160;

            Button profileItem1 = new Button();
            profileItem1.FlatStyle = FlatStyle.System;
            profileItem1.TextAlign = ContentAlignment.MiddleLeft;
            profileItem1.Text = "Сменить пользователя";
            profileItem1.Click += (obj, args) =>
            {
                User.LogOut();
                Program.OpenForm(new Authorization());
                Program.CloseForm(this);
            };

            Button profileItem2 = new Button();
            profileItem2.FlatStyle = FlatStyle.System;
            profileItem2.TextAlign = ContentAlignment.MiddleLeft;
            profileItem2.Text = "Выйти";
            profileItem2.Click += (obj, args) => { Program.CloseForm(this); };

            ProfileDropdown.AddItem(profileItem1);
            ProfileDropdown.AddItem(profileItem2);

            switch (User.AccessType)
            {
                case AccessType.Гость:
                    globalTabControl.TabPages.RemoveAt(1);
                    break;
                case AccessType.Модератор:
                    globalTabControl.TabPages.RemoveAt(1);
                    personsSearchModeratorPanel.Visible = true;
                    filmsSearchModeratorPanel.Visible = true;
                    break;
                case AccessType.Администратор:
                    personsSearchModeratorPanel.Visible = true;
                    filmsSearchModeratorPanel.Visible = true;
                    break;
            }
            #endregion

            #region Main Search TextField
            MainSearchTextField = new TextField(mainSearchTextBox);
            MainSearchTextField.DefaultText = "Фильмы, сериалы, персоны";
            MainSearchTextField.ResetText();
            MainSearchTextField.SetIcon(Resources.Icons.Search, HorizontalAlignment.Right);
            #endregion

            _weekPremiere = new WeekPremiere(premiereWeekValuesPanel);
            _weekPremiere.LinkClicked += (link) =>
            {
                var url = Extensions.ParseVideoURL(link.LinkData.ToString());
                MainTabControl.OpenTab("Видео");
                webBrowser1.Url = new Uri(link.LinkData.ToString());
            };

            InitilizeModeratorPage();
            InitilizeAdministratorPage();
            #endregion

            #region Pages

            #region 1] Main Page
            #region Content Tab Control
            MainTabControl = new UI.TabControl(mainTabControl, false);
            MainTabControl.SelectedItemChanged += async (tab) =>
            {
                if (MainTabControl.SelectedItem != tab) return;

                switch (tab)
                {
                    case "График премьер":
                        _weekPremiere.Clear();                        
                        _weekPremiere.OnGUI(await Film.WeekPremiereAsync());                        
                        break;
                    case "Списки фильмов":
                        filmsSearchPremiereDataGrid.DataSource = await Film.WeekPremiereAsync();
                        break;
                    case "Топ 100":
                        topFilmsDataGrid.DataSource = await Film.TopFilmsAsync(100);
                        break;
                    case "Коллекция":
                        collectionDataGrid.DataSource = await Query.User.CollectionFilmsAsync(User.CollectionID);
                        break;
                } 
            };

            MainContentListBox = new MultiListBox();
            MainContentListBox.AddList(mediaContentListBox);
            MainContentListBox.AddList(posterContentListBox);
            MainContentListBox.AddList(filmsContentListBox);
            MainContentListBox.AddList(userCollectionsListBox);
            MainContentListBox.SelectedIndexChanged += () => MainTabControl.OpenTab(MainContentListBox.SelectedItem);
            #endregion

            #region Content Pages
            #region 1:
            #endregion

            #region 2:
            #endregion

            #region 3:
            #endregion

            #region 4: Persons
            #region Search
            PersonsSearchTextField = new TextField(personsSearchTextBox);
            PersonsSearchTextField.DefaultText = "Введите персону, страну и т.д.";
            PersonsSearchTextField.ResetText();
            PersonsSearchTextField.SetIcon(Resources.Icons.Search, HorizontalAlignment.Right);

            personsSearchDataGrid.Sorted += (obj, args) => personsSearchDataGrid.Enumerate();
            personsSearchDataGrid.DataSourceChanged += (obj, args) => personsSearchDataGrid.Enumerate();
            personsSearchDataGrid.SelectionChanged += (obj, args) =>
            {
                if (personsSearchDataGrid.SelectedRows.Count == 0) return;

                personsSearchRemoveBtn.Enabled = true;
                personsSearchModifyBtn.Enabled = true;
            };
            personsSearchDataGrid.DataSourceChanged += (obj, args) =>
            {
                if (personsSearchDataGrid.SelectedRows.Count == 0)
                {
                    personsSearchRemoveBtn.Enabled = false;
                    personsSearchModifyBtn.Enabled = false;
                }
            };

            personsSearchFoundFilmsDataGrid.Sorted += (obj, args) => personsSearchFoundFilmsDataGrid.Enumerate();
            personsSearchFoundFilmsDataGrid.DataSourceChanged += (obj, args) => personsSearchFoundFilmsDataGrid.Enumerate();
            #endregion

            #region Advanced Search
            personsSearchCountryComboBox.SelectedIndex = 0;
            personsSearchBirthdayComboBox.SelectedIndex = 0;

            #region Film
            PersonsSearchFilms = new CustomListBox<TextField>(personsSearchFilmsValuesPanel, 4);

            EventHandler personsSearchFilmAddAction = (obj, args) =>
            {
                TextField item = PersonsSearchFilms.Instantiate();
                if (item == default) return;
                item.IconEnabled = true;
                item.IconHiddenWhileFocused = false;
                item.SetIcon(Resources.Icons.AddItem, HorizontalAlignment.Right);
                item.DefaultText = "Введите фильм";
                item.ResetText();
                PersonsSearchFilms.AddItem(item);
            };
            EventHandler personsSearchFilmRemoveAction = (obj, args) =>
            {
                TextField item = BaseUIElement.GetParent(obj as Control) as TextField;
                PersonsSearchFilms.RemoveItem(item);
            };

            PersonsSearchFilms.ItemAdded += (obj, args) =>
            {
                PersonsSearchFilms.LastItem.Click += personsSearchFilmAddAction;

                if (PersonsSearchFilms.ItemsCount == 1) return;
                PersonsSearchFilms.SecondLastItem.Click -= personsSearchFilmAddAction;
                PersonsSearchFilms.SecondLastItem.Click += personsSearchFilmRemoveAction;
                PersonsSearchFilms.SecondLastItem.SetIcon(Resources.Icons.RemoveItem, HorizontalAlignment.Right);

                if (PersonsSearchFilms.ItemsCount + 1 <= PersonsSearchFilms.ItemsMaxCount) return;
                PersonsSearchFilms.LastItem.Click -= personsSearchFilmAddAction;
                PersonsSearchFilms.LastItem.Click += personsSearchFilmRemoveAction;
                PersonsSearchFilms.LastItem.SetIcon(Resources.Icons.RemoveItem, HorizontalAlignment.Right);
            };
            PersonsSearchFilms.ItemRemoved += (obj, args) =>
            {
                if (PersonsSearchFilms.LastItem.Icon == Resources.Icons.AddItem) return;
                PersonsSearchFilms.LastItem.SetIcon(Resources.Icons.AddItem, HorizontalAlignment.Right);
                PersonsSearchFilms.LastItem.Click -= personsSearchFilmRemoveAction;
                if (PersonsSearchFilms.ItemsCount + 1 == PersonsSearchFilms.ItemsMaxCount)
                    PersonsSearchFilms.LastItem.Click += personsSearchFilmAddAction;
            };

            TextField personsSearchFilmItem = new TextField();
            personsSearchFilmItem.IconEnabled = true;
            personsSearchFilmItem.IconHiddenWhileFocused = false;
            personsSearchFilmItem.DefaultText = "Введите фильм";
            personsSearchFilmItem.ResetText();
            personsSearchFilmItem.SetIcon(Resources.Icons.AddItem, HorizontalAlignment.Right);
            PersonsSearchFilms.AddItem(personsSearchFilmItem);
            #endregion

            #region Genre
            PersonsSearchGenres = new CustomListBox<TextField>(personsSearchGenresValuesPanel, 4);

            EventHandler personsSearchGenreAddAction = (obj, args) =>
            {
                TextField item = PersonsSearchGenres.Instantiate();
                if (item == default) return;
                item.IconEnabled = true;
                item.IconHiddenWhileFocused = false;
                item.SetIcon(Resources.Icons.AddItem, HorizontalAlignment.Right);
                item.DefaultText = "Введите жанр";
                item.ResetText();
                PersonsSearchGenres.AddItem(item);
            };
            EventHandler personsSearchGenreRemoveAction = (obj, args) =>
            {
                TextField item = BaseUIElement.GetParent(obj as Control) as TextField;
                PersonsSearchGenres.RemoveItem(item);
            };

            PersonsSearchGenres.ItemAdded += (obj, args) =>
            {
                PersonsSearchGenres.LastItem.Click += personsSearchGenreAddAction;

                if (PersonsSearchGenres.ItemsCount == 1) return;
                PersonsSearchGenres.SecondLastItem.Click -= personsSearchGenreAddAction;
                PersonsSearchGenres.SecondLastItem.Click += personsSearchGenreRemoveAction;
                PersonsSearchGenres.SecondLastItem.SetIcon(Resources.Icons.RemoveItem, HorizontalAlignment.Right);

                if (PersonsSearchGenres.ItemsCount + 1 <= PersonsSearchGenres.ItemsMaxCount) return;
                PersonsSearchGenres.LastItem.Click -= personsSearchGenreAddAction;
                PersonsSearchGenres.LastItem.Click += personsSearchGenreRemoveAction;
                PersonsSearchGenres.LastItem.SetIcon(Resources.Icons.RemoveItem, HorizontalAlignment.Right);
            };
            PersonsSearchGenres.ItemRemoved += (obj, args) =>
            {
                if (PersonsSearchGenres.LastItem.Icon == Resources.Icons.AddItem) return;
                PersonsSearchGenres.LastItem.SetIcon(Resources.Icons.AddItem, HorizontalAlignment.Right);
                PersonsSearchGenres.LastItem.Click -= personsSearchGenreRemoveAction;
                if (PersonsSearchGenres.ItemsCount + 1 == PersonsSearchGenres.ItemsMaxCount)
                    PersonsSearchGenres.LastItem.Click += personsSearchGenreAddAction;
            };

            TextField personsSearchGenreItem = new TextField();
            personsSearchGenreItem.IconEnabled = true;
            personsSearchGenreItem.IconHiddenWhileFocused = false;
            personsSearchGenreItem.DefaultText = "Введите жанр";
            personsSearchGenreItem.ResetText();
            personsSearchGenreItem.SetIcon(Resources.Icons.AddItem, HorizontalAlignment.Right);
            PersonsSearchGenres.AddItem(personsSearchGenreItem);
            #endregion
            #endregion
            #endregion

            #region 5: Premiere
            premiereFutureDateComboBox.SelectedIndex = 0;
            premiereAllMonthsComboBox.SelectedIndex = 0;
            #endregion

            #region 7: Films
            #region Search
            FilmsSearchTextField = new TextField(filmsSearchTextBox);
            FilmsSearchTextField.DefaultText = "Введите фильм, жанр и т.д.";
            FilmsSearchTextField.ResetText();
            FilmsSearchTextField.SetIcon(Resources.Icons.Search, HorizontalAlignment.Right);

            filmsSearchDataGrid.DataSourceChanged += (obj, args) =>
            {
                if ((obj as DataGridView).SelectedRows.Count == 0)
                {
                    filmsSearchRemoveFilmBtn.Enabled = false;
                    filmsSearchModifyBtn.Enabled = false;
                }
            };
            filmsSearchDataGrid.SelectionChanged += (obj, args) =>
            {
                if (filmsSearchDataGrid.SelectedRows.Count == 0) return;

                filmsSearchRemoveFilmBtn.Enabled = true;
                filmsSearchModifyBtn.Enabled = true;    
            };          
            #endregion

            #region Advanced Search
            filmsSearchPremiereComboBox.SelectedIndex = 1;
            filmsSearchCountryComboBox.SelectedIndex = 0;
            filmsSearchRatingComboBox.SelectedIndex = 0;

            #region Genre
            FilmsSearchGenres = new CustomListBox<TextField>(filmsSearchGenresValuesPanel, 4);

            EventHandler filmsSearchGenreAddAction = (obj, args) =>
            {
                TextField item = FilmsSearchGenres.Instantiate();
                if (item == default) return;
                item.IconEnabled = true;
                item.IconHiddenWhileFocused = false;
                item.SetIcon(Resources.Icons.AddItem, HorizontalAlignment.Right);
                item.DefaultText = "Введите жанр";
                item.ResetText();
                FilmsSearchGenres.AddItem(item);
            };
            EventHandler filmSearchGenreRemoveAction = (obj, args) =>
            {
                TextField item = BaseUIElement.GetParent(obj as Control) as TextField;
                FilmsSearchGenres.RemoveItem(item);
            };

            FilmsSearchGenres.ItemAdded += (obj, args) =>
            {
                FilmsSearchGenres.LastItem.Click += filmsSearchGenreAddAction;

                if (FilmsSearchGenres.ItemsCount == 1) return;
                FilmsSearchGenres.SecondLastItem.Click -= filmsSearchGenreAddAction;
                FilmsSearchGenres.SecondLastItem.Click += filmSearchGenreRemoveAction;
                FilmsSearchGenres.SecondLastItem.SetIcon(Resources.Icons.RemoveItem, HorizontalAlignment.Right);

                if (FilmsSearchGenres.ItemsCount + 1 <= FilmsSearchGenres.ItemsMaxCount) return;
                FilmsSearchGenres.LastItem.Click -= filmsSearchGenreAddAction;
                FilmsSearchGenres.LastItem.Click += filmSearchGenreRemoveAction;
                FilmsSearchGenres.LastItem.SetIcon(Resources.Icons.RemoveItem, HorizontalAlignment.Right);
            };
            FilmsSearchGenres.ItemRemoved += (obj, args) =>
            {
                if (FilmsSearchGenres.LastItem.Icon == Resources.Icons.AddItem) return;
                FilmsSearchGenres.LastItem.SetIcon(Resources.Icons.AddItem, HorizontalAlignment.Right);
                FilmsSearchGenres.LastItem.Click -= filmSearchGenreRemoveAction;
                if (FilmsSearchGenres.ItemsCount + 1 == FilmsSearchGenres.ItemsMaxCount)
                    FilmsSearchGenres.LastItem.Click += filmsSearchGenreAddAction;
            };

            TextField filmsSearchGenreItem = new TextField();
            filmsSearchGenreItem.IconEnabled = true;
            filmsSearchGenreItem.IconHiddenWhileFocused = false;
            filmsSearchGenreItem.DefaultText = "Введите жанр";
            filmsSearchGenreItem.ResetText();
            filmsSearchGenreItem.SetIcon(Resources.Icons.AddItem, HorizontalAlignment.Right);
            FilmsSearchGenres.AddItem(filmsSearchGenreItem);
            #endregion

            #region Director
            FilmsSearchDirectorTextField = new TextField(filmsSearchDirectorTextBox);
            FilmsSearchDirectorTextField.DefaultText = "Введите режиссёра";
            FilmsSearchDirectorTextField.ResetText();
            #endregion

            #region Acting
            FilmsSearchActing = new CustomListBox<TextField>(filmsSearchActingValuesPanel, 4);

            EventHandler filmsSearchActingAddAction = (obj, args) =>
            {
                TextField item = FilmsSearchActing.Instantiate();
                if (item == default) return;
                item.IconEnabled = true;
                item.IconHiddenWhileFocused = false;
                item.SetIcon(Resources.Icons.AddItem, HorizontalAlignment.Right);
                item.DefaultText = "Введите актёра";
                item.ResetText();
                FilmsSearchActing.AddItem(item);
            };
            EventHandler filmsSearchActingRemoveAction = (obj, args) =>
            {
                TextField item = BaseUIElement.GetParent(obj as Control) as TextField;
                FilmsSearchActing.RemoveItem(item);
            };

            FilmsSearchActing.ItemAdded += (obj, args) =>
            {
                FilmsSearchActing.LastItem.Click += filmsSearchActingAddAction;

                if (FilmsSearchActing.ItemsCount == 1) return;
                FilmsSearchActing.SecondLastItem.Click -= filmsSearchActingAddAction;
                FilmsSearchActing.SecondLastItem.Click += filmsSearchActingRemoveAction;
                FilmsSearchActing.SecondLastItem.SetIcon(Resources.Icons.RemoveItem, HorizontalAlignment.Right);

                if (FilmsSearchActing.ItemsCount + 1 <= FilmsSearchActing.ItemsMaxCount) return;
                FilmsSearchActing.LastItem.Click -= filmsSearchActingAddAction;
                FilmsSearchActing.LastItem.Click += filmsSearchActingRemoveAction;
                FilmsSearchActing.LastItem.SetIcon(Resources.Icons.RemoveItem, HorizontalAlignment.Right);
            };
            FilmsSearchActing.ItemRemoved += (obj, args) =>
            {
                if (FilmsSearchActing.LastItem.Icon == Resources.Icons.AddItem) return;
                FilmsSearchActing.LastItem.SetIcon(Resources.Icons.AddItem, HorizontalAlignment.Right);
                FilmsSearchActing.LastItem.Click -= filmsSearchActingRemoveAction;
                if (FilmsSearchActing.ItemsCount + 1 == FilmsSearchActing.ItemsMaxCount)
                    FilmsSearchActing.LastItem.Click += filmsSearchActingAddAction;
            };

            TextField filmsSearchActingItem = new TextField();
            filmsSearchActingItem.IconEnabled = true;
            filmsSearchActingItem.IconHiddenWhileFocused = false;
            filmsSearchActingItem.DefaultText = "Введите актёра";
            filmsSearchActingItem.ResetText();
            filmsSearchActingItem.SetIcon(Resources.Icons.AddItem, HorizontalAlignment.Right);
            FilmsSearchActing.AddItem(filmsSearchActingItem);
            #endregion
            #endregion
            #endregion

            #region 8: Top 100
            topFilmsDataGrid.DataSourceChanged += (obj, args) => Extensions.Enumerate(topFilmsDataGrid, DataGridViewColumnSortMode.NotSortable);
            #endregion

            #endregion
            #endregion

            #endregion
        }

        #region Content Pages Events

        #region 1] Main Page
        #region 4: Persons
        private async void personsSearchBtn_Click(object sender, EventArgs e)
        {
            if (PersonsSearchTextField.Text.Length > 0)
                personsSearchQueryLabel.Text = PersonsSearchTextField.Text;
            else
            {
                personsSearchQueryLabel.Text = "Пусто";
                personsSearchDataGrid.DataSource = null;
                personsSearchFoundFilmsDataGrid.DataSource = null;
                personsSearchQueryCountLabel.Text = "0";
                return;
            }

            #region Persons
            ClearGridView(personsSearchDataGrid);
            personsSearchDataGrid.DataSource = await Actor.FindPersonsAsync(personsSearchTextBox.Text);
            #endregion

            #region Found Films
            ClearGridView(personsSearchFoundFilmsDataGrid);

            if (personsSearchDataGrid.Rows.Count > 0)
            {
                DataTable foundFilms = new DataTable();
                foreach (DataGridViewRow actor in personsSearchDataGrid.Rows)
                {
                    int id = await Actor.GetIDAsync(actor.Cells[0].Value.ToString());
                    DataTable temp = await Actor.GetActingFilmsAsync(id);
                    foundFilms.Merge(temp);
                }
                personsSearchFoundFilmsDataGrid.DataSource = foundFilms;
            }
            #endregion

            int queryCount = personsSearchDataGrid.Rows.Count + personsSearchFoundFilmsDataGrid.Rows.Count;
            personsSearchQueryCountLabel.Text = queryCount.ToString();
        }
        private async void personsAdvancedSearchApplyBtn_Click(object sender, EventArgs e)
        {
            #region Persons
            ActorAdvancedSearchArgs args = new ActorAdvancedSearchArgs
            {
                IncludeActing = PersonsSearchFilms.GetValues().Length > 0,
                Birthday = personsSearchBirthdayPicker.Value,
                Country = personsSearchCountryComboBox.Text,
                Genres = PersonsSearchGenres.GetValues(),
                Acting = PersonsSearchFilms.GetValues(),
                BirthdayComparison = (ComparisonType)personsSearchBirthdayComboBox.SelectedIndex
            };

            ClearGridView(personsSearchDataGrid);
            personsSearchDataGrid.DataSource = await Actor.AdvancedSearchAsync(args);
            #endregion

            #region Films
            ClearGridView(personsSearchFoundFilmsDataGrid);

            //if (personsSearchDataGrid.RowCount > 0)
            //{
            //    GenericAdvancedSearchArgs filmsArgs = new GenericAdvancedSearchArgs();
            //    filmsArgs.Join(Constants.Tables.ActorTable, Constants.Tables.FilmTable, Constants.Tables.ActingTable);

            //    // Outputing result columns
            //    filmsArgs.Output("Film.Title as Название");
            //    filmsArgs.Output("Acting.Role as Роль");
            //    filmsArgs.Output("Film.Director as Режиссёр");
            //    filmsArgs.Output("Film.Genre as [Жанр фильма]");               
            //    filmsArgs.Output("Film.Description as Описание");
            //    filmsArgs.Output("Film.PremiereDate as [Дата премьеры]");
            //    filmsArgs.Output("Film.Rating as Рейтинг");

            //    filmsArgs.AddCondition(string.Empty, "Actor.ActorID = Acting.ActorID AND Acting.FilmID = Film.FilmID");

            //    Dictionary<string, DataRow> result = new Dictionary<string, DataRow>();
            //    DataTable table = new DataTable();
            //    table.Columns.Add("Название");
            //    table.Columns.Add("Роль");
            //    table.Columns.Add("Режиссёр");
            //    table.Columns.Add("Жанр фильма");             
            //    table.Columns.Add("Описание");
            //    table.Columns.Add("Дата премьеры");
            //    table.Columns.Add("Рейтинг");
            //    foreach (DataGridViewRow row in personsSearchDataGrid.Rows)
            //    {
            //        filmsArgs.AddCondition("Actor.ActorID = ", row.Cells[0].Value.ToString());
            //        DataTable temp = await Generic.AdvancedSearch(filmsArgs);
            //        foreach (DataRow row2 in temp.Rows)
            //        {
            //            string name = row2.Field<string>("Название");
            //            if (!result.ContainsKey(name))
            //            {
            //                result.Add(name, row2);
            //                table.Rows.Add(row2.ItemArray);
            //            }
            //        }
            //        filmsArgs.RemoveCondition(filmsArgs.ConditionsCount - 1);
            //    }
            //    personsSearchFoundFilmsDataGrid.DataSource = table;
            //}
            #endregion

            personsSearchQueryCountLabel.Text = personsSearchDataGrid.Rows.Count.ToString();
        }
        private void personsSearchAddPersonBtn_Click(object sender, EventArgs e)
        {
            Form form = new PersonForm(TableItemAction.ADD);
            form.Show();
        }
        private void personsSearchModifyBtn_Click(object sender, EventArgs e)
        {
            Form form = new PersonForm((int)personsSearchDataGrid.SelectedRows[0].Cells[0].Value, personsSearchDataGrid.SelectedRows[0], TableItemAction.MODIFY);
            form.Show();
        }
        private async void personsSearchRemoveBtn_Click(object sender, EventArgs e)
        {
            if (personsSearchDataGrid.SelectedRows.Count == 0) return;

            int actorID = (int)personsSearchDataGrid.SelectedRows[0].Cells[0].Value;

            if (await Query.Actor.RemoveActor(actorID))
            {
                personsSearchDataGrid.Rows.RemoveAt(personsSearchDataGrid.SelectedRows[0].Index);
            }
        }
        #endregion

        #region 5: Films
        private async void filmsSearchBtn_Click(object sender, EventArgs e)
        {
            if (FilmsSearchTextField.Text.Length > 0)
                filmsSearchQueryLabel.Text = FilmsSearchTextField.Text;
            else
            {                
                filmsSearchQueryLabel.Text = "Пусто";           
                filmsSearchQueryCountLabel.Text = "0";
                ClearGridView(filmsSearchDataGrid);
                return;
            }

            filmsSearchDataGrid.DataSource = await Film.FindFilm(FilmsSearchTextField.Text);
            filmsSearchQueryCountLabel.Text = filmsSearchDataGrid.Rows.Count.ToString();
        }
        private async void filmsAdvancedSearchApplyBtn_Click(object sender, EventArgs e)
        {
            FilmAdvancedSearchArgs args = new FilmAdvancedSearchArgs
            {
                IncludeActing = FilmsSearchActing.GetValues().Length > 0,
                Country = filmsSearchCountryComboBox.Text,
                Genres = FilmsSearchGenres.GetValues(),
                Acting = FilmsSearchActing.GetValues(),
                PremiereDate = filmsSearchFromDatePicker.Value,
                Director = FilmsSearchDirectorTextField.Text,
                Rating = filmsSearchRatingNumeric.Value,
                PremiereDateComparison = (ComparisonType)filmsSearchPremiereComboBox.SelectedIndex,
                RatingComparison = (ComparisonType)filmsSearchRatingComboBox.SelectedIndex,
            };

            filmsSearchDataGrid.DataSource = await Film.AdvancedSearchAsync(args);
            filmsSearchQueryCountLabel.Text = filmsSearchDataGrid.Rows.Count.ToString();
        }
        private void filmsSearchAddFilmBtn_Click(object sender, EventArgs e)
        {
            Form form = new FilmForm(TableItemAction.ADD);
            form.Show();
        }
        private void filmsSearchModifyBtn_Click(object sender, EventArgs e)
        {
            FilmInfo filmInfo = new FilmInfo
            {
                FilmID = (int)filmsSearchDataGrid.SelectedRows[0].Cells[0].Value,
                Title =  filmsSearchDataGrid.SelectedRows[0].Cells[1].Value.ToString(),
                Director = filmsSearchDataGrid.SelectedRows[0].Cells[2].Value.ToString(),
                Genre = filmsSearchDataGrid.SelectedRows[0].Cells[3].Value.ToString(),
                Country = filmsSearchDataGrid.SelectedRows[0].Cells[4].Value.ToString(),
                Description = filmsSearchDataGrid.SelectedRows[0].Cells[5].Value.ToString(),
                PremiereDate = (DateTime)filmsSearchDataGrid.SelectedRows[0].Cells[6].Value,
                Rating = (decimal)filmsSearchDataGrid.SelectedRows[0].Cells[7].Value
            };
            Form form = new FilmForm(filmInfo, TableItemAction.MODIFY);
            form.Show();
        }
        private async void filmsSearchRemoveFilmBtn_Click(object sender, EventArgs e)
        {
            if (filmsSearchDataGrid.SelectedRows.Count == 0) return;

            int filmID = (int)filmsSearchDataGrid.SelectedRows[0].Cells[0].Value;
            if (await Query.Film.RemoveFilm(filmID))
            {
                filmsSearchDataGrid.Rows.RemoveAt(filmsSearchDataGrid.SelectedRows[0].Index);
            }
        }
        #endregion

        #endregion

        #endregion
    }
}