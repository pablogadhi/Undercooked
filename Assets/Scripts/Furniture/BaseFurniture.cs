using UnityEngine;
using Liftables;

namespace Furniture
{
    public abstract class BaseFurniture : MonoBehaviour
    {
        public int Id {get;set;}

        protected virtual void Start ()
        {
            Id = gameObject.GetInstanceID();
        }

        public abstract bool PutDownObject(LiftableObject liftable);
    }
}
