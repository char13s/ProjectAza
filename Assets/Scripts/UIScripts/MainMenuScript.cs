using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject Canvas;
    [SerializeField] private GameObject episodeSelectCanvas;
    [SerializeField] private GameObject black;
    //[SerializeField] private Button continueButton;
    //[SerializeField] private Button newGameButton;
    //[SerializeField] private Button loadGameButton;
    // Start is called before the first frame update

    public void BackGroundControl(bool val) {
       // black.SetActive(val);
    }
}
