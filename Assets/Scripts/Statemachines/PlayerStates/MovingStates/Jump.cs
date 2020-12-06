using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Jump : StateMachineBehaviour
{
    private Player player;
    [SerializeField] private float jumpForce;
     private float jumpForceMax;

    public static event UnityAction<float> jumped;
    public float JumpForceMax { get => jumpForceMax; set => jumpForceMax =value; }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        player = Player.GetPlayer();
        //player.Grounded = false;
        //
        //player.Rbody.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        //
        if (jumped!=null) {
            jumped(jumpForce);
        }
    }
    //private void ChannelJump(float charge) {
    //    JumpForceMax = 0;
    //    JumpForceMax = jumpForce + charge;
    //
    //    player.Rbody.AddForce(new Vector3(0, JumpForceMax, 0), ForceMode.Impulse);
    //    ChargeJump.jumpCharge -= ChannelJump;
    //}
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        player.Jumping = false;
    }
}
