using System.Windows.Forms;
using Core.Query;
using Core.Security;

namespace Core.UI.Forms
{
    public partial class PasswordChangeForm : Form
    {
        private readonly UserInfo UserInfo;
        private readonly TextField _currentPassword, _newPassword, _repeatPassword;

        public PasswordChangeForm(UserInfo userInfo)
        {
            InitializeComponent();
            this.UserInfo = userInfo;

            _currentPassword = new TextField(currentPasswordTextbox);
            _currentPassword.DefaultText = "Введите пароль";
            _currentPassword.ResetText();

            _newPassword = new TextField(newPasswordTextbox);
            _newPassword.DefaultText = "Введите пароль";
            _newPassword.ResetText();

            _repeatPassword = new TextField(repeatPasswordTextbox);
            _repeatPassword.DefaultText = "Введите пароль";
            _repeatPassword.ResetText();
        }

        private void cancelBtn_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private async void applyBtn_Click(object sender, System.EventArgs e)
        {
            if (Extensions.IsNullOrEmpty(_currentPassword.Text, _newPassword.Text, _repeatPassword.Text))
            {
                MessageBox.Show("Обязательные поля не могут быть пустыми.", "Регистрация", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (_currentPassword.Text != UserInfo.Password)
            {
                MessageBox.Show("Неверно указан текущий пароль.", "Смена пароля", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (_newPassword.Text == UserInfo.Password)
            {
                MessageBox.Show("Новый пароль не должен совпадать со старым.", "Смена пароля", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (_newPassword.Text != _repeatPassword.Text)
            {
                MessageBox.Show("Подтвердите пароль ещё раз.", "Смена пароля", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }           

            // Проверка пароля на минимальное соответствие требованиям
            IPasswordValidator validator = new PasswordValidator();
            if (!validator.IsValid(_newPassword.Text))
            {
                MessageBox.Show("Пароль не соответствует минимальным требованиям безопасности.", "Регистрация", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var form = Forms.DecisionForm.CreateForm();
            form.SetDescription("Данное действие невозможно отменить. Вы действительно хотите продолжить?");
            bool isConfirmed = await form.AwaitForDecision();

            if (isConfirmed && await Query.User.ChangePassword(UserInfo.ID, _newPassword.Text))
            {
                MessageBox.Show("Вы успешно сменили пароль.", "Смена пароля", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}