using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class LightDashing : StateMachineBehaviour
{
    public static event UnityAction<bool> sparkle;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        if (sparkle != null) {
            sparkle(true);
        }
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        if (sparkle != null) {
            sparkle(false);
        }
    }
}
