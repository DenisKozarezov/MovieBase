using System.Threading.Tasks;
using System.Windows.Forms;

namespace Core.UI.Forms
{
    internal class ConfirmForm : IConfirmAwaiter
    {
        private string _title;
        private string _description;
        private readonly MessageBoxIcon _messageType;
        private TaskCompletionSource<bool> _taskCompletionSource;

        private ConfirmForm(string title, string description, MessageBoxIcon messageType)
        {
            _title = title;
            _description = description;
            _messageType = messageType;
        }
        public static IConfirmAwaiter CreateForm(MessageBoxIcon messageType = MessageBoxIcon.Information)
        {
            return new ConfirmForm("Информация", "Здесь вы видите очень важную информацию.", messageType);
        }

        public async Task<bool> AwaitForConfirm()
        {
            _taskCompletionSource = new TaskCompletionSource<bool>();
            if (MessageBox.Show(_description, _title, MessageBoxButtons.OK, _messageType) == DialogResult.OK)
            {
                _taskCompletionSource.SetResult(true);
            }
            else _taskCompletionSource.SetResult(false);
            return await _taskCompletionSource.Task;
        }

        public void SetDescription(string description)
        {
            _description = description;
        }

        public void SetTitle(string title)
        {
            _title = title;
        }
    }
}