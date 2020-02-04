using UnityEngine;
using Liftables;

namespace Furniture
{
    public class Table : BaseFurniture
    {
        public AudioClip GrabClip;
        public AudioClip PutClip;

        public LiftableObject TopObject
        {
            set;
            get;
        }

        protected AudioSource audioSource;

        protected new virtual void Start ()
        {
            Id = gameObject.GetInstanceID();
            TopObject = LookForLiftable(gameObject);
            audioSource = GetComponent<AudioSource>();
        }


        // Funcion que ve si el mueble contiene un objeto
        private LiftableObject LookForLiftable(GameObject furniture)
        {
            for (int i = 0; i < furniture.transform.childCount; i++)
            {
                Transform child = furniture.transform.GetChild(i);
                if (child.CompareTag("Liftable"))
                {
                    return child.GetComponent<LiftableObject>();
                }
            }
            return null;
        }

        // Funcion que revisa si existe un objeto encima del mueble
        public bool HasLiftable()
        {
            if (TopObject != null)
            {
                return true;
            }
            return false;
        }

        // Funcion que levanta el objeto y lo coloca en el transform
        // especificado en los argumentos.
        public virtual LiftableObject LiftTopObject(Transform hands)
        {
            if (TopObject != null)
            {
                TopObject.transform.parent = hands;
                TopObject.transform.localPosition = Vector3.zero;
                TopObject.transform.localRotation = Quaternion.identity;
                TopObject.transform.parent.localPosition = new Vector3(-0.00626f, 0.00559f, 0.00225f);
                TopObject.transform.parent.localRotation = Quaternion.Euler(-90f, 0f, 35f);
                Utilities.ChangeHighlight(TopObject.transform, Color.black);
                LiftableObject lObject = TopObject;
                TopObject = null;

                audioSource.clip = GrabClip;
                audioSource.Play();

                return lObject;
            }
            return null;
        }

        // Funcion que pone un objeto en la mesa o llena el objecto que
        // ya se encuentra en ella
        public override bool PutDownObject(LiftableObject liftable)
        {
            if (TopObject == null)
            {
                liftable.transform.parent = this.transform;
                liftable.transform.localPosition = liftable.Offset;
                liftable.transform.localRotation = Quaternion.Euler(0f, liftable.transform.localEulerAngles.y, 0f);
                TopObject = liftable;

                audioSource.clip = PutClip;
                audioSource.Play();

                return true;
            }
            else if (liftable is Ingredient && TopObject is CookingPot)
            {
                bool didFill = (TopObject as CookingPot).Fill((liftable as Ingredient));
                if (didFill)
                {
                    Destroy(liftable.gameObject);
                    return true;
                }
            }
            else if (liftable is CookingPot && TopObject is Plate)
            {
                bool didFill = (TopObject as Plate).Fill(liftable as CookingPot);
                if (didFill)
                {
                    (liftable as CookingPot).Empty();
                    return false;
                }

            }
            return false;

        }

    }
}
