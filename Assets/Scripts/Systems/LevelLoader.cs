using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.Events;
[RequireComponent(typeof(Button))]
public class LevelLoader : MonoBehaviour {
    [SerializeField] private int level;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private Button button;


    public static event UnityAction turnOffCanvas;
    public static event UnityAction<int,GameObject> updateLevel;

    private void OnEnable() {


    }
    private void OnDisable() {



    }
    void Start() {
        button.onClick.AddListener(OnClick);
    }

    private void OnClick() {
        if (updateLevel != null) {
            updateLevel(level,spawnPoint);
        }
    }

}
