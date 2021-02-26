using UnityEngine;
using UnityEngine.Events;
public class PlayerStats 
{
    private float health;
    private float healthLeft;
    private float mp;
    private float mpLeft;
    private int battlePressure;

    public static event UnityAction updateUI;
    public float Health { get => health; set { health = value;if (updateUI != null) { updateUI(); } } }
    public float HealthLeft { get => healthLeft; set { healthLeft = value; if (updateUI != null) { updateUI(); } } }
    public float Mp { get => mp; set { mp = value; if (updateUI != null) { updateUI(); } } }
    public float MpLeft { get => mpLeft; set { mpLeft = value; if (updateUI != null) { updateUI(); } } }
    public int BattlePressure { get => battlePressure; set { battlePressure = value; if (updateUI != null) { updateUI(); } } }

    public void SetStatsDefault() {
        Health = 30;
        HealthLeft = Health;
        Mp = 20;
        MpLeft = Mp;
        BattlePressure = 10;
        EnemyBaseScript.sendBp += RecieveBP;
        SkillIncrease.sendIncrease += RecieveBP;
    }
    private void RecieveBP(int powah) => BattlePressure += powah;
}
