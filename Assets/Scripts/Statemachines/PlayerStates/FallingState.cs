using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class FallingState : StateMachineBehaviour {
    private Player player;
    public static event UnityAction gravity;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        player = Player.GetPlayer();
        if (gravity != null) {
            gravity();
        }
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        if (!player.Rbody.useGravity) {
            player.Rbody.useGravity = true;
        }
    }
}
