using System.Windows.Forms;
using Core.Query;

namespace Core.UI.Forms
{
    public partial class OptionsForm : Form
    {
        private readonly TextField _firstName, _name, _secondName;
        private readonly UserInfo _userInfo;

        public OptionsForm(UserInfo user)
        {
            InitializeComponent();

            _userInfo = user;
            string[] fioParts = user.FIO.Split(' ');

            this._firstName = new TextField(firstNameTextBox);
            this._firstName.DefaultText = "Введите фамилию";
            this._firstName.SetText(fioParts[0]);

            this._name = new TextField(nameTextBox);
            this._name.DefaultText = "Введите имя";
            this._name.SetText(fioParts[1]);

            this._secondName = new TextField(secondNameTextBox);
            this._secondName.DefaultText = "Введите отчество";
            this._secondName.SetText(fioParts[2]);
        }

        private async void applyBtn_Click(object sender, System.EventArgs e)
        {
            if (Extensions.IsNullOrEmpty(_firstName.Text, _name.Text))
            { 
                MessageBox.Show("Все обязательные поля должны быть заполнены.", this.Name, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            UserInfo userInfo = new UserInfo
            {
                ID = _userInfo.ID,
                FIO = string.Join(" ", _firstName.Text, _name.Text, _secondName.Text)
            };

            if (await Query.User.UpdateProfileInfo(userInfo))
            {
                MessageBox.Show("Операция успешна.", this.Name, MessageBoxButtons.OK, MessageBoxIcon.Information);
                (Program.CurrentForm as AccountForm).UpdateProfileInfo(_firstName.Text, _name.Text, _secondName.Text);
            }
        }
        private void passwordChangeLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var form = new PasswordChangeForm(_userInfo);
            form.Show();
        }
    }
}