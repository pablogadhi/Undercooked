using UnityEngine;
using UnityEngine.UI;

public enum IngredientName { Tomato, Onion, Mushroom };

namespace Liftables
{
    public class Ingredient : LiftableObject
    {
        public IngredientName Name;
        public GameObject ChoppedObject;
        public float ChoppingTime;

        public bool Chopped
        {
            get;
            set;
        }
        public float Progress
        {
            get;
            set;
        }

        void Start()
        {
            Progress = 0;
        }

        public bool Chopp(Image progressBar)
        {
            if (Progress >= ChoppingTime)
            {
                Chopped = true;
                for (int i = 0; i < transform.childCount; i++)
                {
                    Destroy(transform.GetChild(i).gameObject);
                }
                Instantiate(ChoppedObject, transform);
            }
            else if (!Chopped)
            {
                Progress += Time.deltaTime;
                progressBar.fillAmount = Progress / ChoppingTime;
            }
            return !Chopped;
        }
    }
}
