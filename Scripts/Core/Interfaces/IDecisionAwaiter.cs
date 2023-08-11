using System.Threading.Tasks;

namespace Core.UI.Forms
{
    internal interface IDecisionAwaiter
    {
        Task<bool> AwaitForDecision();
        void SetTitle(string title);
        void SetDescription(string description);
    }
}