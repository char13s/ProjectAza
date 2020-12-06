using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class SlamState : StateMachineBehaviour {
    private Player pc;
    //private bool input;
    //[SerializeField] private bool hasNextInput;
    [SerializeField] private float slamForce;
    public static event UnityAction<bool> gravity;
    public static event UnityAction<int> slam;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        pc = Player.GetPlayer();
        pc.Rbody.AddForce(new Vector3(0,slamForce,0),ForceMode.Impulse);
        if (gravity != null) {
            gravity(true);
        }
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        if (slam != null) {
            slam(0);
        }
    }
}
