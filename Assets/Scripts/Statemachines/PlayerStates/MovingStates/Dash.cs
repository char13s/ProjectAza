using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Dash : StateMachineBehaviour
{
    [SerializeField] private GameObject trail;
    private Player player;
    [SerializeField] private float moveSpeed;
    public static event UnityAction<bool> bodyControl;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        player = Player.GetPlayer();
        player.Moving = false;
        player.CancelCamMovement = true;
        //if (bodyControl != null) {
        //    bodyControl(false);
        //    
        //}
        //trail.SetActive(true);
        player.Rbody.velocity = new Vector3(0,0,0);
        if (player.Displacement != Vector2.zero) {
            player.Rbody.AddForce(player.Displacement * moveSpeed, ForceMode.Impulse);
        }
        else {
            player.Rbody.AddForce(player.transform.forward * moveSpeed, ForceMode.Impulse);
        }
        
        //player.transform.position = Vector3.MoveTowards(player.transform.position, player.DefaultLockOnPoint.transform.position, moveSpeed);
        //Turn mesh invisible
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        //make mesh reappear
       if (bodyControl != null) {
           bodyControl(true);
       }
        //trail.SetActive(false);
        player.CancelCamMovement = false;
        player.Teleport = false;
        //player.Rbody.velocity = new Vector3(0, 0, 0);
    }
}
