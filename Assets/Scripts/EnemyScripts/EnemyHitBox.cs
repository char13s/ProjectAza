using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBox : MonoBehaviour
{
    [SerializeField] private EnemyBaseScript enemy;
        // Start is called before the first frame update
    void Start(){
        
    }
    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<Player>()) {
            enemy.CalculateAttack();

        }
        
    }
}
