using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class AirCombos : StateMachineBehaviour
{
    private bool input;
    [SerializeField]private bool hasNext;
    public static UnityAction<bool> gravity;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        input = false;
        if (gravity != null) {
            gravity(false);
        }
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        if (Input.GetButtonDown("Fire1")&&hasNext) {
            input = true;
        }
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        if (!input) { 
        if (gravity != null) {
            gravity(true);
        }
        }
    }
}
