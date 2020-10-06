using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Text health;
    [SerializeField] private Text battlePower;
    [SerializeField] private Text mp;
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = Player.GetPlayer();
        PlayerStats.updateUI += UpdateUI;
    }
    private void UpdateUI() {
        health.text = "Health: "+player.stats.HealthLeft.ToString();
        battlePower.text = "BP: " + player.stats.BattlePressure.ToString();
        mp.text = "Mp: " + player.stats.MpLeft.ToString();
    }
}
