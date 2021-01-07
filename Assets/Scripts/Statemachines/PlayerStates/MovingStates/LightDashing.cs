using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class LightDashing : StateMachineBehaviour
{
    public static event UnityAction<bool> sparkle;
    public static event UnityAction<bool> vanish;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        if (sparkle != null) {
            sparkle(true);
        }
        if (vanish != null) {
            vanish(false);
        }
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        
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
