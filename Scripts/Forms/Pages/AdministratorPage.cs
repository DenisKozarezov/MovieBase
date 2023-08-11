using System;
using System.Windows.Forms;
using Core.Query;

namespace Core.UI.Forms
{
    // ======== ADMINISTRATOR PAGE ========
    public partial class AccountForm : Form
    {
        private TabControl AdminTabControl;
        private MultiListBox AdminContentListBox;
        private TextField BlockingTextField;
        private TextField AccountControlFIOTextField;

        private EventHandler adminDataGridAction;

        public void InitilizeAdministratorPage()
        {
            AdminContentListBox = new MultiListBox();
            AdminContentListBox.AddList(dataBaseContentListBox);
            AdminContentListBox.AddList(journalStateContentListBox);
            AdminContentListBox.AddList(communicationContentListBox);
            AdminContentListBox.SelectedIndexChanged += () => AdminTabControl.OpenTab(AdminContentListBox.SelectedItem);

            AdminTabControl = new UI.TabControl(adminTabControl, false);
            AdminTabControl.SelectedItemChanged += async (tab) =>
            {
                switch (tab)
                {
                    case "Управление учётной записью":
                        accountControlDataGrid.DataSource = await Query.User.UsersListAsync();
                        break;
                    case "Авторизация":
                        authorizationDataGrid.DataSource = await Query.User.AuthorizationListAsync();
                        break;
                    case "Блокировка":
                        blockingDataGrid.DataSource = await Query.User.BlockingListAsync();
                        break;
                }
            };
            adminDataGridAction = (obj, e) =>
            {
                var grid = obj as DataGridView;

                foreach (DataGridViewRow row in grid.Rows)
                {
                    row.HeaderCell.Value = (row.Index + 1).ToString();
                    if (row.Index >= grid.Rows.Count) return;

                    foreach (DataGridViewCell cell in row.Cells)
                    {                
                        switch (cell.OwningColumn.Name)
                        {
                            case "Уровень доступа":
                                User.TryParseAccessType(cell.Value.ToString(), out AccessType accessType);
                                if (accessType == AccessType.Администратор)
                                {
                                    row.Cells[cell.ColumnIndex].Style = Styles.AdminUserStyle;
                                }
                                break;
                            case "Статус":
                                User.TryParseUserStatus(cell.Value.ToString(), out UserStatus userStatus);
                                if (userStatus == UserStatus.Заблокирован)
                                {
                                    row.Cells[cell.ColumnIndex].Style = Styles.BlockedUserStyle;
                                }
                                break;
                        }
                    }
                }
                grid.ReadOnly = true;
            };                       

            #region Content Pages

            #region 1: Account Control
            AccountControlFIOTextField = new TextField(accountControlFIOTextBox);
            AccountControlFIOTextField.DefaultText = "Введите ФИО";
            AccountControlFIOTextField.ResetText();
            AccountControlFIOTextField.SetIcon(Resources.Icons.Search, HorizontalAlignment.Right);

            accountControlFilterAccessTypeComboBox.SelectedIndex = 0;
            accountControlFilterUserStatusComboBox.SelectedIndex = 0;
            accountControlAccessTypeComboBox.SelectedIndex = 0;

            accountControlAccessTypeModifyBtn.Click += (obj, args) =>
            {
                DataGridView dataGrid = accountControlDataGrid;
                GenericAdvancedSearchArgs advancedSearch = new GenericAdvancedSearchArgs();
                advancedSearch.Join("User");
                advancedSearch.Output("AccessType = '" + accountControlAccessTypeComboBox.SelectedItem.ToString() + "'");
                advancedSearch.AddCondition("UserID = ", Extensions.GetCellByName("ID пользователя", dataGrid.SelectedRows[0]).Value.ToString());
                Generic.Update(advancedSearch);

                var cell = Extensions.GetCellByName("Уровень доступа", dataGrid.SelectedRows[0]);
                cell.Value = accountControlAccessTypeComboBox.SelectedItem.ToString();
                User.TryParseAccessType(accountControlAccessTypeComboBox.SelectedItem.ToString(), out AccessType accessType);
                cell.Style = accessType == AccessType.Администратор ? Styles.AdminUserStyle : Styles.DefaultCellStyle;
            };
            accountControlModifyBtn.Click += (obj, args) =>
            {
                DataGridView dataGrid = accountControlDataGrid;
                dataGrid.ReadOnly = false;
                dataGrid.EditMode = DataGridViewEditMode.EditOnKeystroke;
                dataGrid.Columns[0].ReadOnly = true;
                dataGrid.Columns[dataGrid.ColumnCount - 1].ReadOnly = true;
                dataGrid.Columns[dataGrid.ColumnCount - 2].ReadOnly = true;
                foreach (DataGridViewRow row in dataGrid.Rows)
                {
                    if (row != dataGrid.SelectedRows[0]) row.ReadOnly = true;
                }
                dataGrid.SelectedRows[0].ReadOnly = false;
            };

            accountControlDataGrid.DataSourceChanged += adminDataGridAction;
            accountControlDataGrid.Sorted += adminDataGridAction;
            accountControlDataGrid.DataSourceChanged += (obj, args) => EnableAccountControl((obj as DataGridView).RowCount != 0);
            accountControlDataGrid.SelectionChanged += (obj, args) =>
            {
                if (accountControlDataGrid.SelectedRows.Count == 0) return;

                UserStatus userStatus;
                User.TryParseUserStatus(Extensions.GetCellByName("Статус", accountControlDataGrid.SelectedRows[0]).Value, out userStatus);
                AccessType accessType;
                User.TryParseAccessType(Extensions.GetCellByName("Уровень доступа", accountControlDataGrid.SelectedRows[0]).Value, out accessType);

                if (accessType == AccessType.Администратор)
                {
                    EnableAccountControl(false);
                    return;
                }
                EnableBlockButton(userStatus == UserStatus.Свободен);
            };
            #endregion

            #region 2: Authorization List
            AuthorizationTextField = new TextField(authorizationTextBox);
            AuthorizationTextField.DefaultText = "Введите ФИО";
            AuthorizationTextField.ResetText();
            AuthorizationTextField.SetIcon(Resources.Icons.Search, HorizontalAlignment.Right);

            authorizationAccessTypeComboBox.SelectedIndex = 0;
            authorizationUserStatusComboBox.SelectedIndex = 0;
            authorizationDateComboBox.SelectedIndex = 0;

            authorizationDataGrid.DataSourceChanged += adminDataGridAction;
            authorizationDataGrid.Sorted += adminDataGridAction;
            #endregion

            #region 3: Blocking List
            BlockingTextField = new TextField(blockingTextBox);
            BlockingTextField.DefaultText = "Введите ФИО";
            BlockingTextField.ResetText();
            BlockingTextField.SetIcon(Resources.Icons.Search, HorizontalAlignment.Right);

            blockingAccessTypeComboBox.SelectedIndex = 0;
            blockingDateComboBox.SelectedIndex = 0;

            blockingDataGrid.DataSourceChanged += adminDataGridAction;
            blockingDataGrid.Sorted += adminDataGridAction;
            #endregion

            #endregion
        }

        #region 1: Account Control
        private void EnableAccountControl(bool isEnabled)
        {
            accountControlAccessTypeModifyBtn.Enabled = isEnabled;
            accountControlUnblockBtn.Enabled = isEnabled;
            accountControlBlockBtn.Enabled = isEnabled;
            accountControlAddBtn.Enabled = isEnabled;
            accountControlRemoveBtn.Enabled = isEnabled;
            accountControlModifyBtn.Enabled = isEnabled;
        }
        private void EnableBlockButton(bool isEnabled)
        {
            accountControlBlockBtn.Enabled = isEnabled;
            accountControlUnblockBtn.Enabled = !isEnabled;
        }

        private void accountControlApplyBtn_Click(object sender, EventArgs e)
        {
            GenericAdvancedSearchArgs searchArgs = new GenericAdvancedSearchArgs();
            searchArgs.Join(Constants.Tables.UserTable);

            // Outputing result columns
            searchArgs.Output("UserID as [ID пользователя]");
            searchArgs.Output("Login as Логин");
            searchArgs.Output("Password as Пароль");
            searchArgs.Output("FIO as [ФИО пользователя]");
            searchArgs.Output("AccessType as [Уровень доступа]");
            searchArgs.Output("UserStatus as Статус");

            // AccessType
            if (accountControlFilterAccessTypeComboBox.Text != string.Empty)
                searchArgs.AddCondition("AccessType = '", accountControlFilterAccessTypeComboBox.Text, "'");

            // UserStatus
            if (accountControlFilterUserStatusComboBox.Text != string.Empty)
                searchArgs.AddCondition("UserStatus = '", accountControlFilterUserStatusComboBox.Text, "'");

            // FIO
            if (AccountControlFIOTextField.Text != string.Empty)
                searchArgs.AddCondition("FIO LIKE '%", AccountControlFIOTextField.Text, "%'");

            accountControlDataGrid.DataSource = Generic.AdvancedSearch(searchArgs);
        }
        private async void accountControlBlockBtn_Click(object sender, EventArgs e)
        {
            if (accountControlPeriodNumeric.Value == 0)
            {
                MessageBox.Show("Период блокировки не может быть равен нулю.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridView dataGrid = accountControlDataGrid;
            int userID = (int)Extensions.GetCellByName("ID пользователя", dataGrid.SelectedRows[0]).Value;
            int period = (int)accountControlPeriodNumeric.Value;
            string reason = accountControlReasonTextBox.Text;

            var form = Forms.DecisionForm.CreateForm(MessageBoxIcon.Warning);
            form.SetTitle("Блокировка пользователя");
            form.SetDescription("Данное действие невозможно отменить. Вы действительно хотите продолжить?");
            bool isConfirmed = await form.AwaitForDecision();
            if (!isConfirmed) return;

            if (await Query.User.BlockUserAsync(userID, period, reason))
            {
                var cell = Extensions.GetCellByName("Статус", dataGrid.SelectedRows[0]);
                cell.Style = Styles.BlockedUserStyle;
                cell.Value = UserStatus.Заблокирован.ToString();
                EnableBlockButton(false);
            }
        }
        private async void accountControlUnblockBtn_Click(object sender, EventArgs e)
        {
            DataGridView dataGrid = accountControlDataGrid;
            int userID = (int)Extensions.GetCellByName("ID пользователя", dataGrid.SelectedRows[0]).Value;

            var form = Forms.DecisionForm.CreateForm(MessageBoxIcon.Warning);
            form.SetTitle("Снятие блокировки");
            form.SetDescription("Данное действие невозможно отменить. Вы действительно хотите продолжить?");
            bool isConfirmed = await form.AwaitForDecision();
            if (!isConfirmed) return;

            if (await Query.User.UnblockUserAsync(userID))
            {
                var cell = Extensions.GetCellByName("Статус", dataGrid.SelectedRows[0]);
                cell.Style = Styles.DefaultCellStyle;
                cell.Value = UserStatus.Свободен.ToString();
                EnableBlockButton(true);
            }
        }
        private void accountControlAddBtn_Click(object sender, EventArgs e)
        {
            Form form = new UserForm(TableItemAction.ADD);
            form.Show();
        }
        private async void accountControlRemoveBtn_Click(object sender, EventArgs e)
        {
            DataGridView dataGrid = accountControlDataGrid;
            int userID = (int)Extensions.GetCellByName("ID пользователя", dataGrid.SelectedRows[0]).Value;

            var form = Forms.DecisionForm.CreateForm(MessageBoxIcon.Warning);
            form.SetTitle("Удаление пользователя");
            form.SetDescription("Данное действие невозможно отменить. Вы действительно хотите продолжить?");
            bool isConfirmed = await form.AwaitForDecision();

            if (isConfirmed && await Query.User.RemoveUserAsync(userID))
            {
                dataGrid.Rows.RemoveAt(dataGrid.SelectedRows[0].Index);
            }
        }
        private void accountControlModifyBtn_Click(object sender, EventArgs e)
        {
            AccessType accessType;
            User.TryParseAccessType(Extensions.GetCellByName("Уровень доступа", accountControlDataGrid.SelectedRows[0]).Value, out accessType);
            UserStatus userStatus;
            User.TryParseUserStatus(Extensions.GetCellByName("Статус", accountControlDataGrid.SelectedRows[0]).Value, out userStatus);

            UserInfo userInfo = new UserInfo
            {
                ID = (int)Extensions.GetCellByName("ID пользователя", accountControlDataGrid.SelectedRows[0]).Value,
                FIO = Extensions.GetCellByName("ФИО пользователя", accountControlDataGrid.SelectedRows[0]).Value.ToString(),
                Login = Extensions.GetCellByName("Логин", accountControlDataGrid.SelectedRows[0]).Value.ToString(),
                Password = Extensions.GetCellByName("Пароль", accountControlDataGrid.SelectedRows[0]).Value.ToString(),
                AccessType = accessType,
                UserStatus = userStatus
            };

            Form form = new UserForm(userInfo, TableItemAction.MODIFY);
            form.Show();
        }
        #endregion

        #region 2: Authorization
        private async void authorizationApplyBtn_Click(object sender, EventArgs e)
        {
            User.TryParseAccessType(authorizationAccessTypeComboBox.Text, out AccessType access);
            User.TryParseUserStatus(authorizationUserStatusComboBox.Text, out UserStatus status);
            UserAdvancedSearchArgs args = new UserAdvancedSearchArgs
            {
                FIO = AuthorizationTextField.Text,
                AccessType = access,
                UserStatus = status,
                DateTime = authorizationDatePicker.Value,
                DateComparison = (ComparisonType)authorizationDateComboBox.SelectedIndex
            };
            authorizationDataGrid.DataSource = await Query.User.AuthorizationListAsync(args);
        }
        #endregion

        #region 3: Blocking
        private async void blockingApplyBtn_Click(object sender, EventArgs e)
        {
            User.TryParseAccessType(blockingAccessTypeComboBox.SelectedItem, out AccessType accessType);
            UserAdvancedSearchArgs args = new UserAdvancedSearchArgs
            {
                FIO = BlockingTextField.Text,
                AccessType = accessType,
                DateTime = blockingDatePicker.Value,
                DateComparison = (ComparisonType)blockingDateComboBox.SelectedIndex
            };
            blockingDataGrid.DataSource = await Query.User.BlockingListAsync(args);
        }
        #endregion
    }
}