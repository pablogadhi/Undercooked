using UnityEngine;

namespace Player{
    public abstract class BaseWalking: PlayerState {
        public void walk(ChefBehaviour chef, Transform chefTrans, Rigidbody chefBody){
            chefTrans.forward = Vector3.Lerp(chefTrans.forward, chef.Mdirection, 0.5f);
            chefBody.velocity = chef.Mdirection * Time.deltaTime * chef.speed;
        }
    }
}
