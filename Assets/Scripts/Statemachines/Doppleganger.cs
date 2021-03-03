using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doppleganger : StateMachineBehaviour
{
    [SerializeField] private GameObject clone;
    private Player player;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        player = Player.GetPlayer();
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        Instantiate(clone, player.transform.position + new Vector3(1, 0, 0),Quaternion.identity);
    }
}
