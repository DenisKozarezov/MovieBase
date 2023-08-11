using System.Windows.Forms;
using Core.Query;

namespace Core.UI.Forms
{
    public partial class UserForm : Form
    {
        private readonly TableItemAction Action;

        TextField LoginField;
        TextField PasswordField;
        TextField FirstNameField;
        TextField NameField;
        TextField SecondNameField;

        public UserForm(UserInfo userInfo, TableItemAction action)
        {
            InitializeComponent();
            InitializeForm(action);
            Action = action;

            string[] fioParts = userInfo.FIO.Split(' ');

            userIDNumeric.Value = userInfo.ID;
            LoginField.SetText(userInfo.Login);
            PasswordField.SetText(userInfo.Password);
            NameField.SetText(fioParts[1]);
            FirstNameField.SetText(fioParts[0]);
            SecondNameField.SetText(fioParts[2]);
        }
        public UserForm(TableItemAction action)
        {
            InitializeComponent();
            InitializeForm(action);
            Action = action;        
        }
        private void InitializeForm(TableItemAction action)
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

            LoginField = new TextField(loginTextBox);
            LoginField.DefaultText = "Введите логин";
            LoginField.ResetText();

            PasswordField = new TextField(passwordTextBox);
            PasswordField.DefaultText = "Введите имя";
            PasswordField.ResetText();

            NameField = new TextField(nameTextBox);
            NameField.DefaultText = "Введите имя";
            NameField.ResetText();

            FirstNameField = new TextField(secondNameTextBox);
            FirstNameField.DefaultText = "Введите фамилию";
            FirstNameField.ResetText();

            SecondNameField = new TextField(secondNameTextBox);
            SecondNameField.DefaultText = "Введите отчество";
            SecondNameField.ResetText();
        }

        private async void button_Click(object sender, System.EventArgs e)
        {
            if (Extensions.IsNullOrEmpty(LoginField.Text, NameField.Text, FirstNameField.Text))
            {
                MessageBox.Show("Обязательные поля не могут быть пустыми.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            UserInfo userInfo = new UserInfo
            {
                ID = (int)userIDNumeric.Value,
                Login = LoginField.Text,
                Password = PasswordField.Text,
                FIO = string.Join(" ", FirstNameField.Text, NameField.Text, SecondNameField.Text),
                AccessType = AccessType.Гость,
                UserStatus = UserStatus.Свободен
            };
            switch (Action)
            {
                case TableItemAction.ADD:                  
                    await Query.User.AddUserAsync(userInfo);
                    break;
                case TableItemAction.MODIFY:
                    await Query.User.UpdateAsync(userInfo);
                    break;
            }
            Close();
        }
    }
}