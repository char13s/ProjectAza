using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class AirCombos : StateMachineBehaviour {
    private Player pc;
    private bool input;
    [SerializeField] private bool hasNextInput;
    public static event UnityAction<bool> gravity;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        pc = Player.GetPlayer();
        pc.Rbody.velocity = Vector3.zero;
        if (gravity != null) {
            gravity(false);
        }
        input = false;
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        if (Input.GetButtonDown("Fire1")&&hasNextInput) {
            input = true;
            pc.CmdInput = 1;
        }
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        if (!input) { 
        if (gravity != null) {
            gravity(true);
        } 
        pc.CmdInput = 0;
        }
    }
}
