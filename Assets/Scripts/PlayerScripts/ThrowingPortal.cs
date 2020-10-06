using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ThrowingPortal : MonoBehaviour
{
    public static event UnityAction<Transform> sendSpot;
    private Rigidbody rbody;
    [SerializeField] private float move;
    private void Awake() {
        rbody = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        rbody.AddForce(transform.forward*move, ForceMode.Impulse);
    }
    private void OnTriggerEnter(Collider other) {
        if (sendSpot != null) {
            sendSpot(transform);
        }
        Destroy(gameObject,4);
    }
}
