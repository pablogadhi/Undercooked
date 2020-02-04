using UnityEngine;
using UnityEngine.UI;
using Liftables;
using Player;

namespace Furniture
{
    public class ChoppingTable : Table, IActionFurniture
    {

        public Image ProgressBar;
        public AudioClip ChopClip;

        private Transform knife;
        private Transform board;
        private Transform playerHands;

        protected override void Start()
        {
            base.Start();
            knife = transform.Find("ChoppingBoard").Find("Handle").transform;
            board = transform.Find("ChoppingBoard").transform;
            playerHands = null;
        }

        public override LiftableObject LiftTopObject(Transform hands)
        {
            if (TopObject is Ingredient)
            {
                Ingredient ingr = TopObject as Ingredient;
                if (ingr.Progress == 0 || ingr.Chopped)
                {
                    return base.LiftTopObject(hands);
                }
            }
            else
            {
                return base.LiftTopObject(hands);
            }
            return null;
        }

        private void TakeKnife()
        {
            if (knife.parent != playerHands)
            {
                knife.parent = playerHands;
                knife.localPosition = new Vector3(0.786f, -0.716f, 0.52f);
                knife.localRotation = Quaternion.Euler(0f, 180f, -50f);
                ProgressBar.transform.parent.gameObject.SetActive(true);
            }
        }

        private void PutDownKnife()
        {
            knife.parent = board;
            knife.localPosition = new Vector3(0f, 0f, 0.177f);
            knife.localRotation = Quaternion.Euler(0f, -19f, 0f);
            knife.localScale = Vector3.one;
            if ((TopObject as Ingredient).Chopped)
            {
                ProgressBar.transform.parent.gameObject.SetActive(false);
            }
        }

        public void SubscribeMethods(InteractingState state, Transform hands)
        {
            playerHands = hands;
            state.OnInteractingStart += TakeKnife;
            state.OnInteractingStop += PutDownKnife;
        }

        public void UnsubscribeMethods(InteractingState state)
        {

            state.OnInteractingStart -= TakeKnife;
            state.OnInteractingStop -= PutDownKnife;
            playerHands = null;
        }

        public bool Interact()
        {
            if (TopObject != null && TopObject is Ingredient)
            {
                Ingredient ingr = TopObject as Ingredient;
                bool chopping = ingr.Chopp(ProgressBar);
                if (chopping && !audioSource.isPlaying)
                {
                    audioSource.clip = ChopClip;
                    audioSource.pitch = 4;
                    audioSource.Play();
                }
                return chopping;
            }
            return false;
        }
    }
}
