using ICollidable = BurnoutBuster.Physics.ICollidable;

namespace BurnoutBuster.Items
{
    public interface IInteractable : ICollidable
    {
        void OnInteraction(IInteract subject);
    }
}
