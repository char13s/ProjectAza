using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class LightDashing : StateMachineBehaviour
{
    public static event UnityAction<bool> sparkle;
    public static event UnityAction<bool> vanish;
    private Player player;
    [SerializeField] private float move;
    [SerializeField] private GameObject steps;
    private GameObject stepRef;
    private Vector3 speed;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        player = Player.GetPlayer();
        if (sparkle != null) {
            sparkle(true);
        }
        if (vanish != null) {
            vanish(false);
        }
        //stepRef = Instantiate(steps,);
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        if (!player.CantMove) {
            if (player.Direction.magnitude > 0) {
                speed = move * player.Direction.normalized;
                speed.y = player.Rbody.velocity.y;
                
            }
            else {
                speed = move * player.transform.forward;
                speed.y = player.Rbody.velocity.y;
            }
            player.Rbody.velocity = speed;
        }
        //player.Rbody.velocity.y = 0;
        //player.transform.position+= player.Displacement * move*Time.delta;
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        if (sparkle != null) {
            sparkle(false);
        }
        if (vanish != null) {
            vanish(true);
        }
    }
}
