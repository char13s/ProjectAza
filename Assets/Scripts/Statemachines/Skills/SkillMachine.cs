using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillMachine : StateMachineBehaviour {
    [SerializeField] private int weaponAssigned;
    [SerializeField] private int currentWeapon;
    private Player player;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        player = Player.GetPlayer();
        player.SkillId = 0;
        currentWeapon = player.Weapon;
        WeaponAssign();
    }
    private void WeaponAssign() {
        switch (weaponAssigned) {
            case 0:
                break;
        }
    }
}
