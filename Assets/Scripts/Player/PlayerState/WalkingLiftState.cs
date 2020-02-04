using UnityEngine;

namespace Player
{
    public class WalkingLiftState: BaseWalking
    {
        public WalkingLiftState(ChefBehaviour chef)
        {
            this.Chef = chef;
            Chef.Anim.SetBool("Walking", true);
            Chef.Anim.SetBool("HasObject", true);
        }

        public override PlayerState HandleInput()
        {
            if (Input.GetButtonDown(Chef.GrabButton))
            {
                if (Chef.Liftable != null && Chef.FrontFurniture != null)
                {
                    bool didPut = Chef.FrontFurniture.PutDownObject(Chef.Liftable);
                    if (didPut)
                    {
                        Chef.Liftable = null;
                        Chef.Anim.SetBool("HasObject", false);
                        return new WalkingState(Chef);
                    }
                }
            }
            if ( Input.GetAxis(Chef.HorizontalAxis) == 0 && Input.GetAxis(Chef.VerticalAxis) == 0 )
            {

                Chef.Anim.SetBool("Walking", false);
                return new IdleLiftState(Chef);
            }
            return this;
        }
    }
}
