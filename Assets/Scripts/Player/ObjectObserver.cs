using UnityEngine;
using Furniture;

namespace Player
{
    /* Clase que se encarga de observar si existe un mueble en frente del jugador,
     * actualiza la referencia del jugador a ese mueble e ilumina los hijos del mueble. */
    public class ObjectObserver : MonoBehaviour
    {
        public Color higlightColor;

        public delegate void FindFurnitureAction(BaseFurniture furniture);
        public event FindFurnitureAction OnFindFurniture;

        private BaseFurniture frontFurniture;

        void Start()
        {
            frontFurniture = null;
        }

        void Update ()
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, transform.forward, out hit, 2.5f);
            if (hit.collider != null)
            {
                GameObject hitObject = hit.collider.gameObject;

                if (frontFurniture != null && frontFurniture.Id != hitObject.GetInstanceID())
                {
                    Utilities.ChangeHighlight(frontFurniture.transform, Color.black);

                }

                // Se obvian a los jugadores
                if (!hitObject.CompareTag("Player"))
                {
                    Utilities.ChangeHighlight(hitObject.transform, higlightColor);
                    frontFurniture = (BaseFurniture) hitObject.GetComponent<BaseFurniture>();
                }
            }
            else if (frontFurniture != null)
            {
                Utilities.ChangeHighlight(frontFurniture.transform, Color.black);
                frontFurniture = null;
            }
            //Se informa a los subscriptores que cambio el mueble de enfrente
            if (OnFindFurniture != null)
            {
                OnFindFurniture(frontFurniture);
            }
        }
    }
}
