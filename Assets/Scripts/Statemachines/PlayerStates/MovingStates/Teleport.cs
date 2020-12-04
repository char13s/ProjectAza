using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : StateMachineBehaviour {
    private PlayerLockon playerTarget;
    private Player player;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        player = Player.GetPlayer();
        playerTarget = PlayerLockon.GetLockon();
        if (playerTarget.Enemies.Count > 0) {
            player.transform.position = playerTarget.EnemyLockedTo().gameObject.transform.position + new Vector3(0, 4, 0);
        }
        else {
            player.transform.position = player.transform.position + player.transform.forward * 15 + new Vector3(0, 5, 0);
        }
    }
    //public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
    //    player.transform.position.MoveTowards(player.transform.position,playerTarget.EnemyLockedTo().gameObject.transform.position + new Vector3(0, 5, 0));
    //}
}
