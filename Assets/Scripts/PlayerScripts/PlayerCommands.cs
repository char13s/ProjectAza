using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PlayerCommands : MonoBehaviour
{
    private enum Inputs { X, Square, Triangle, Circle, Up, Down }
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
        if (lockon) {
            GetInputs();
        }

    }
    private void GetInputs() {
        InputChains();
        TriangleButton();
        XButton();
        SquareButton();
        CircleButton();
        if (inputs.Count > 3) {
            ResetChain();
        }

    }
    private void FixedUpdate() {
        AnalogInputs();
    }
    private IEnumerator SlowUpdate() {
        YieldInstruction wait = new WaitForSeconds(0.05f);
        while (isActiveAndEnabled) {
            yield return wait;
            ResetChain();
        }
    }
    private void AnalogInputs() {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        if (y > 0.7f) {
            AddInput(Inputs.Up);
        }
        if (y < -0.7f) {
            AddInput(Inputs.Down);
        }
    }
    private void XButton() {
        if (Input.GetButtonDown("Jump")) {
            if (sendInput != null) {
                sendInput("X");
            }
            AddInput(Inputs.X);
        }
    }
    private void TriangleButton() {
        if (Input.GetButtonDown("Fire3")) {
            if (sendInput != null) {
                sendInput("Triangle");
            }
            AddInput(Inputs.Triangle);
        }
    }
    private void SquareButton() {
        if (Input.GetButtonDown("Fire1")) {
            if (sendInput != null) {
                sendInput("Square");
            }
            AddInput(Inputs.Square);
        }
    }
    private void CircleButton() {
        if (Input.GetButtonDown("Fire2")) {
            if (sendInput != null) {
                sendInput("Circle");
            }
            AddInput(Inputs.Circle);
        }
    }
    private void AddInput(Inputs button) {
        inputs.Add(button);
    }
    private void InputChains() {
        if (Input.GetButtonDown("Fire2") && Input.GetButtonDown("Fire3")) {
            Debug.Log("Fire!");
        }
        if (inputs.Contains(Inputs.Triangle) && inputs.Contains(Inputs.Circle)) {
            Debug.Log("Fire!BIcth");
            ResetChain();
        }
        if (inputs.Contains(Inputs.Triangle) && inputs.Contains(Inputs.Up)) {
            Debug.Log("Up Attack!");
            ResetChain();
            Chain = 5;
        }
        if (inputs.Contains(Inputs.Triangle) && inputs.Contains(Inputs.Down)) {
            Debug.Log("Down Attack!");
            ResetChain();

        }
        if (inputs.Contains(Inputs.X) && inputs.Contains(Inputs.Up)) {
            Debug.Log("Teleport");
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
