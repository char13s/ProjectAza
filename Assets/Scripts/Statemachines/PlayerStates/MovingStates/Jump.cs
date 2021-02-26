using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Jump : StateMachineBehaviour
{
    private Player player;
    [SerializeField] private float jumpForce;
    [SerializeField] private float move;
    private Vector3 speed;
     private float jumpForceMax;

    public static event UnityAction<float> jumped;
    public float JumpForceMax { get => jumpForceMax; set => jumpForceMax =value; }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        player = Player.GetPlayer();
        if (jumped!=null) {
            jumped(jumpForce);
        }
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (player.Moving) {
            speed = move * player.transform.forward;
            speed.y = player.Rbody.velocity.y;
            player.Rbody.velocity = speed;
        }
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        player.Jumping = false;
    }
}
