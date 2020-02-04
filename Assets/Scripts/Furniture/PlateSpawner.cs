using System.Collections.Generic;
using UnityEngine;
using Liftables;

namespace Furniture
{
    public class PlateSpawner : Table
    {

        public Object plate;
        public int startWith;

        private Stack<LiftableObject> plates;

        protected override void Start()
        {
            base.Start();
            plates = new Stack<LiftableObject>();
            for (int i = 0; i < startWith; i++)
            {
                spawnPlate();
            }
        }

        void Update()
        {
            if (TopObject == null && plates.Count != 0)
            {
                TopObject = plates.Pop();
            }

        }

        public void spawnPlate()
        {
            LiftableObject instantiatedPlate = (Instantiate(plate, transform) as GameObject).GetComponent<LiftableObject>();
            if (TopObject != null)
            {
                instantiatedPlate.transform.localPosition = instantiatedPlate.Offset + new Vector3(0f, (plates.Count + 1) * 0.1f, 0f);
                plates.Push(instantiatedPlate);
            }
            else
            {
                TopObject = instantiatedPlate;
                instantiatedPlate.transform.localPosition = instantiatedPlate.Offset;
            }
        }

        public bool interact(Transform hands)
        {
            spawnPlate();
            return true;
        }
    }
}
