using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ChargeJump : StateMachineBehaviour
{
    public static event UnityAction<float> jumpCharge;
    public static event UnityAction jump;

    private float charge;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        charge = 0;
        if (jump != null) {
                jump();
            }
    }
}
