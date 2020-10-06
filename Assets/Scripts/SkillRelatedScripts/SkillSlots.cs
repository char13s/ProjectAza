using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlots : MonoBehaviour
{
    private Skills skill;
    [SerializeField]private Text quickSKillMenuSlot;
    private int mpRequired;
    private int id;
    [SerializeField] private Text skillName;
    public int MpRequired { get => mpRequired; set => mpRequired = value; }
    public Skills Skill { get => skill; set { skill = value;SkillSetUp(); } }

    public int ID { get => id; set => id = value; }

    private static SkillSlots lastSelectedSlot;
    // Start is called before the first frame update
    void Start()
    {
        Skills.sendSkill += SetSkill;
    }
    public void SetSkill(Skills skill) {
        lastSelectedSlot.Skill = skill;
    }
    public void SetLastSkillSlot() {
        lastSelectedSlot = this;
    }
    private void SkillSetUp() {
        ID = skill.SkillId;
        MpRequired = skill.MpCost;
        skillName.text = skill.SkillName;
        quickSKillMenuSlot.text = skill.SkillName;
    }  
}
