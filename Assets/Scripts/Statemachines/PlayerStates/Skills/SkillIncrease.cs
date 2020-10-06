using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class SkillIncrease : StateMachineBehaviour
{
    [SerializeField] private int bpIncrease;
    private int increase;
    private Player player;
    public static event UnityAction<int> sendIncrease;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        player = Player.GetPlayer();
        int bp = player.stats.BattlePressure;
        increase = (int)(bp * bpIncrease * 0.01f);
        if (sendIncrease != null) {
            sendIncrease(increase);
        }
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        if (sendIncrease != null) {
            sendIncrease(-increase);
        }
    }
}
