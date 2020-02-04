using UnityEngine;
using UnityEngine.UI;
using Furniture;

namespace GameBehaviours
{
    public class Recipe : MonoBehaviour
    {
        public float movementSpeed;
        public float recipeTime;

        private IngredientName ingredient;
        private Vector2 end;
        private RectTransform rectTrans;
        private float timer;
        private Image timerProgress;
        private bool startTimer;
        private bool moveToNext;
        private int extraPoints;
        private ServingStation station;

        void Start ()
        {
            timerProgress = transform.Find("RecipeCardTop").Find("RecipeTimerBackground").Find("RecipeTimerForeground").GetComponent<Image>();
            rectTrans = GetComponent<RectTransform>();
            timer = recipeTime;
            startTimer = false;
            moveToNext = true;
            extraPoints = 6;
        }

        void Update ()
        {
            if (moveToNext)
            {
                rectTrans.anchoredPosition = Vector2.Lerp(end, rectTrans.anchoredPosition, 0.90f);
                if (!startTimer && rectTrans.anchoredPosition.x <= end.x + 5f)
                {
                    startTimer = true;
                }

                if (rectTrans.anchoredPosition.x == end.x)
                {
                    moveToNext = false;
                }
            }

            if (startTimer)
            {
                timer -= Time.deltaTime;
                timerProgress.fillAmount = timer / recipeTime;
                if (timer < ((extraPoints / 2) * (recipeTime / 4)))
                {
                    extraPoints -= 2;
                    switch (extraPoints)
                    {
                    case 4:
                        timerProgress.color = new Color(85f / 255f, 195f / 255f, 49f / 255f);
                        break;
                    case 2:
                        timerProgress.color = new Color(243f / 255f, 234f / 255f, 47f / 255f);
                        break;
                    case 0:
                        timerProgress.color = new Color(227f / 255f, 49f / 255f, 0f / 255f);
                        break;
                    }
                }

                if (timer < 0f)
                {
                    failure();
                }
            }

        }

        public void success()
        {
            Destroy(this.gameObject);
        }

        public void failure()
        {
            station.orderFailed(this);
        }

        public void move()
        {
            end = new Vector2(rectTrans.anchoredPosition.x - 226f, rectTrans.anchoredPosition.y);
            moveToNext = true;
        }

        public void setIngredient(IngredientName name)
        {
            ingredient = name;
        }

        public IngredientName getIngredient()
        {
            return ingredient;
        }

        public void setEndPosition(Vector2 position)
        {
            end = position;
        }

        public void setServingStation(ServingStation station)
        {
            this.station = station;
        }

        public int getExtraPoints()
        {
            return extraPoints;
        }
    }
}
