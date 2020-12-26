using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ChainInput : StateMachineBehaviour
{
    public static UnityAction<int> sendChain;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        if (sendChain != null) {
            sendChain(0);
        }
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        if (sendChain != null) {
            sendChain(0);
        }
    }
}
