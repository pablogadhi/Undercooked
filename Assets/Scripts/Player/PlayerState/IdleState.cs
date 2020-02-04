using UnityEngine;
using Furniture;

namespace Player
{
    public class IdleState: PlayerState
    {

        public IdleState(ChefBehaviour chef)
        {
            this.Chef = chef;
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
                        return new IdleLiftState(Chef);
                    }
                }
            }
            if (Input.GetAxis(Chef.HorizontalAxis) != 0 || Input.GetAxis(Chef.VerticalAxis) != 0)
            {
                return new WalkingState(Chef);
            }
            if (Input.GetButtonDown(Chef.InteractButton) && Chef.FrontFurniture is IActionFurniture)
            {
                return new InteractingState(Chef, Chef.FrontFurniture as IActionFurniture);
            }
            return this;

        }

    }
}
