using System;
using System.Drawing;
using System.Windows.Forms;
using Core.Query;
using Core.Security;

namespace Core.UI.Forms
{
    public partial class Authorization : Form
    {
        private byte _failedTries = 0;

        TextField login, password;
        TextField firstName, name, secondName, loginRegistrate, passwordRegistrate;     

        public Authorization()
        {
            InitializeComponent();    
            Program.CurrentForm = this;
            
            login = new TextField(loginTextBox);
            login.DefaultText = "Логин";
            login.ResetText();
     
            password = new TextField(passwordTextBox);
            password.DefaultText = "Пароль";
            password.ResetText();

            //firstName = new TextField(firstNameTextBox);
            //firstName.DefaultText = "Введите фамилию";
            //firstName.ResetText();

            //name = new TextField(nameTextBox);
            //name.DefaultText = "Введите имя";
            //name.ResetText();

            //secondName = new TextField(secondNameTextBox);
            //secondName.DefaultText = "Введите отчество";
            //secondName.ResetText();

            //loginRegistrate = new TextField(loginRegistrateTextBox);
            //loginRegistrate.DefaultText = "Введите логин";
            //loginRegistrate.ResetText();

            //passwordRegistrate = new TextField(passwordRegistrateTextBox);
            //passwordRegistrate.DefaultText = "Введите пароль";
            //passwordRegistrate.ResetText();
        }

        #region Authorization Tab
        private async void autorizeButton_Click(object sender, EventArgs e)
        {
            if (_failedTries >= Constants.AuthorizationTriesMax)
            {
                int id = await Query.User.GetID(this.login.Text);
                if (id > 0)
                {
                    await Query.User.BlockUserAsync(id, 10, "Превышение кол-ва попыток авторизации.");
                }
                errorLabel.Text = "Вы превысили количество допустимых попыток.";
                return;
            }

            string login = this.login.Text;
            string password = this.password.Text;

            // Авторизация прошла успешно
            AuthorizationResultArgs authorizationResult = await Query.User.TryAuthorizeAsync(login, password);
            if (authorizationResult.IsAuthorized)
            {
                Program.OpenForm(new AccountForm(new User(authorizationResult)));
                Close();
                return;
            }

            // Авторизация не удалась
            _failedTries++;
            errorLabel.ForeColor = Color.Red;
            errorLabel.Visible = true;
            switch (authorizationResult.UserStatus)
            {
                case UserStatus.Заблокирован:
                    errorLabel.Text = "Ваша учетная запись заблокирована!";
                    break;
                default:
                    errorLabel.Text = "Неверный логин или пароль! Осталось попыток: " + (Constants.AuthorizationTriesMax - _failedTries + 1) + ".";
                    break;
            }
        }
        #endregion

        #region Registration Tab
        /// <summary>
        /// Добавление в базу данных нового пользователя.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void registerBtn_Click(object sender, EventArgs e)
        {
            if (Extensions.IsNullOrEmpty(name.Text, firstName.Text))
            {
                MessageBox.Show("Обязательные поля не могут быть пустыми.", "Регистрация", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Проверка пароля на минимальное соответствие требованиям
            IPasswordValidator validator = new PasswordValidator();
            if (!validator.IsValid(passwordRegistrate.Text))
            {
                MessageBox.Show("Пароль не соответствует минимальным требованиям безопасности.", "Регистрация", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Хэширование пароля
            IPasswordHasher hasher = new PasswordHasher(passwordRegistrate.Text);
            string hashedPassword = hasher.CreateHash(Constants.HashAlgorythm);

            // Запрос на процедуру добавления пользователя
            UserInfo userInfo = new UserInfo
            {
                Login = loginRegistrate.Text,
                Password = hashedPassword,
                FIO = string.Join(" ", firstName.Text, name.Text, secondName.Text),
                AccessType = AccessType.Гость,
                UserStatus = UserStatus.Свободен
            };
            await Query.User.AddUserAsync(userInfo);
        }
        #endregion
    }
}