using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour {
    [SerializeField] internal EquipmentData data = new EquipmentData();
    private EquipmentInvent invent;
    private void Start() {
        invent = EquipmentInvent.GetEquipmentInvent();
    }
    public void PickUp() {
        invent.AddToInvent(data);
        Debug.Log("Picked up");
        Destroy(gameObject, 0.1f);
    }
}
