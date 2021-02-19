using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
public class PlayerCommands : MonoBehaviour {
    private enum Inputs { X, Square, Triangle, Circle, Up, Down, Right, Left, Direction }
    #region Events
    public static event UnityAction<string> sendInput;
    public static event UnityAction<int> sendChain;
    #endregion

    private Coroutine fakeUpdate;
    private bool lockon;
    #region Outside Scripts
    private Animator anim;
    #endregion
    #region Anim parameters
    private int chain;
    #endregion
    [SerializeField] private List<Inputs> inputs;

    public int Chain { get => chain; set { chain = value; anim.SetInteger("ChainInput", chain); } }

    private void Awake() {
        inputs = new List<Inputs>(52);
    }
    void Start() {
        fakeUpdate = StartCoroutine(SlowUpdate());
        anim = GetComponent<Animator>();
        Player.lockOn += LockControl;
    }
    private void Update() {
        GetInputs();
    }
    private void GetInputs() {
        InputChains();
        if (lockon) {
            InputCombinations();
        }
        
        if (inputs.Count > 3) {
            ResetChain();
        }
    }
    private void FixedUpdate() {
        //AnalogInputs();
    }
    private IEnumerator SlowUpdate() {
        YieldInstruction wait = new WaitForSeconds(0.5f);
        while (isActiveAndEnabled) {
            yield return wait;
            ResetChain();
        }
    }
    private void OnMovement(InputValue value) {
        
            AddInput(Inputs.Direction);
        
    }
    private void OnDash(InputValue value) {
        //Chain = 7;
    }
    private void OnUp() {
        AddInput(Inputs.Up);
    }
    private void OnDown() {
        AddInput(Inputs.Down);
    }
    private void OnJump() {

        if (sendInput != null) {
            sendInput("X");
        }
        AddInput(Inputs.X);

    }
    private void OnEnergyShot() {

        if (sendInput != null) {
            sendInput("Triangle");
        }
        AddInput(Inputs.Triangle);

    }
    private void OnAttack() {

        if (sendInput != null) {
            sendInput("Square");
        }
        AddInput(Inputs.Square);

    }
    private void OnStyle() {

        if (sendInput != null) {
            sendInput("Circle");
        }
        AddInput(Inputs.Circle);

    }
    private void OnShoot() {
        Chain = 16;
    }
    private void OnHoldAttack() {
        Chain = 15;
    }
    private void OnHoldEnergy() {
        Chain = 17;
    }
    private void AddInput(Inputs button) {
        inputs.Add(button);
    }
    private void InputChains() {

        if (inputs.Contains(Inputs.Triangle) && inputs.Contains(Inputs.Circle)) {
            Debug.Log("Fire!BIcth");
            Chain = 9;
            ResetChain();
        }
        if (inputs.Contains(Inputs.X)) {
            Chain = 1;
        }
        if (inputs.Contains(Inputs.Square)) {
            Chain = 2;

        }
        if (inputs.Contains(Inputs.Triangle)) {
            Chain = 3;

        }
        if (inputs.Contains(Inputs.Circle)) {
            Chain = 4;
        }
        if (inputs.Contains(Inputs.Circle) && inputs.Contains(Inputs.Direction)) {

            Chain = 7;
        }
    }
    private void InputCombinations() {
        if (inputs.Contains(Inputs.Square) && inputs.Contains(Inputs.Up)) {
            Debug.Log("Up Attack!");
            if (sendInput != null) {
                sendInput("Up + Square");
            }
            ResetChain();
            Chain = 8;
        }
        if (inputs.Contains(Inputs.Square) && inputs.Contains(Inputs.Down)) {
            Debug.Log("Down Attack!");
            if (sendInput != null) {
                sendInput("Down + Square");
            }
            Chain = 5;
            ResetChain();
            //Insert Chain Here.
        }
        if (inputs.Contains(Inputs.Circle) && inputs.Contains(Inputs.Up)) {

            if (sendInput != null) {
                sendInput("Up + Circle");
            }
            Chain = 6;
            ResetChain();
        }
    }
    private void ResetChain() {
        inputs.Clear();
    }
    private void LockControl(bool val) {
        lockon = val;
    }
}
