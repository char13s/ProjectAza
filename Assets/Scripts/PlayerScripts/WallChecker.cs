using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class WallChecker : MonoBehaviour
{
    public static event UnityAction<bool> stickToWall;
    private Rigidbody rbody;
    private Player player;
    private float distanceWall;
    private bool wall;
    private void Awake() {
        player = Player.GetPlayer();
        distanceWall = GetComponent<Collider>().bounds.extents.y;
        rbody = GetComponentInParent<Rigidbody>();
        
        if (rbody == null) {
            print("fuck");
        }
    }
    private void OnTriggerEnter(Collider other) {
        
    }
    private void Update() {
        if (wall) {
            WallStick();
        }
    }
    private void FixedUpdate() {
        if (Physics.Raycast(transform.position, player.transform.forward, distanceWall + 0.1f)) {
            wall = true;
        }
        else {
            wall = false;
        }
    }
    private void WallStick() {
        if (Input.GetButtonDown("Jump")) {
            rbody.useGravity = false;
            rbody.velocity = new Vector3(0,0,0);

        }
        if (Input.GetButtonUp("Jump")) {
            rbody.useGravity = true;
        }
    }
    //private void OnTriggerEnter(Collider other) {
    //    if (stickToWall != null) {
    //        stickToWall(true);
    //    }
    //}
    //private void OnTriggerStay(Collider other) {
    //    if (stickToWall != null) {
    //        stickToWall(true);
    //    }
    //}
    //private void OnTriggerExit(Collider other) {
    //    if (stickToWall != null) {
    //        stickToWall(false);
    //    }
    //}

}
