using UnityEngine;
using Furniture;
using Liftables;

namespace Player
{
    public class ChefBehaviour : MonoBehaviour
    {
        public float speed;
        public string GrabButton;
        public string InteractButton;
        public string BoostButton;
        public string HorizontalAxis;
        public string VerticalAxis;

        public delegate void PressCancel();
        public event PressCancel OnCancelPressed;

        public Animator Anim
        {
            get;
            set;
        }
        public BaseFurniture FrontFurniture
        {
            get;
            set;
        }
        public LiftableObject Liftable
        {
            get;
            set;
        }
        public Transform Hands
        {
            get;
            set;
        }

        public Vector3 Mdirection
        {
            get;
            set;
        }

        private Rigidbody body;
        private PlayerState state;

        void Start ()
        {
            Anim = GetComponent<Animator>();
            body = GetComponent<Rigidbody>();
            Hands = transform.Find("Armature").Find("Spine").Find("Hand.R").Find("CarringObject");
            Hands.localPosition = new Vector3(-0.00626f, 0.00559f, 0.00225f);
            Hands.localRotation = Quaternion.Euler(-90f, 0f, 35f);

            FrontFurniture = null;
            Liftable = null;
            state = new IdleState(this);
            GetComponent<ObjectObserver>().OnFindFurniture += ChangeFrontFurniture;
        }

        void Update()
        {
            Mdirection = new Vector3(Input.GetAxis(HorizontalAxis), 0f, Input.GetAxis(VerticalAxis));
            state = state.HandleInput();

            if (Input.GetButtonDown("Cancel"))
            {
                if (OnCancelPressed != null)
                {

                    OnCancelPressed();
                }
            }
        }

        void FixedUpdate()
        {
            if (state is BaseWalking)
            {
                (state as BaseWalking).walk(this, transform, body);
            }
        }

        void OnDestroy()
        {
            GetComponent<ObjectObserver>().OnFindFurniture -= ChangeFrontFurniture;
        }

        private void ChangeFrontFurniture(BaseFurniture furniture)
        {
            this.FrontFurniture = furniture;
        }

    }
}
