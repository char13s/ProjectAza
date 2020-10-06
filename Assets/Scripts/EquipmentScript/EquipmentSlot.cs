using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
public class EquipmentSlot : MonoBehaviour
{
    private EquipmentData item;
    private static EquipmentSlot lastSlotSelected;
    private Image image;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
        image = GetComponent<Image>();
    }

    private void SetEquipment(EquipmentData equip) {
        item = equip;
        
        image.sprite = item.Sprite;
    }
    private void OnClick() {
        lastSlotSelected = this;
    }
}
