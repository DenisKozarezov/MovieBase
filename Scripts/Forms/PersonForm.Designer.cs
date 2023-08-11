namespace Core.UI.Forms
{
    partial class PersonForm
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
            this.actorIDNumeric = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.firstNameTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button = new System.Windows.Forms.Button();
            this.datePicker = new System.Windows.Forms.DateTimePicker();
            this.countryComboBox = new System.Windows.Forms.ComboBox();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.secondTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.actorIDNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID актёра";
            // 
            // actorIDNumeric
            // 
            this.actorIDNumeric.Enabled = false;
            this.actorIDNumeric.Location = new System.Drawing.Point(106, 23);
            this.actorIDNumeric.Maximum = new decimal(new int[] {
            -727379968,
            232,
            0,
            0});
            this.actorIDNumeric.Name = "actorIDNumeric";
            this.actorIDNumeric.Size = new System.Drawing.Size(68, 20);
            this.actorIDNumeric.TabIndex = 1;
            this.actorIDNumeric.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Фамилия*";
            // 
            // firstNameTextBox
            // 
            this.firstNameTextBox.Location = new System.Drawing.Point(106, 55);
            this.firstNameTextBox.MaxLength = 100;
            this.firstNameTextBox.Name = "firstNameTextBox";
            this.firstNameTextBox.Size = new System.Drawing.Size(149, 20);
            this.firstNameTextBox.TabIndex = 3;
            this.firstNameTextBox.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 155);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Дата рождения";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(57, 187);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Страна";
            // 
            // button
            // 
            this.button.Location = new System.Drawing.Point(85, 221);
            this.button.Name = "button";
            this.button.Size = new System.Drawing.Size(109, 23);
            this.button.TabIndex = 8;
            this.button.Text = "Добавить";
            this.button.UseVisualStyleBackColor = true;
            this.button.Click += new System.EventHandler(this.button_Click);
            // 
            // datePicker
            // 
            this.datePicker.Location = new System.Drawing.Point(106, 152);
            this.datePicker.Name = "datePicker";
            this.datePicker.Size = new System.Drawing.Size(149, 20);
            this.datePicker.TabIndex = 9;
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
            this.countryComboBox.Location = new System.Drawing.Point(106, 184);
            this.countryComboBox.Name = "countryComboBox";
            this.countryComboBox.Size = new System.Drawing.Size(149, 21);
            this.countryComboBox.TabIndex = 10;
            this.countryComboBox.TabStop = false;
            this.countryComboBox.Text = "Россия";
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(106, 90);
            this.nameTextBox.MaxLength = 100;
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(149, 20);
            this.nameTextBox.TabIndex = 12;
            this.nameTextBox.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(71, 93);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Имя*";
            // 
            // secondTextBox
            // 
            this.secondTextBox.Location = new System.Drawing.Point(106, 119);
            this.secondTextBox.MaxLength = 100;
            this.secondTextBox.Name = "secondTextBox";
            this.secondTextBox.Size = new System.Drawing.Size(149, 20);
            this.secondTextBox.TabIndex = 14;
            this.secondTextBox.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(46, 122);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Отчество";
            // 
            // PersonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(285, 256);
            this.Controls.Add(this.secondTextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.countryComboBox);
            this.Controls.Add(this.datePicker);
            this.Controls.Add(this.button);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.firstNameTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.actorIDNumeric);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PersonForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Добавить";
            ((System.ComponentModel.ISupportInitialize)(this.actorIDNumeric)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown actorIDNumeric;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox firstNameTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button;
        private System.Windows.Forms.DateTimePicker datePicker;
        private System.Windows.Forms.ComboBox countryComboBox;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox secondTextBox;
        private System.Windows.Forms.Label label6;
    }
}