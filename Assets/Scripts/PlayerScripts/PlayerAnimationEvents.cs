using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    private Player player;
    private Rigidbody rbody;
    [Header("Effects")]
    [SerializeField] private GameObject aura;
    [SerializeField] private TrailRenderer trail;
    [SerializeField] private GameObject shotSparks;
    [SerializeField] private GameObject darkHand;
    [Header("Attacks")]
    [SerializeField] private GameObject darkField;
    [SerializeField] private GameObject shadowBall;
    [Header("Energy Shots")]
    [SerializeField] private GameObject shootPoint;
    [SerializeField] private GameObject rightHand;
    [SerializeField] private GameObject lefttHand;
    [SerializeField] private GameObject energyWave;
    [SerializeField] private GameObject energyBullet;
    private void Start() {
        player = GetComponent<Player>();
        rbody = GetComponent<Rigidbody>();
    }
    private void Fire() {

        Instantiate(energyWave, shootPoint.transform.position, Quaternion.identity);

    }
    private void ActivateAura() {

    }
    private void DeactivateAura() {

    }
    private void TrailControl(int val) {
        if (val == 1) {
            trail.emitting = true;
        }
        else {
            trail.emitting = false;
        }
    }
    private void Jump(int force) {
        player.Rbody.AddForce(new Vector3(0, force, 0), ForceMode.Impulse);
        player.ReadyJump = false;
    }
    private void Shoot() {
        player.Shoot = false;
        Instantiate(shotSparks, rightHand.transform.position, Quaternion.identity);
        Instantiate(energyBullet,rightHand.transform.position,Quaternion.identity);
    }
    private void DarkExplode() {
        Instantiate(darkField,transform.position,Quaternion.identity);
    }
    private void DarkHand() {
        //Instantiate(darkHand, lefttHand.transform.position, Quaternion.identity);
        darkHand.SetActive(true);
    }
    private void ShadowBall() {
        darkHand.SetActive(false);
        Instantiate(shadowBall, lefttHand.transform.position, Quaternion.identity);
    }
}
