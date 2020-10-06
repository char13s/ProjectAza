using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class LegControlState : StateMachineBehaviour
{
    public static event UnityAction<int> legLayer;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        if (legLayer!=null){
            legLayer(1);
        }
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        if (legLayer != null) {
            legLayer(0);
        }
    }
}
