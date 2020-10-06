using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillMachine : StateMachineBehaviour {
    private Player player;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        player = Player.GetPlayer();
        player.SkillId = 0;
    }
}
