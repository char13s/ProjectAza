using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class EquipmentInputs : MonoBehaviour
{
    private bool inputActive;
    private EquipmentInvent invent;
    public static event UnityAction<bool> openMenu;
    private void Start() {
        invent = EquipmentInvent.GetEquipmentInvent();
    }
    void Update()
    {
        if (inputActive) {
            Inputs();
        }
    }
    private void Inputs() {
        if (Input.GetButtonDown("Fire3")) {
            //turn on are you sure menu

            if (openMenu != null) {
                openMenu(true);
            }
        }

    }
    public void SetInputs(bool val) {
        inputActive = val;
    }
}
