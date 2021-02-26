using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiseHitBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<EnemyBaseScript>()) { 
        other.GetComponent<EnemyBaseScript>().SmackedUp();}
    }
}
