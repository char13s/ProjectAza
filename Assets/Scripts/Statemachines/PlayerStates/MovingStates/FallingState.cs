using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class FallingState : StateMachineBehaviour
{
    public static event UnityAction gravity;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        if (gravity != null) {
            gravity();
        }
    }
}
