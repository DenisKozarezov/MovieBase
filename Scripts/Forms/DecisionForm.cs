using System.Threading.Tasks;
using System.Windows.Forms;

namespace Core.UI.Forms
{
    internal class DecisionForm : IDecisionAwaiter
    {
        private string _title;
        private string _description;
        private readonly MessageBoxIcon _messageType;
        private TaskCompletionSource<bool> _taskCompletionSource;

        private DecisionForm(string title, string description, MessageBoxIcon messageType)
        {
            _title = title;
            _description = description;       
            _messageType = messageType;
        }
        public static IDecisionAwaiter CreateForm(MessageBoxIcon messageType = MessageBoxIcon.Exclamation)
        {
            return new DecisionForm("Предупреждение", "Вы уверены в этом?", messageType);
        }

        public async Task<bool> AwaitForDecision()
        {
            _taskCompletionSource = new TaskCompletionSource<bool>();
            if (MessageBox.Show(_description, _title, MessageBoxButtons.YesNo, _messageType) == DialogResult.Yes)
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