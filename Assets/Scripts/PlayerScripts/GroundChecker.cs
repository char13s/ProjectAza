using UnityEngine;
using UnityEngine.Events;
public class GroundChecker : MonoBehaviour
{
    public static event UnityAction<bool> grounded;


    private void OnTriggerEnter(Collider other) {
        if (grounded != null) {
            grounded(true);
        }
    }
    private void OnTriggerStay(Collider other) {
        if (grounded != null) {
            grounded(true);
        }
        
    }
    private void OnTriggerExit(Collider other) {
        if (grounded != null) {
            grounded(false);
        }
    }
    
}
