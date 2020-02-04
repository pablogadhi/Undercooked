using UnityEngine;
using Furniture;

namespace Player
{
    public class InteractingState: PlayerState
    {
        public delegate void InteractingAction();
        public event InteractingAction OnInteractingStart;
        public event InteractingAction OnInteractingStop;


        public InteractingState(ChefBehaviour chef, IActionFurniture furniture)
        {
            this.Chef = chef;
            furniture.SubscribeMethods(this, Chef.Hands);
        }

        public override PlayerState HandleInput()
        {
            if (Input.GetButton(Chef.InteractButton) && Chef.FrontFurniture is IActionFurniture)
            {
                bool interacting = (Chef.FrontFurniture as IActionFurniture).Interact();
                if (interacting)
                {
                    if (Chef.Anim.GetBool("Walking") == true)
                    {
                        Chef.Anim.SetBool("Walking", false);
                    }
                    Chef.Anim.SetBool("Interacting", true);
                    OnInteractingStart();
                    if (Chef.FrontFurniture is ChoppingTable)
                    {
                        Chef.Anim.SetBool("Chopping", true);
                    }
                }
                else
                {
                    Exit();
                    return new IdleState(Chef);
                }
            }
            if (Input.GetButtonUp(Chef.InteractButton))
            {
                Exit();
                return new IdleState(Chef);
            }
            return this;
        }

        public void Exit()
        {
            Chef.Anim.SetBool("Interacting", false);
            Chef.Anim.SetBool("Chopping", false);
            OnInteractingStop();
            (Chef.FrontFurniture as IActionFurniture).UnsubscribeMethods(this);
        }
    }
}
