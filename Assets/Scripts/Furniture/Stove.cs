using UnityEngine;
using Liftables;

namespace Furniture
{
    public class Stove : Table
    {
        public delegate void TopObjectAction();
        public event TopObjectAction OnTopObjectPut;

        private float progress;
        private float burningProgress;
        private bool subscribed;

        protected override void  Start()
        {
            base.Start();
            (TopObject as CookingPot).SubscribeToStove(this);
            subscribed = true;
        }

        void Update()
        {
            if (TopObject != null && !(TopObject as CookingPot).IsEmpty() && subscribed)
            {
                OnTopObjectPut();
            }
        }

        public override bool PutDownObject(LiftableObject liftable)
        {
            if (TopObject == null && liftable is CookingPot)
            {
                (liftable as CookingPot).SubscribeToStove(this);
                subscribed = true;
                return base.PutDownObject(liftable);
            }
            else if (TopObject != null)
            {
                return base.PutDownObject(liftable);
            }
            return false;
        }

        public override LiftableObject LiftTopObject(Transform hands)
        {
            (TopObject as CookingPot).UnsubscribeToStove(this);
            subscribed = false;
            return base.LiftTopObject(hands);
        }
    }
}
