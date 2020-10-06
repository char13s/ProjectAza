using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonSword : MonoBehaviour
{
    [SerializeField] private GameObject spawnIn;
    [SerializeField] private GameObject spawnOut;
    private void OnEnable() {
        //spawm in particles
        if (spawnIn != null) {
            Instantiate(spawnIn,transform.position,Quaternion.identity);
        }
    }
    private void OnDisable() {
        //spawn in particles
       //f (spawnOut != null) {
       //   Instantiate(spawnOut, transform.position, Quaternion.identity);
       //
    }
}
