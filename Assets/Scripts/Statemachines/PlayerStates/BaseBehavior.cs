using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBehavior : StateMachineBehaviour
{
    private Player pc;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        pc = Player.GetPlayer();
        pc.CmdInput = 0;
    }
}
