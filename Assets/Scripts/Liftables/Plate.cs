using System.Collections.Generic;
using UnityEngine;

namespace Liftables
{
    public class Plate : LiftableObject
    {
        public bool IsDirty;

        public IngredientName[] Ingredients
        {
            get;
            set;
        }
        public bool Filled
        {
            get;
            set;
        }

        private Transform soup;
        private Color soupColor;

        void Start()
        {
            soup = transform.Find("Soup");
            Filled = false;
        }

        public bool Fill(CookingPot pot)
        {
            if (!Filled && !pot.Burned && pot.Cooked && pot.Ingredients.Count == 3)
            {
                soup.gameObject.SetActive(true);
                switch(pot.Ingredients[0]){
                case IngredientName.Tomato:
                    soupColor = new Color(219f / 255f, 50f / 255f, 30f / 255f);
                    break;
                case IngredientName.Onion:
                    soupColor = new Color(185f / 255f, 84f / 255f, 0f);
                    break;
                case IngredientName.Mushroom:
                    soupColor = new Color(156f / 255f, 112f / 255f, 70f / 255f);
                    break;
                }
                soup.gameObject.GetComponent<Renderer>().material.SetColor("_Color", soupColor);
                Filled = true;
                Ingredients = pot.Ingredients.ToArray();
                return true;
            }
            return false;
        }

        public void Empty()
        {
            Ingredients = null;
            soup.gameObject.SetActive(false);
            Filled = false;
        }
    }
}
