using System;

namespace Core.UI
{
    public interface IInteractable
    {
        event EventHandler Click;
        object GetValue();
    }
}