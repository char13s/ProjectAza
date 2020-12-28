using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingState : StateMachineBehaviour
{
    [SerializeField] private float move;
    private Player player;
    private Vector3 speed;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        player = Player.GetPlayer();
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        if (!player.CantMove) {
            Vector3 direction = new Vector3(player.Displacement.x,0, player.Displacement.y);
        speed= direction * move;
        
        speed.y = player.Rbody.velocity.y;
        player.Rbody.velocity = speed;}
        //player.Rbody.velocity.y = 0;
        //player.transform.position+= player.Displacement * move*Time.delta;
    }
}
