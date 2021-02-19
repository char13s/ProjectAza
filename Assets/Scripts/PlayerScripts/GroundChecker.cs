using UnityEngine;
using UnityEngine.Events;
public class GroundChecker : MonoBehaviour
{
    public static event UnityAction<bool> grounded;
    private float distanceGround;
    private Player player;
    private void Start() {
        distanceGround = GetComponent<Collider>().bounds.extents.y;
        player = Player.GetPlayer();
    }

    //private void OnTriggerEnter(Collider other) {
    //    if (grounded != null) {
    //        grounded(true);
    //    }
    //}
    ////private void OnTriggerStay(Collider other) {
    ////    if (grounded != null) {
    ////        grounded(true);
    ////    }
    ////    
    ////}
    //private void OnTriggerExit(Collider other) {
    //    if (grounded != null) {
    //        grounded(false);
    //    }
    //}
    private void FixedUpdate() {
        if (!Physics.Raycast(transform.position, -Vector2.up, distanceGround + 0.1f)) {
            player.Grounded = false;

        }
        else {
            player.Grounded = true;

        }
    }
}
