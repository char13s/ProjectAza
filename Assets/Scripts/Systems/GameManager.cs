using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

using Cinemachine;
public class GameManager : MonoBehaviour {
    [SerializeField] private GameObject zend;
    [SerializeField] private GameObject mainMEnuSpawn;
    [SerializeField] private CinemachineVirtualCamera titleCam;
    [SerializeField] private UnityEvent onPause;
    [SerializeField] private UnityEvent onUnPause;
    [SerializeField] private GameObject spawn;
    public static event UnityAction<Transform> spawnPlayer;
    public static event UnityAction<int> controlSwitcher;
    public static event UnityAction<bool> sealPlayer;
    public static event UnityAction<Transform> setCam;
    public static event UnityAction update;

    private bool pause;
    private GameObject spawnPoint;

    private int currentLevel;

    public int CurrentLevel { get => currentLevel; set { currentLevel = value; LoadLevel(); } }

    private void OnEnable() {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }
    private void OnDisable() {

        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }
    // Start is called before the first frame update
    void Start() {
        Player.pause += PauseState;
        LevelLoader.updateLevel += UpdateCurrentLevel;
        if (sealPlayer != null) {
            sealPlayer(true);
        }
    }
   private void Update() {
        if (update != null) {
            update();
        }
    }
    private void PauseState() {
        if (!pause) {
            Time.timeScale = 0;
            pause = true;
            onPause.Invoke();
            if (sealPlayer != null) {
                sealPlayer(true);
            }
            controlSwitcher.Invoke(2);
        }
        else {
            Time.timeScale = 1;
            pause = false;
            onUnPause.Invoke();
            if (sealPlayer != null) {
                sealPlayer(false);
            }
            controlSwitcher.Invoke(0);
        }
    }
    public void ActivatePlayer(bool val) {
        if (spawnPlayer != null) {
            spawnPlayer(mainMEnuSpawn.transform);
        }
    }
    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode) {
        if (scene.buildIndex == 0) {
            titleCam.m_Priority = 100;
        }
        else {
            titleCam.m_Priority = 0;
            if (sealPlayer != null) {
                sealPlayer(false);
            }
        }
        if (scene.buildIndex > 0) {
            ActivatePlayer(true);
            if (spawnPlayer != null) {
                spawnPlayer(spawnPoint.transform);

            }

        }
    }
    public void UnloadCurrentLevel() {
        SceneManager.UnloadSceneAsync(CurrentLevel);
        Time.timeScale = 1;
        pause = false;
        onUnPause.Invoke();
        if (sealPlayer != null) {
            sealPlayer(true);
        }
    }
    private void UpdateCurrentLevel(int level, GameObject spawn) {
        CurrentLevel = level;
        spawnPoint = spawn;

    }
    private void LoadLevel() {
        SceneManager.LoadSceneAsync(CurrentLevel, LoadSceneMode.Additive);
    }
    public void StartGame(int level) {
        CurrentLevel = level;
        spawnPoint = spawn;
    }
    public void QuitGame() {
        Application.Quit();
    }
}
