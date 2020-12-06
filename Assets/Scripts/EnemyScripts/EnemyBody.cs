using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBody : MonoBehaviour
{
    [SerializeField] EnemyBaseScript mainEnemy;
    [SerializeField] GameObject hitSparks;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other) {
        mainEnemy.Hit = true;

        if (hitSparks != null) {
            Instantiate(hitSparks, mainEnemy.transform.position, Quaternion.identity);
        }
    }
}
