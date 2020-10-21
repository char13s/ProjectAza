using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class EquipmentInvent : MonoBehaviour
{
    [SerializeField]private List<EquipmentItemSlot> invent = new List<EquipmentItemSlot>(40);
    private static EquipmentInvent instance;
    private static EquipmentItemSlot lastSelectedSlot;

    public static event UnityAction clearDisplay;
    public static EquipmentItemSlot LastSelectedSlot { get => lastSelectedSlot; set => lastSelectedSlot = value; }

    public static EquipmentInvent GetEquipmentInvent() => instance;
    
    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        }
        else {
            instance = this;
        }
    }
    public void AddToInvent(EquipmentData item) {
        if (SearchForSlot() != null) {
            SearchForSlot().Item = item;
            Debug.Log("Item added");
        }
        else {
            Debug.Log("Slot null");
        
        //send message that invent is full
    }
    }
    private EquipmentItemSlot SearchForSlot() {
        foreach (EquipmentItemSlot item in invent) {
            if (item.Item == null) { 
                return item;
            }
        }
        return null;
    }
    public void EmptyASlot() {
        lastSelectedSlot.ClearASlot();
        if (clearDisplay != null) {
            clearDisplay();
        }
    }
}
