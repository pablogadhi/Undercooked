using UnityEngine;
using Liftables;

namespace Furniture
{
    public class IngredientSpawner : Table
    {

        public Object Ingredient;

        private Animator anim;

        protected override void Start(){
            base.Start();
            anim = GetComponent<Animator>();
        }

        public override LiftableObject LiftTopObject(Transform hands)
        {
            if (TopObject != null)
            {
                return base.LiftTopObject(hands);
            }
            else if (hands.childCount == 0)
            {
                anim.SetTrigger("Open");
                audioSource.clip = GrabClip;
                audioSource.Play();
                return (Instantiate(Ingredient, hands) as GameObject).GetComponent<LiftableObject>();
            }
            return null;
        }
    }
}
