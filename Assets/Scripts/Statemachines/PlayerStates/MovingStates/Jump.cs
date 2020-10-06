using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Jump : StateMachineBehaviour
{
    private Player player;
    [SerializeField] private float jumpForce;
     private float jumpForceMax;

    public static event UnityAction<bool> climbable;
    public float JumpForceMax { get => jumpForceMax; set => jumpForceMax =value; }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        player = Player.GetPlayer();
        //ChargeJump.jumpCharge += ChannelJump;
        //if (climbable!=null) {
        //    climbable(true);
        //}
        player.Rbody.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
    }
    //private void ChannelJump(float charge) {
    //    JumpForceMax = 0;
    //    JumpForceMax = jumpForce + charge;
    //
    //    player.Rbody.AddForce(new Vector3(0, JumpForceMax, 0), ForceMode.Impulse);
    //    ChargeJump.jumpCharge -= ChannelJump;
    //}
    //public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
    //    if (climbable != null) {
    //        climbable(false);
    //    }
    //}
}
