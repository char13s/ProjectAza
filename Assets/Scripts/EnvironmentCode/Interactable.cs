using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<Equipment>()) {
            other.GetComponent<Equipment>().PickUp();
        }
    }
}
