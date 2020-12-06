using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerAnimationEvents : MonoBehaviour {
    private Player player;
    private Rigidbody rbody;
    private PlayerLockon playerTarget;
    [Header("Effects")]
    [SerializeField] private GameObject aura;
    [SerializeField] private TrailRenderer trail;
    [SerializeField] private GameObject shotSparks;
    [SerializeField] private GameObject darkHand;
    [SerializeField] private GameObject swordPoint;
    [SerializeField] private GameObject groundPointFront;
    [SerializeField] private GameObject vanishEffect;
    [Header("Attacks")]
    [SerializeField] private GameObject darkField;
    [SerializeField] private GameObject shadowBall;
    [SerializeField] private GameObject splash;
    [Header("Energy Shots")]
    [SerializeField] private GameObject shootPoint;
    [SerializeField] private GameObject rightHand;
    [SerializeField] private GameObject lefttHand;
    [SerializeField] private GameObject energyWave;
    [SerializeField] private GameObject energyBullet;
    [Header("obj refs")]
    [SerializeField] private GameObject riseHitBox;
    [SerializeField] private GameObject slamBox;
    private void Start() {
        player = GetComponent<Player>();
        playerTarget = GetComponent<PlayerLockon>();
        rbody = GetComponent<Rigidbody>();
        Teleport.vanish += TeleportVanish;
        SlamState.slam += SmackDown;
    }
    #region Anim Events
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
    private void LightSplash() {
        Instantiate(splash, groundPointFront.transform.position, Quaternion.identity);
    }
    private void TeleportVanish() {
        Instantiate(vanishEffect, transform.position, vanishEffect.transform.rotation);
        player.Body.SetActive(false);
        StartCoroutine(WaitToMove());
    }
    private void RiseHit(int val) {
        if (val == 1) {
            riseHitBox.SetActive(true);
        }
        else {
            riseHitBox.SetActive(false);
        }
    }
    private void SmackDown(int val) {
        if (val == 1) {
            slamBox.SetActive(true);
        }
        else {
            slamBox.SetActive(false);
        }
    }
    #endregion
    #region Coroutines
    private IEnumerator WaitToMove() {
        YieldInstruction wait = new WaitForSeconds(0.5f);
        yield return wait;
        if (playerTarget.Enemies.Count > 0) {
            if (playerTarget.EnemyLockedTo().Grounded) {
                transform.position = playerTarget.EnemyLockedTo().gameObject.transform.position + new Vector3(0, 1, -1);
            }
            else {
                transform.position = playerTarget.EnemyLockedTo().gameObject.transform.position + new Vector3(0, 0.1f, -1);
            }
        }
        else {
            transform.position = transform.position + transform.forward * 15 + new Vector3(0, 2.5f, 0);
        }
        player.Grounded = false;
        player.Body.SetActive(true);
    }
    #endregion
}
