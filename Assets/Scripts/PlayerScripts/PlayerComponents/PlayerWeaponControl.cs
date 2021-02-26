using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponControl : MonoBehaviour
{
    [SerializeField] private GameObject katana;
    [SerializeField] private GameObject scabbard;
    [SerializeField] private GameObject fistL;
    [SerializeField] private GameObject fistR;
    [SerializeField] private GameObject wand;
    [SerializeField] private GameObject azaSword;
    [SerializeField] private GameObject greatSword;
    // Start is called before the first frame update
    void Start()
    {
        SummonAzaSword.summonWeapon += SummonLightSword;
        SummonFist.summonWeapon += SummonFireFist;
        SummonKatana.summonWeapon += SummonElectroKatana;
    }

    // Update is called once per frame

    #region Action Mappings
    private void WeaponSwitch1() { 
        
    }
    private void WeaponSwitch2() {

    }
    private void WeaponSwitch3() {

    }
    private void WeaponSwitch4() {

    }

    #endregion
    private void SummonLightSword(bool val) {
        //Instantiate(swordSpawn, transform.position, Quaternion.identity);
        azaSword.SetActive(val);
    }
    private void SummonElectroKatana(bool val) {
        katana.SetActive(val);
        scabbard.SetActive(val);
    }
    private void SummonFireFist(bool val) {
        fistL.SetActive(val);
        fistR.SetActive(val);
    }


}
