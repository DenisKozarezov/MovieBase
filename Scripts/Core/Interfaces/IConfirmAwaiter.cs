using System.Threading.Tasks;

namespace Core.UI.Forms
{
    internal interface IConfirmAwaiter
    {
        Task<bool> AwaitForConfirm();
        void SetTitle(string title);
        void SetDescription(string description);
    }
}