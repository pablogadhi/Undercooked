using UnityEngine;
using Player;

namespace Furniture
{
    public interface IActionFurniture
    {
        bool Interact();
        void SubscribeMethods(InteractingState state, Transform hands);
        void UnsubscribeMethods(InteractingState state);
    }
}
