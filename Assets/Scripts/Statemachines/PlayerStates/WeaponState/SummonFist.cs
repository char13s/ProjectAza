using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class SummonFist : StateMachineBehaviour
{
    private Player player;
    [SerializeField] private bool isActive;
    public static event UnityAction<bool> summonWeapon;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        if (summonWeapon != null) {
            summonWeapon(isActive);
        }
    }
}
