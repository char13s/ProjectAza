using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
public class EquipmentItemSlot : MonoBehaviour   
{
    [SerializeField] private Text itemName;
    [SerializeField]private Image image;
    private EquipmentData item;

    public EquipmentData Item { get => item; set { item = value;SetEquipment(); } }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
        
        //GetComponent<Button>().OnSelect();
    }
    private void OnClick() {
        //if item is not null then prompt open the window for what to do with the item
    }
    private void DisplayInfo() {
        //send item info to display in invent if not null
    }
    private void SetEquipment() {
        image.sprite = item.Sprite;
    }
}
