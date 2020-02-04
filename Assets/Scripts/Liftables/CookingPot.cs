using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Furniture;

namespace Liftables
{
    public class CookingPot : LiftableObject
    {
        public float CookingTime;
        public float BurniningTime;
        public Sprite[] IngredientSprites;

        public bool Cooked
        {
            get;
            set;
        }

        public bool Burned
        {
            get;
            set;
        }

        public List<IngredientName> Ingredients
        {
            get;
            set;
        }

        private Image progressBar;
        private Image[] ingredientImages;
        private Transform soup;
        private Color burnedColor;
        private Color soupColor;
        private Color previousColor;
        private Sprite addedSprite;
        private float cookingProgress;
        private float burningProgress;
        private AudioSource aSource;

        void Start()
        {
            soup = transform.Find("Soup");
            progressBar = transform.Find("ProgressUI").Find("ProgressContainer").Find("ProgressBar").GetComponent<Image>();
            Image ingredientImage0 = transform.Find("IngredientsUI").Find("Ingredient0").GetComponent<Image>();
            Image ingredientImage1 = transform.Find("IngredientsUI").Find("Ingredient1").GetComponent<Image>();
            Image ingredientImage2 = transform.Find("IngredientsUI").Find("Ingredient2").GetComponent<Image>();
            ingredientImages = new Image[] {ingredientImage0, ingredientImage1, ingredientImage2};
            Ingredients =  new List<IngredientName>();
            burnedColor = new Color(49f / 255f, 16f / 255f, 12f / 255f);
            Burned = false;
            aSource = GetComponent<AudioSource>();
        }

        public void SubscribeToStove(Stove stove)
        {
            stove.OnTopObjectPut += Cook;
        }

        public void UnsubscribeToStove(Stove stove)
        {
            stove.OnTopObjectPut -= Cook;
            aSource.Stop();
        }

        public bool Fill(Ingredient ingr)
        {
            if (Ingredients.Count < 3 && ingr.Chopped)
            {
                previousColor = soupColor;
                switch (ingr.Name)
                {
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

                addedSprite = IngredientSprites[(int) ingr.Name];

                if (Ingredients.Count == 0)
                {
                    soup.gameObject.SetActive(true);
                }
                else
                {
                    soup.localPosition = new Vector3(0f, soup.localPosition.y + 0.25f, 0f);
                    soupColor = (previousColor + soupColor) / 2;
                }
                Ingredients.Add(ingr.Name);
                ingredientImages[Ingredients.Count - 1].sprite = addedSprite;
                ingredientImages[Ingredients.Count - 1].color = new Color(1f, 1f, 1f, 1f);
                soup.gameObject.GetComponent<Renderer>().material.SetColor("_Color", soupColor);
                progressBar.transform.parent.gameObject.SetActive(true);
                Cooked = false;
                cookingProgress = (CookingTime / 4) * (Ingredients.Count - 1);
                progressBar.fillAmount = cookingProgress / CookingTime;
                burningProgress = 0;
                return true;
            }
            return false;
        }

        public bool IsEmpty()
        {
            if (Ingredients.Count == 0)
            {
                return true;
            }
            return false;
        }

        public void Empty()
        {
            soup.localPosition = Vector3.zero;
            soup.gameObject.SetActive(false);
            Ingredients.RemoveRange(0, Ingredients.Count);
            Burned = false;
            Cooked = false;
            cookingProgress = 0;
            burningProgress = 0;
            foreach (Image img in ingredientImages)
            {
                img.sprite = IngredientSprites[IngredientSprites.Length - 1];
                img.color = new Color(1f, 1f, 1f, 93 / 255f);
            }
        }

        public void Burn()
        {
            if (burningProgress >= BurniningTime && !Burned)
            {
                soup.gameObject.GetComponent<Renderer>().material.SetColor("_Color", burnedColor);
                Burned = true;
                aSource.Stop();

            }
            else
            {
                burningProgress += Time.deltaTime;
                if (!aSource.isPlaying && !Burned)
                {
                    aSource.Play();
                }
            }
        }

        public void Cook()
        {
            if (cookingProgress >= CookingTime)
            {
                Cooked = true;
                progressBar.transform.parent.gameObject.SetActive(false);
                Burn();
            }
            else
            {
                cookingProgress += Time.deltaTime;
                progressBar.fillAmount = cookingProgress / CookingTime;
            }
            Debug.Log(cookingProgress);
        }
    }
}
