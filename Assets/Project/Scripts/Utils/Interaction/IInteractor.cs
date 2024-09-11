using System;

namespace Utils.Interaction
{
    public interface IInteractor
    {
        event Action OnInteracted;
        bool IsInteractable { get; }
        void Interact();
    }
}
