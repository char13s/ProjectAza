using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class WallChecker : MonoBehaviour
{
    public static event UnityAction<bool> stickToWall;
    private void OnTriggerEnter(Collider other) {
        if (stickToWall != null) {
            stickToWall(true);
        }
    }
    private void OnTriggerStay(Collider other) {
        if (stickToWall != null) {
            stickToWall(true);
        }
    }
    private void OnTriggerExit(Collider other) {
        if (stickToWall != null) {
            stickToWall(false);
        }
    }
}
