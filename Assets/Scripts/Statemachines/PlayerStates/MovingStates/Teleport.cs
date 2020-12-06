using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Teleport : StateMachineBehaviour {
    
    public static event UnityAction vanish;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        if (vanish != null) {
            vanish();
        }
    }
    //public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
    //    player.transform.position.MoveTowards(player.transform.position,playerTarget.EnemyLockedTo().gameObject.transform.position + new Vector3(0, 5, 0));
    //}
}
