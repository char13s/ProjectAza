using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformationState : StateMachineBehaviour
{
    [SerializeField] private GameObject firBurst;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        Instantiate(firBurst,Player.GetPlayer().transform.position,Quaternion.identity);
    }
}
