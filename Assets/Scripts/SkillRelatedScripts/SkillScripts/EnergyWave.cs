using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyWave : MonoBehaviour
{
    private Vector3 direction;
    [SerializeField] private int move;
    [SerializeField] private GameObject fadeAway;
    [SerializeField] private GameObject boom;
    // Start is called before the first frame update
    private void Start() {
        direction = Player.GetPlayer().transform.forward;
        transform.rotation= Quaternion.LookRotation(direction, Vector3.up);
        //LayerMask.GetMask("Ground");
        Destroy(gameObject, 4f);
        StartCoroutine(Disappear());
    }

    // Update is called once per frame
    private void Update() {
        transform.position += direction * move * Time.deltaTime;
    }

    //private int AdditionPower()=>Player.GetPlayer().stats.Level 
    private IEnumerator Disappear() {
        YieldInstruction wait = new WaitForSeconds(3.9f);
        yield return wait;
        //Instantiate(fadeAway,transform.position,Quaternion.identity);
    }
    private void OnTriggerEnter(Collider other) {

        //if (other.gameObject.CompareTag("Enemy")) {
        //    if (other.gameObject.GetComponent<Enemy>() != null) {
        //        other.gameObject.GetComponent<Enemy>().CalculateDamage(5);
        //    }
        //
        //
        //
        //}
        if (!other.GetComponent<Player>()) { 
            Instantiate(boom, transform.position, transform.rotation);
            Destroy(gameObject);
        }

    }
}
