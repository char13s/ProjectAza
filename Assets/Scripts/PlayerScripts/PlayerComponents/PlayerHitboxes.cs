using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitboxes : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject basicHitBox;
    [SerializeField] private GameObject slamHitBox;
    [SerializeField] private GameObject riseHitBox;
    //[SerializeField] private GameObject 
    void Start()
    {
        
    }
    private void SlamHitbox(bool val) {
        slamHitBox.SetActive(val);
    }
    private void RiseHitBox(bool val) {
        riseHitBox.SetActive(val);
    }
}
