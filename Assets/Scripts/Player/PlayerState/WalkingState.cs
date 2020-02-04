using UnityEngine;
using Furniture;

namespace Player
{
    public class WalkingState : BaseWalking
    {
        public WalkingState(ChefBehaviour chef)
        {
            this.Chef = chef;
            chef.Anim.SetBool("Walking", true);
        }

        public override PlayerState HandleInput()
        {
            if (Input.GetButtonDown(Chef.GrabButton))
            {
                if (Chef.Liftable == null && Chef.FrontFurniture is Table)
                {
                    Chef.Liftable = (Chef.FrontFurniture as Table).LiftTopObject(Chef.Hands);
                    if (Chef.Liftable != null)
                    {
                        return new WalkingLiftState(Chef);
                    }
                }
            }
            if (Input.GetAxis(Chef.HorizontalAxis) == 0 && Input.GetAxis(Chef.VerticalAxis) == 0)
            {
                Chef.Anim.SetBool("Walking", false);
                return new IdleState(Chef);
            }
            if (Input.GetButtonDown(Chef.InteractButton) && Chef.FrontFurniture is IActionFurniture)
            {
                return new InteractingState(Chef, Chef.FrontFurniture as IActionFurniture);
            }
            return this;
        }
    }
}
