using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class DisplayWindow : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Text description;
    [SerializeField] private EventSystem eventSys;

    private void OnEnable() {
        EquipmentInvent.clearDisplay += ClearDisplay;
    }
    private void OnDisable() {
        
    }
    
    public void SetDisplay() {
        if (eventSys.currentSelectedGameObject.GetComponent<EquipmentItemSlot>()!=null) {
            image.sprite=eventSys.currentSelectedGameObject.GetComponent<EquipmentItemSlot>().Item.Sprite;
            description.text= eventSys.currentSelectedGameObject.GetComponent<EquipmentItemSlot>().Item.Description;
        }
    }
    private void ClearDisplay() {
        image.sprite = null;
        description.text = "";
    }
}
