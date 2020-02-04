using UnityEngine;
using Liftables;

namespace Furniture
{
    public class TrashBox : BaseFurniture
    {
        public override bool PutDownObject(LiftableObject liftable)
        {
            if (liftable is CookingPot)
            {
                (liftable as CookingPot).Empty();
            }
            else if (liftable is Plate)
            {
                (liftable as Plate).Empty();
            }
            else if (liftable is Ingredient)
            {
                Destroy(liftable.gameObject);
                return true;
            }
            return false;
        }
    }
}
