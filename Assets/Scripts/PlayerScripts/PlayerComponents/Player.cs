﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    #region Animation states
    private bool moving, attacking, jumping, readyJump, grounded, wallInReach, wallStuck;
    private int cmdInput, skillId;
    private int lStickX, lStickY;
    private bool teleport;
    private bool shoot;
    private bool testButton;
    private bool lightDash;
    private bool attack;
    #endregion
    #region Script refs
    private Animator anim;
    private Rigidbody rbody;
    private PlayerCommands comm;
    private PlayerLockon playerTarget;
    private PlayerInput map;

    #endregion
    #region Obj refs

    [Header("Objects")]
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject defaultLockOnPoint;
    [SerializeField] private GameObject throwingPortal;
    [Header("Body Refs")]
    [SerializeField] private GameObject meshRef;
    [SerializeField] private GameObject body;
    [SerializeField] private GameObject hurtBox;
    [SerializeField] private GameObject interactionBox;
    //[SerializeField] private GameObject hitBox;
    [SerializeField] private GameObject dodgeBox;
    [SerializeField] private GameObject wallChecker;
    [SerializeField] private GameObject hair;
    [Space]
    [Header("Effects")]
    [SerializeField] private GameObject leftDash;
    [SerializeField] private GameObject rightDash;
    [SerializeField] private GameObject swordSpawn;
    [SerializeField] private GameObject UIAura;
    [SerializeField] private GameObject auraExplode;
    [SerializeField] private GameObject spawnIn;
    [SerializeField] private GameObject teleportSparks;
    [Space]
    [Header("SkillButtons")]
    [SerializeField] private SkillSlots triangle;
    [SerializeField] private SkillSlots circle;
    [SerializeField] private SkillSlots square;
    [SerializeField] private SkillSlots x;
    [Space]
    [Header("Materials")]
    private SkinnedMeshRenderer current;
    [SerializeField] private Material normal;
    [SerializeField] private Material transparent;
    [SerializeField] private Material shiny;
    [Space]
    [Header("Hair Materials")]
    [SerializeField] private Material normalHair;
    [SerializeField] private Material electricHair;
    [SerializeField] private Material fireHair;
    [SerializeField] private Material iceHair;
    [SerializeField] private Material darkHair;
    [SerializeField] private Material lightHair;
    #endregion
    #region Coroutines
    private Coroutine mpDrain;
    #endregion
    private Vector2 displacement;
    private Vector3 direction;
    private Vector3 moveDirection;
    private bool skillButton,teleportButton;
    private int weapon;
    private bool lockedOn;
    private bool cantMove,cancelCamMovement;
    private float turnSmoothVelocity;

    private static Player instance;
    #region Input seals
    private bool inputSeal;
    private bool teleportSeal;
    #endregion


    #region Constructors
    internal PlayerStats stats = new PlayerStats();
    private AxisButton R2 = new AxisButton("R2");
    private AxisButton L2 = new AxisButton("L2");

    #endregion
    #region Events
    public static event UnityAction<float> jumpForce;
    public static event UnityAction pause;
    public static event UnityAction<bool> skills;
    public static event UnityAction<bool> lockOn;
    public static event UnityAction<int> transformation;
    #endregion
    #region Getters and Setters
    public bool Moving { get => moving; set { moving = value; anim.SetBool("Moving", moving); } }
    public Vector2 Displacement { get => displacement; set => displacement = value; }
    public int CmdInput { get => cmdInput; set { cmdInput = value; anim.SetInteger("CmdInput", cmdInput); } }
    public bool Attacking { get => attacking; set { attacking = value; anim.SetBool("AttackStance", attacking); } }
    public Rigidbody Rbody { get => rbody; set => rbody = value; }
    public bool Jumping { get => jumping; set { jumping = value; anim.SetBool("Jump", jumping); } }
    public bool Grounded { get => grounded; set { grounded = value; anim.SetBool("Grounded", grounded); } }
    public bool ReadyJump { get => readyJump; set { readyJump = value; anim.SetBool("ReadyJump", readyJump); } }
    public int SkillId { get => skillId; set { skillId = value; anim.SetInteger("SkillId", skillId); } }

    public bool TeleportButton { get => teleportButton; set { teleportButton = value; anim.SetBool("TeleportButton", teleportButton); } }

    public GameObject DefaultLockOnPoint { get => defaultLockOnPoint; set => defaultLockOnPoint = value; }
    public bool CancelCamMovement { get => cancelCamMovement; set => cancelCamMovement = value; }
    public bool LockedOn { get => lockedOn; set { lockedOn = value; CancelCamMovement = value; if (lockOn != null) { lockOn(value); } } }
    public bool SkillButton { get => skillButton; set { skillButton = value; SkillTrigger(); } }

    public int LStickX { get => lStickX; set { lStickX = value; anim.SetInteger("X", lStickX); } }
    public int LStickY { get => lStickY; set { lStickY = value; anim.SetInteger("Y", lStickY); } }

    public bool Teleport { get => teleport; set { teleport = value; anim.SetBool("Teleport", teleport); } }

    public bool Shoot { get => shoot; set { shoot = value; anim.SetBool("Shoot", shoot); } }

    public bool TestButton { get => testButton; set => testButton = value; }
    public GameObject Body { get => body; set => body = value; }
    public GameObject Hair { get => hair; set => hair = value; }
    public bool CantMove { get => cantMove; set => cantMove = value; }
    public bool LightDash { get => lightDash; set { lightDash = value; anim.SetBool("LightDash", lightDash); } }

    public Vector3 Direction { get => direction; set => direction = value; }
    public int Weapon { get => weapon; set { weapon = value; anim.SetInteger("Weapon",weapon); } }

    public PlayerCommands Comm { get => comm; set => comm = value; }
    public bool Attack { get => attack; set { attack = value; anim.SetTrigger("Attack"); } }

    public Vector3 MoveDirection { get => moveDirection; set => moveDirection = value; }
    #endregion
    public static Player GetPlayer() => instance;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        }
        else {
            instance = this;
        }
        map = GetComponent<PlayerInput>();
        anim = GetComponent<Animator>();
        Rbody = GetComponent<Rigidbody>();
        current = meshRef.GetComponent<SkinnedMeshRenderer>();
        Comm = GetComponent<PlayerCommands>();
        playerTarget = GetComponent<PlayerLockon>();
    }
    void Start() {
        
        stats.SetStatsDefault();
        LegControlState.legLayer += LegLayerControl;
        GroundChecker.grounded += GroundCheck;
        //WallChecker.stickToWall += WallCheck;
        Dash.bodyControl += BodyControl;
        //Cursor.lockState = CursorLockMode.Locked;
        ChargeJump.jump += Jumps;
        Jump.jumped += Jumped;
        
        // ThrowingPortal.sendSpot += TeleportHere;
        GameManager.spawnPlayer += TeleportHere;
        SceneDialogue.sealPlayerInput += SetInputSeal;
        GameManager.sealPlayer += SetInputSeal;
        ChainInput.sendChain += ChainControl;
        AirCombos.gravity += GravityControl;
        PlayerLockon.enemyDetected += AttackState;
        PlayerLockon.switchMaps += SwitchControls;
        LightDashing.sparkle += LightUp; 
        //LightDashing.vanish += BodyControl;
        LightDashing.sparkle +=PhaseUp;
        GameManager.controlSwitcher += SwitchControls;
        //SlamState.gravity += GravityControl;
        //WallCheckState.wallCheck += WallCheck;
    }

    private void Update() {
        Move();
    }
    private IEnumerator SwitchMaterial() {

        while (isActiveAndEnabled) {
            yield return null;
            float lerp = Mathf.PingPong(Time.time, 0.2f) / 0.2f;
            GetComponentInChildren<SkinnedMeshRenderer>().material.Lerp(normal, shiny, lerp);
        }
    }
    #region Movement 
    private void Move() {
        Direction = cam.transform.TransformDirection(new Vector3(displacement.x, 0, displacement.y).normalized);
        if (displacement.magnitude >= 0.1f) {
            if (!cantMove) {
                Moving = true;
            }
            direction.y = 0;
            Vector3 rot = Vector3.Normalize(Direction);
            rot.y = 0;
            MoveDirection = Quaternion.Euler(rot) * Camera.main.transform.forward;
            transform.rotation = Quaternion.LookRotation(rot);
        }
        else {
            Moving = false;
        }
    }
    private void OnMovement(InputValue value) {
        Displacement = value.Get<Vector2>();

    }
    #endregion
    #region Action Mappings
    
    private void OnAttack(InputValue value) {
        Debug.Log("Attack");
        if (value.isPressed) { 
        anim.SetTrigger("Attack");}
    }
    private void OnEnergyShot() {
        Debug.Log("Shoot");
    }
    private void OnJump() {
        Comm.Chain = 1;
    }
    private void OnStyle() {
        Debug.Log("Style");
    }
    private void OnUp() {
        Debug.Log("Up");
    }
    private void OnPause() {
        if (pause != null) {
            pause();
        }
        print("What the hell");
    }
    private void OnTransform() {
        anim.SetTrigger("Transform");
    }
    private void OnLightDash(InputValue value) {
        if (value.isPressed) {
            LightDash = true;
        }
        else {
            
        }
        
    }
    private void OnLightDashRelease() {
        LightDash = false;
    }
    private void OnLockOn(InputValue value) {//R1

        if (value.isPressed) {
            LockedOn = true;
            cancelCamMovement = true;
        }
        else {
            LockedOn = false;
            cancelCamMovement = false;
        }
    }
    private void OnSkill(InputValue value) {
        if (value.isPressed) {
            Debug.Log("Skill");
            if (stats.MpLeft >= square.MpRequired) {
                SkillId = square.ID;
                stats.MpLeft -= square.MpRequired;
            }
        }
    }
    private void OnWeaponSwitch(InputValue value) {
        if (value.isPressed) {
            Weapon = 2;
            Debug.Log("Bruh");
            Debug.Log(Weapon);
        }
    }
    #endregion
    #region Inputs
    /*
    private void ShootLight() {
        if (Input.GetButtonDown("Fire3")) {
            Shoot = true;
            stats.MpLeft--;
        }
    }
    private void SkillButtonControl() {
        if (R2.GetButtonDown() || Input.GetButtonDown("R2")) {
            Debug.Log("R2 pressed");
            SkillButton = true;
        }
        if (R2.GetButtonUp() || Input.GetButtonUp("R2")) {
            SkillButton = false;
        }
    }
    private void JumpCharge() {//X
        if (Input.GetButtonDown("Jump") && !jumping && grounded) {
            //UnGround();
            //Jumping = true;
            //Teleport = true;
            cancelCamMovement = true;
            stats.MpLeft--;
            Instantiate(teleportSparks, transform.position, teleportSparks.transform.rotation);
            //Grounded = false;
            //StartCoroutine(WaitToResetGround());
        }

    }
    private void OnSkillSquare() {
        
    }
    private void OnSkillTriangle() {
        if (stats.MpLeft >= triangle.MpRequired) {
            SkillId = triangle.ID;
            stats.MpLeft -= triangle.MpRequired;
            Debug.Log("Skill for triangle used");
        }
    }
    private void OnSkillX() {
        if (stats.MpLeft >= x.MpRequired) {
            SkillId = x.ID;
            stats.MpLeft -= x.MpRequired;
        }
    }
    private void OnSkillCircle() {
        if (stats.MpLeft >= circle.MpRequired) {
            SkillId = circle.ID;
            stats.MpLeft -= circle.MpRequired;
        }
    }
    
        private void Interact() {////circle
            if (Input.GetButtonDown("Fire2")) {
                interactionBox.SetActive(true);
            }
            if (Input.GetButtonUp("Fire2")) {
                interactionBox.SetActive(false);
            }
        }
        private void Phase() {//L1
            if (Input.GetButtonDown("L1") && stats.MpLeft > 0) {
                PhaseUp();
            }
            if (Input.GetButton("L1") && stats.MpLeft > 0) {
                Debug.Log("Phase is up");
            }
            if (Input.GetButtonUp("L1")) {
                PhaseOff();
                //shoot out power down particles
            }
        }*/
    private void PhaseUp(bool val) {
        if (val) {
            mpDrain = StartCoroutine(MpDrain(1));
            current.material = transparent;
        }
        else {
            StopCoroutine(mpDrain);
            current.material = normal;

        }
        //UIAura.SetActive(true);
        //hurtBox.SetActive(false);
        //dodgeBox.SetActive(true);
        //auraExplode.SetActive(true);
        //Instantiate(leftDash, transform.position, Quaternion.identity);
        //Instantiate(rightDash, transform.position, Quaternion.identity);
    }
    #endregion
    #region Form Logic
    private void Deform() {

    }
    private void FormControl() {

    }
    #endregion
    #region Event Methods

    private void SwitchControls(int val) {
        switch (val) {
            case 0:
                map.SwitchCurrentActionMap("OpenWorldControls");
                break;
            case 1:
                map.SwitchCurrentActionMap("CombatControls");
                break;
            case 2:
                map.SwitchCurrentActionMap("PauseControls");
                break;
        }
        //print(map.currentActionMap);
    }
    private void AttackState(bool val) {
        Attacking = val;
    }
    private void TeleportToEnemey() {
        if (playerTarget.Enemies.Count > 0) {
            transform.position = playerTarget.EnemyLockedTo().gameObject.transform.position + new Vector3(0, 4, 0);
        }
        else {
            transform.position = transform.position + transform.forward * 15 + new Vector3(0, 5, 0);
        }
    }
    private void SkillTrigger() {
        if (skills != null) {
            skills(skillButton);
        }
    }
    private void Jumps() {
        if (Input.GetButtonDown("Jump") && !jumping && grounded) {
            Jumping = true;
            wallChecker.SetActive(true);
            Debug.Log("fuck yo jump");
        }
    }
    private void GroundCheck(bool val) {
        Grounded = val;
    }
    //private void WallCheck(bool val) {
    //    wallInReach = val;
    //}
    private void WallCheck(bool val) {
        wallChecker.SetActive(val);
    }
    private void LegLayerControl(int weight) {
        anim.SetLayerWeight(1, weight);
    }
    private void TeleportHere(Transform spot) {
        transform.position = spot.position + new Vector3(0, 1, 0);
    }
    private void ChainControl(int val) {
        Comm.Chain = val;
    }
    private void GravityControl(bool val) {
        rbody.useGravity = val;
    }
    
    private void SetInputSeal(bool val) {
        inputSeal = val;
    }
    private void BodyControl(bool val) {
        Body.SetActive(val);
        spawnIn.SetActive(val);
        if (val) {
            UIAura.SetActive(false);
            //rbody.velocity = new Vector3(0, 0, 0);
            Instantiate(teleportSparks, transform.position, teleportSparks.transform.rotation);
        }
        else {
            Teleport = false;
            UIAura.SetActive(true);

        }

    }
    private void LightUp(bool val) {
        UIAura.SetActive(val);

    }
    private void Jumped(float val) {
        UnGround();
        Grounded = false;
        Vector3 speed = Rbody.velocity;
        Rbody.AddForce(Vector3.up*val, ForceMode.Impulse);
        print(rbody.velocity);
        StartCoroutine(WaitToResetGround());
    }
    #endregion
    #region Coroutines
    private IEnumerator MpDrain(int rate) {
        YieldInstruction wait = new WaitForSeconds(rate);
        while (isActiveAndEnabled) {
            yield return wait;
            if (stats.MpLeft == 0) {
                PhaseUp(false);
                LightDash = false;
            }
            else {
                stats.MpLeft--;
            }
        }
    }
    private IEnumerator WaitToResetGround() {
        YieldInstruction wait = new WaitForSeconds(0.85f);
        yield return wait;
        GroundChecker.grounded += GroundCheck;
    }

    private IEnumerator WaitToResetWallCheck() {
        YieldInstruction wait = new WaitForSeconds(0.2f);
        yield return wait;
        WallChecker.stickToWall += WallCheck;
    }
    private IEnumerator WaitToStopJump() {
        yield return null;
        yield return null;
        yield return null;
        Jumping = false;
        ReadyJump = false;
    }

    #endregion


    private void UnGround() {
        GroundChecker.grounded -= GroundCheck;
    }
    private void UnWall() {
        WallChecker.stickToWall -= WallCheck;
    }

    #region Cooldowns
    private IEnumerator WaitToTeleport() {
        YieldInstruction wait = new WaitForSeconds(4);
        yield return wait;
        //removeinputLock on teleport
    }
    #endregion
}
