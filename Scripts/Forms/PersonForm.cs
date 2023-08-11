using System.Windows.Forms;
using Core.Query;

namespace Core.UI.Forms
{
    public partial class PersonForm : Form
    {
        int ActorID { set; get; }
        TableItemAction Action { set; get; }

        TextField FirstNameField;
        TextField NameField;
        TextField SecondNameField;
        public PersonForm(int actorID, DataGridViewRow row, TableItemAction action)
        {
            InitializeComponent();
            ActorID = actorID;
            Action = action;

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

            string str = row.Cells[1].Value.ToString();
            string firstName = str.Substring(0, str.IndexOf(' '));
            str = str.Remove(0, str.IndexOf(' ') + 1);
            string name = str.Substring(0, str.IndexOf(' '));
            str = str.Remove(0, str.IndexOf(' ') + 1);
            string secondName = str.Substring(0, str.Length);

            actorIDNumeric.Value = (int)row.Cells[0].Value;
            countryComboBox.SelectedItem = row.Cells[3].Value.ToString();

            FirstNameField = new TextField(firstNameTextBox);
            FirstNameField.DefaultText = "Введите фамилию";
            FirstNameField.SetText(firstName);

            NameField = new TextField(nameTextBox);
            NameField.DefaultText = "Введите имя";
            NameField.SetText(name);

            SecondNameField = new TextField(secondTextBox);
            SecondNameField.DefaultText = "Введите отчество";
            SecondNameField.SetText(secondName);

            int index = row.Cells[2].Value.ToString().IndexOf('.');
            string date = row.Cells[2].Value.ToString().Substring(0, index + 1);
            datePicker.Value = System.DateTime.Parse(date);
        }
        public PersonForm(TableItemAction action)
        {
            InitializeComponent();
            Action = action;

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

            FirstNameField = new TextField(firstNameTextBox);
            FirstNameField.DefaultText = "Введите фамилию";
            FirstNameField.ResetText();

            NameField = new TextField(nameTextBox);
            NameField.DefaultText = "Введите имя";
            NameField.ResetText();

            SecondNameField = new TextField(secondTextBox);
            SecondNameField.DefaultText = "Введите отчество";
            SecondNameField.ResetText();
        }

        private void button_Click(object sender, System.EventArgs e)
        {
            if (NameField.Text == string.Empty || FirstNameField.Text == string.Empty)
            {
                MessageBox.Show("Обязательные поля не могут быть пустыми.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            switch (Action)
            {
                case TableItemAction.ADD:
                    Actor.AddActor(FirstNameField.Text + " " + NameField.Text + " " + SecondNameField.Text, datePicker.Value.ToShortDateString(), countryComboBox.Text);
                    break;
                case TableItemAction.MODIFY:
                    GenericAdvancedSearchArgs args = new GenericAdvancedSearchArgs();
                    args.Join("Actor");
                    args.Output("FIO = '" + FirstNameField.Text + " " + NameField.Text + " " + SecondNameField.Text + "'");
                    args.Output("Birthday = '" + datePicker.Value.ToShortDateString() + "'");
                    args.Output("Country = '" + countryComboBox.Text + "'");
                    args.AddCondition("ActorID = ", ActorID.ToString());
                    Generic.Update(args);
                    break;
            }
            Close();
        }
    }
}