using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameBehaviours;
using Liftables;

namespace Furniture
{
    public class ServingStation : BaseFurniture
    {

        public float spawnTime;
        public Object recipePrefab;
        public Sprite[] ingredientSprites;
        public Sprite[] soupSprites;
        public Text pointsText;
        public PlateSpawner platespawner;
        public AudioClip goodClip;
        public AudioClip badClip;

        private List<Recipe> recipes;
        private Transform spawnContainer;
        private float spawnTimer;
        private float spawnPosX;
        private int points;
        private AudioSource aSource;

        protected override void Start()
        {
            base.Start();
            recipes = new List<Recipe>();
            spawnContainer = GameObject.Find("RecipeContainer").transform;
            spawnTimer = spawnTime - 1f;
            spawnPosX = 0f;
            aSource = GetComponent<AudioSource>();
        }

        void Update()
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer > spawnTime && recipes.Count < 5)
            {
                spawnRecipe();
                spawnTimer = 0f;
            }
        }

        private void spawnRecipe()
        {
            GameObject newRecipe = Instantiate(recipePrefab, spawnContainer) as GameObject;
            newRecipe.GetComponent<RectTransform>().anchoredPosition = new Vector3(Screen.width + 1000f, 0f);

            int ingredientIndex = Random.Range(0, soupSprites.Length);
            //FIXME Hacer que se elijan recetas de prefabs ya echos en lugar
            //de cambiar sprites para mejorar el performance
            newRecipe.transform.Find("RecipeCardTop").Find("RecipeIcon").GetComponent<Image>().sprite = soupSprites[ingredientIndex];
            Transform ingrIcons = newRecipe.transform.Find("RecipeCardBottom").Find("RecipeIngredients");
            for (int i = 0; i < ingrIcons.childCount; i++)
            {
                ingrIcons.GetChild(i).GetComponent<Image>().sprite = ingredientSprites[ingredientIndex];
            }
            Recipe recipeComp = newRecipe.GetComponent<Recipe>();
            recipeComp.setEndPosition(new Vector2(spawnPosX, 0f));
            recipeComp.setIngredient((IngredientName)ingredientIndex);
            recipeComp.setServingStation(this);
            spawnPosX += 226f;
            recipes.Add(recipeComp);
        }

        public override bool PutDownObject(LiftableObject liftable)
        {
            if(liftable is Plate && (liftable as Plate).Filled){
                checkOrder((liftable as Plate).Ingredients); 
                Destroy(liftable.gameObject);
                return true;
            }
            return false;
        }

        public void checkOrder(IngredientName[] ingredients)
        {
            foreach (Recipe recipe in recipes)
            {
                int correctIngredients = 0;
                for (int i = 0; i < ingredients.Length; i++)
                {
                    if (ingredients[i] == recipe.getIngredient())
                    {
                        correctIngredients++;
                    }
                    else
                    {
                        break;
                    }
                }
                if (correctIngredients == 3)
                {
                    points += 20 + recipe.getExtraPoints();
                    //FIXME Hacer el siguiente codigo funcion porque se repite
                    //en la funcion orderFailed
                    pointsText.text = points.ToString();
                    int index = recipes.IndexOf(recipe);
                    recipe.success();
                    recipes.Remove(recipe);
                    for (int i = index; i < recipes.Count; i++)
                    {
                        recipes[i].move();
                    }
                    spawnPosX -= 226f;
                    platespawner.spawnPlate();
                    aSource.clip = goodClip;
                    aSource.Play();

                    break;
                }
            }
        }

        public void orderFailed(Recipe recipe)
        {
            points -= 20;
            pointsText.text = points.ToString();
            int index = recipes.IndexOf(recipe);
            recipes.Remove(recipe);
            //FIXME El destroy deberia hacerce en la clase recipe
            Destroy(recipe.gameObject);
            for (int i = index; i < recipes.Count; i++)
            {
                recipes[i].move();
            }
            spawnPosX -= 226f;
            aSource.clip = badClip;
            aSource.Play();
        }
    }
}
