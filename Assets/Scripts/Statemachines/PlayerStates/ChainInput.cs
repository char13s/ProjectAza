using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ChainInput : StateMachineBehaviour
{
    public static event UnityAction<int> sendChain;
    private Player player;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        if (sendChain != null) {
            sendChain(0);
        }
    }
}
