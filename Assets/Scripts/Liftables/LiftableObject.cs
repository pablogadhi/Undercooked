using UnityEngine;

namespace Liftables
{
    public abstract class LiftableObject : MonoBehaviour
    {
        public Vector3 offset;

        public Vector3 Offset
        {
            set
            {
                offset = value;
            }
            get
            {
                return offset;
            }
        }
    }
}
