using UnityEngine;

namespace Player
{
    public class IdleLiftState : PlayerState
    {
        public IdleLiftState(ChefBehaviour chef)
        {
            this.Chef = chef;
            Chef.Anim.SetBool("HasObject", true);
        }

        public override PlayerState HandleInput()
        {
            if (Input.GetButtonDown(Chef.GrabButton) && Chef.Liftable != null)
            {
                if (Chef.Liftable != null && Chef.FrontFurniture != null)
                {
                    bool didPut = Chef.FrontFurniture.PutDownObject(Chef.Liftable);
                    if (didPut)
                    {
                        Chef.Liftable = null;
                        Chef.Anim.SetBool("HasObject", false);
                        return new IdleState(Chef);
                    }
                }
            }
            if (Input.GetAxis(Chef.HorizontalAxis) != 0 || Input.GetAxis(Chef.VerticalAxis) != 0)
            {
                return new WalkingLiftState(Chef);
            }
            return this;

        }

    }
}
