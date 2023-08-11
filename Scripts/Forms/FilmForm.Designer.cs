namespace Core.UI.Forms
{
    partial class FilmForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.titleTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.directorTextBox = new System.Windows.Forms.TextBox();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.ratingNumeric = new System.Windows.Forms.NumericUpDown();
            this.datePicker = new System.Windows.Forms.DateTimePicker();
            this.countryComboBox = new System.Windows.Forms.ComboBox();
            this.genreComboBox = new System.Windows.Forms.ComboBox();
            this.button = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.filmIDNumeric = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.ratingNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.filmIDNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Название*";
            // 
            // titleTextBox
            // 
            this.titleTextBox.Location = new System.Drawing.Point(100, 39);
            this.titleTextBox.MaxLength = 100;
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new System.Drawing.Size(203, 20);
            this.titleTextBox.TabIndex = 1;
            this.titleTextBox.TabStop = false;
            this.titleTextBox.Text = "Новый фильм";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Режиссёр";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(59, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Жанр";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(52, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Страна";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(38, 146);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Описание";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 253);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Дата премьеры";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(51, 281);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Рейтинг";
            // 
            // directorTextBox
            // 
            this.directorTextBox.Location = new System.Drawing.Point(100, 65);
            this.directorTextBox.MaxLength = 100;
            this.directorTextBox.Name = "directorTextBox";
            this.directorTextBox.Size = new System.Drawing.Size(203, 20);
            this.directorTextBox.TabIndex = 8;
            this.directorTextBox.TabStop = false;
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.Location = new System.Drawing.Point(100, 143);
            this.descriptionTextBox.MaxLength = 200;
            this.descriptionTextBox.Multiline = true;
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.Size = new System.Drawing.Size(203, 104);
            this.descriptionTextBox.TabIndex = 11;
            this.descriptionTextBox.TabStop = false;
            this.descriptionTextBox.Text = "...";
            // 
            // ratingNumeric
            // 
            this.ratingNumeric.DecimalPlaces = 1;
            this.ratingNumeric.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.ratingNumeric.Location = new System.Drawing.Point(100, 279);
            this.ratingNumeric.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.ratingNumeric.Name = "ratingNumeric";
            this.ratingNumeric.Size = new System.Drawing.Size(61, 20);
            this.ratingNumeric.TabIndex = 13;
            this.ratingNumeric.TabStop = false;
            // 
            // datePicker
            // 
            this.datePicker.Location = new System.Drawing.Point(100, 253);
            this.datePicker.Name = "datePicker";
            this.datePicker.Size = new System.Drawing.Size(123, 20);
            this.datePicker.TabIndex = 14;
            this.datePicker.TabStop = false;
            // 
            // countryComboBox
            // 
            this.countryComboBox.FormattingEnabled = true;
            this.countryComboBox.Items.AddRange(new object[] {
            "Австралия",
            "Австрия",
            "Азербайджан",
            "Албания",
            "Алжир",
            "Ангола",
            "Андорра",
            "Антигуа и Барбуда",
            "Аргентина",
            "Армения",
            "Афганистан",
            "Багамские Острова",
            "Бангладеш",
            "Барбадос",
            "Бахрейн",
            "Белоруссия",
            "Белиз",
            "Бельгия",
            "Бенин",
            "Болгария",
            "Боливия",
            "Босния и Герцеговина",
            "Ботсвана",
            "Бразилия",
            "Бруней",
            "Буркина-Фасо",
            "Бурунди",
            "Бутан",
            "Ватикан",
            "Вануату",
            "Великобритания",
            "Венгрия",
            "Венесуэла",
            "Восточный Тимор",
            "Вьетнам",
            "Габон",
            "Гаити",
            "Гайана",
            "Гамбия",
            "Гана",
            "Гватемала",
            "Гвинея",
            "Гвинея-Бисау",
            "Германия",
            "Гондурас",
            "Гренада",
            "Греция",
            "Грузия",
            "Дания",
            "Демократическая Республика Конго",
            "Джибути",
            "Доминика",
            "Доминиканская Республика",
            "Египет",
            "Замбия",
            "Зимбабве",
            "Израиль",
            "Индия",
            "Индонезия",
            "Иордания",
            "Ирак",
            "Иран",
            "Ирландия",
            "Исландия",
            "Испания",
            "Италия",
            "Йемен",
            "Кабо-Верде",
            "Казахстан",
            "Камбоджа",
            "Камерун",
            "Канада",
            "Катар",
            "Кения",
            "Кипр",
            "Кирибати",
            "Китай",
            "Колумбия",
            "Коморы",
            "Конго",
            "Корейская Народно-Демократическая Республика",
            "Коста-Рика",
            "Кот-д\'Ивуар",
            "Куба",
            "Кувейт",
            "Кыргызстан",
            "Лаос",
            "Латвия",
            "Лесото",
            "Либерия",
            "Ливан",
            "Ливия",
            "Литва",
            "Лихтенштейн",
            "Люксембург",
            "Маврикий",
            "Мавритания",
            "Мадагаскар",
            "Малави",
            "Малайзия",
            "Мали",
            "Мальдивы",
            "Мальта",
            "Марокко",
            "Маршалловы Острова",
            "Мексика",
            "Мозамбик",
            "Молдавия",
            "Монако",
            "Монголия",
            "Мьянма",
            "Намибия",
            "Науру",
            "Непал",
            "Нигер",
            "Нигерия",
            "Нидерланды",
            "Никарагуа",
            "Новая Зеландия",
            "Норвегия",
            "Объединённые Арабские Эмираты",
            "Оман",
            "Пакистан",
            "Палау",
            "Панама",
            "Папуа-Новая Гвинея",
            "Парагвай",
            "Перу",
            "Польша",
            "Португалия",
            "Россия",
            "Руанда",
            "Румыния",
            "Сальвадор",
            "Самоа",
            "Сан-Марино",
            "Сан-Томе и Принсипи",
            "Саудовская Аравия",
            "Северная Македония",
            "Сейшельские Острова",
            "Сенегал",
            "Сент-Винсент и Гренадины",
            "Сент-Китс и Невис",
            "Сент-Люсия",
            "Сербия",
            "Сингапур",
            "Сирия",
            "Словакия",
            "Словения",
            "Соединённое Королевство Великобритании и Северной Ирландии",
            "США",
            "Соломоновы Острова",
            "Сомали",
            "Судан",
            "Суринам",
            "Сьерра-Леоне",
            "Таджикистан",
            "Тайланд",
            "Танзания",
            "Того",
            "Тонга",
            "Тринидад и Тобаго",
            "Тувалу",
            "Тунис",
            "Туркменистан",
            "Турция",
            "Уганда",
            "Узбекистан",
            "Украина",
            "Уругвай",
            "Федеративные Штаты Микронезии",
            "Фиджи",
            "Филиппины",
            "Финляндия",
            "Франция",
            "Хорватия",
            "Центральноафриканская Республика",
            "Чад",
            "Черногория",
            "Чехия",
            "Чили",
            "Швейцария",
            "Швеция",
            "Шри-Ланка",
            "Эквадор",
            "Экваториальная Гвинея",
            "Эритрея",
            "Эсватини",
            "Эстония",
            "Эфиопия",
            "Южная Корея",
            "Южно-Африканская Республика",
            "Южный Судан",
            "Ямайка",
            "Япония"});
            this.countryComboBox.Location = new System.Drawing.Point(100, 117);
            this.countryComboBox.Name = "countryComboBox";
            this.countryComboBox.Size = new System.Drawing.Size(203, 21);
            this.countryComboBox.TabIndex = 15;
            this.countryComboBox.TabStop = false;
            // 
            // genreComboBox
            // 
            this.genreComboBox.FormattingEnabled = true;
            this.genreComboBox.Items.AddRange(new object[] {
            "аниме",
            "биография",
            "боевик",
            "вестерн",
            "военный",
            "детектив",
            "детский",
            "документальный",
            "драма",
            "исторический",
            "кинокомикс",
            "комедия",
            "концерт",
            "короткометражный",
            "криминал",
            "мелодрама",
            "мистика",
            "музыка",
            "мультфильм,",
            "мюзикл",
            "научный",
            "нуар",
            "приключения",
            "семейный",
            "спорт",
            "триллер",
            "ужасы",
            "фантастика",
            "фэнтези"});
            this.genreComboBox.Location = new System.Drawing.Point(101, 90);
            this.genreComboBox.Name = "genreComboBox";
            this.genreComboBox.Size = new System.Drawing.Size(203, 21);
            this.genreComboBox.TabIndex = 16;
            this.genreComboBox.TabStop = false;
            // 
            // button
            // 
            this.button.Location = new System.Drawing.Point(91, 314);
            this.button.Name = "button";
            this.button.Size = new System.Drawing.Size(155, 25);
            this.button.TabIndex = 17;
            this.button.Text = "ДОБАВИТЬ";
            this.button.UseVisualStyleBackColor = true;
            this.button.Click += new System.EventHandler(this.button_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(37, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "ID фильма";
            // 
            // filmIDNumeric
            // 
            this.filmIDNumeric.Enabled = false;
            this.filmIDNumeric.Location = new System.Drawing.Point(100, 14);
            this.filmIDNumeric.Maximum = new decimal(new int[] {
            1661992960,
            1808227885,
            5,
            0});
            this.filmIDNumeric.Name = "filmIDNumeric";
            this.filmIDNumeric.Size = new System.Drawing.Size(61, 20);
            this.filmIDNumeric.TabIndex = 19;
            this.filmIDNumeric.TabStop = false;
            // 
            // FilmForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 345);
            this.Controls.Add(this.filmIDNumeric);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.button);
            this.Controls.Add(this.genreComboBox);
            this.Controls.Add(this.countryComboBox);
            this.Controls.Add(this.titleTextBox);
            this.Controls.Add(this.datePicker);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ratingNumeric);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.descriptionTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.directorTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FilmForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Добавить фильм";
            ((System.ComponentModel.ISupportInitialize)(this.ratingNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.filmIDNumeric)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DateTimePicker datePicker;
        private System.Windows.Forms.NumericUpDown ratingNumeric;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.TextBox directorTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox titleTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox countryComboBox;
        private System.Windows.Forms.ComboBox genreComboBox;
        private System.Windows.Forms.Button button;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown filmIDNumeric;
    }
}