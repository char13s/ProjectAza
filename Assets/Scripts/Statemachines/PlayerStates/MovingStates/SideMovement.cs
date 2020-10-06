using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideMovement : StateMachineBehaviour
{
    [SerializeField] private float move;
    private Player player;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        player = Player.GetPlayer();
        player.Rbody.AddForce(player.transform.right * move, ForceMode.Impulse);
    }
}
