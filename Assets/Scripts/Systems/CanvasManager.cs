using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CanvasManager : MonoBehaviour {
    [SerializeField] private GameObject playerUI;
    [Header("Main Menu Canvases")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject continueCanvas;
    [SerializeField] private GameObject episodeSelect;
    [SerializeField] private GameObject backGround;
    [Header("Pause Canvases")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject characters;
    [SerializeField] private GameObject skills;
    [SerializeField] private GameObject equipment;
    [SerializeField] private GameObject options;
    [SerializeField] private GameObject quitMenu;
    [Header("Skill Menu Stuff")]
    [SerializeField] private GameObject skillMenu;
    [SerializeField] private GameObject skillList;
    [Header("Equipment Menu Stuff")]
    [SerializeField] private GameObject equipInvent;
    [SerializeField] private GameObject areYouSureEquip;
    private void Start() {
        TurnCanvasOff(true);
        Player.skills += SkillAccessMenu;
        EquipmentInputs.openMenu += AreYouSureEquip;
        //backGround.SetActive(true);
    }
    public void TurnCanvasOff(bool val) {//Main Menu control
        mainMenu.SetActive(val);
    }
    public void OpenEpisodeSelect(bool val) {
        episodeSelect.SetActive(val);
    }
    //pause menu stuff
    public void PauseMenuControl(bool val) {
        pauseMenu.SetActive(val);
    }
    public void SkillMenuContol(bool val) {
        skills.SetActive(val);
    }
    public void EquipmentControl(bool val) {
        equipment.SetActive(val);
    }
    public void OptionsControl(bool val) {
        options.SetActive(val);
    }
    public void QuitMenuControl(bool val) {
        quitMenu.SetActive(val);
    }
    public void CharacterMenuControl(bool val) {
        characters.SetActive(val);
    }
    //inner menu stuff
    public void SkillListControl(bool val) {
        skillList.SetActive(val);
    }
    public void BackGround(bool val) {
        //backGround.SetActive(val);
    }
    private void SkillAccessMenu(bool val) {
        skillMenu.SetActive(val);
    }
    public void EquipmentInventControl(bool val) {
        equipInvent.SetActive(val);
    }
    public void AreYouSureEquip(bool val) {
        areYouSureEquip.SetActive(val);
    }
}
