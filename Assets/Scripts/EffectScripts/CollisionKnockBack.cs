using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionKnockBack : MonoBehaviour
{
    [SerializeField] private float knockBackStrength;
    private void OnTriggerEnter(Collider other) {
        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
        if (rb != null) {
            Vector3 direction = other.transform.position - transform.position;
            rb.AddForce(direction.normalized * knockBackStrength,ForceMode.Impulse);
        }
    }
}
