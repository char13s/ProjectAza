using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class Skills : MonoBehaviour
{
    [SerializeField] private string skillName;
    [SerializeField] private int skillId;
    [SerializeField] private int mpCost;
    [SerializeField] private Text nameText;

    public int MpCost { get => mpCost; set => mpCost = value; }
    public string SkillName { get => skillName; set => skillName = value; }
    public int SkillId { get => skillId; set => skillId = value; }

    //[SerializeField] private int unlocked
    public static event UnityAction<Skills> sendSkill;
    // Start is called before the first frame update
    void Start()
    {
        nameText.text = skillName;
    }
    public void SetSkill() {
        if (sendSkill != null) {
            sendSkill(this);
        }
    }
}
