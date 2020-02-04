using UnityEngine;

namespace GameBehaviours
{
    public class UIWorldDirection: MonoBehaviour
    {

        private GameObject Camera;
        private RectTransform rectTrans;

        void Start()
        {
            Camera = GameObject.FindGameObjectWithTag("MainCamera");
            rectTrans = GetComponent<RectTransform>();
        }

        void Update()
        {

            rectTrans.rotation = Camera.transform.rotation;
        }
    }
}
