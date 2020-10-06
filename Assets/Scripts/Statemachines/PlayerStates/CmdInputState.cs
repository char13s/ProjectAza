﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CmdInputState : StateMachineBehaviour
{
    private Player pc;
    [SerializeField] private float move;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        pc = Player.GetPlayer();
        pc.CmdInput = 0;
        pc.Rbody.AddForce(pc.transform.forward*move,ForceMode.Impulse);
        
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        //GetInput();
    }
    private void GetInput() {
        if (Input.GetButtonDown("Square")) {
            pc.CmdInput = 1;
        }

        if (Input.GetButtonDown("Triangle")) {
            pc.CmdInput = 2;
        }

    }
}
