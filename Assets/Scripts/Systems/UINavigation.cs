using UnityEngine;
using UnityEngine.EventSystems;
public class UINavigation : MonoBehaviour
{
    [SerializeField] private EventSystem eventSys;
    [Header("First Button Selects")]
    [SerializeField] private GameObject topOfPauseMenu;
    [SerializeField] private GameObject skillMenu;
    [SerializeField] private GameObject equipmentMenu;
    [SerializeField] private GameObject episodeSelectMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject characterMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject quitMenu;
    [SerializeField] private GameObject skillList;
    [SerializeField] private GameObject equipInvent;
    [SerializeField] private GameObject areYouSureEquipMenu;
    public void OnPause() {
        eventSys.SetSelectedGameObject(topOfPauseMenu);
    }
    public void OnSkillMenuOpen() {
        eventSys.SetSelectedGameObject(skillMenu);
    }
    public void OnEquipmentOpen() {
        eventSys.SetSelectedGameObject(equipmentMenu);
    }
    public void OnEpisodeSelectOpen() {
        eventSys.SetSelectedGameObject(episodeSelectMenu);
    }
    public void OnMainMenuOpen() {
        eventSys.SetSelectedGameObject(mainMenu);
    }
    public void OnCharacterMenuOpen() {
        eventSys.SetSelectedGameObject(characterMenu);
    }
    public void OnOptionsMenuOpen() {
        eventSys.SetSelectedGameObject(optionsMenu);
    }
    public void OnQuitMenuOpen() {
        eventSys.SetSelectedGameObject(quitMenu);
    }
    public void OnSkillListOpen() {
        eventSys.SetSelectedGameObject(skillList);
    }
    public void EquipInventOpen() {
        eventSys.SetSelectedGameObject(equipInvent);
    }
    public void GetSelectedSlot() {
        if (eventSys.currentSelectedGameObject.GetComponent<EquipmentItemSlot>() != null) { 
        EquipmentInvent.LastSelectedSlot= eventSys.currentSelectedGameObject.GetComponent<EquipmentItemSlot>();
        }

    }
}
