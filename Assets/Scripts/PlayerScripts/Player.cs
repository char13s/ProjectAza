using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Player : MonoBehaviour {
    #region Animation states
    private bool moving;
    private bool attacking;
    private int cmdInput;
    private bool jumping;
    private bool readyJump;
    private bool grounded;
    private int lStickX;
    private int lStickY;
    private bool wallInReach;
    private bool wallStuck;
    private int skillId;
    private bool teleport;
    private bool shoot;
    private bool testButton;
    #endregion
    #region Script refs
    private Animator anim;
    private Rigidbody rbody;
    private PlayerCommands comm;
    private PlayerLockon playerTarget;
    #endregion
    #region Obj refs
    [Header("Objects")]
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject AzaSword;
    [SerializeField] private GameObject defaultLockOnPoint;
    [SerializeField] private GameObject throwingPortal;
    [Header("Body Refs")]
    [SerializeField] private GameObject meshRef;
    [SerializeField] private GameObject body;
    [SerializeField] private GameObject hurtBox;
    [SerializeField]private GameObject interactionBox;
    //[SerializeField] private GameObject hitBox;
    [SerializeField] private GameObject dodgeBox;
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
    [Header("EquipmentSlots")]
    [SerializeField] private EquipmentSlot slot1;
    [SerializeField] private EquipmentSlot slot2;
    [Space]
    [Header("Materials")]
    private SkinnedMeshRenderer current;
    [SerializeField] private Material normal;
    [SerializeField] private Material transparent;
    [SerializeField] private Material shiny;
    #endregion
    #region Coroutines
    private Coroutine mpDrain;
    #endregion
    private Vector3 displacement;
    private bool skillButton;
    private bool teleportButton;
    private bool lockedOn;
    
    private bool cancelCamMovement;
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
    #endregion
    #region Getters and Setters
    public bool Moving { get => moving; set { moving = value; anim.SetBool("Moving", moving); } }
    public Vector3 Displacement { get => displacement; set => displacement = value; }
    public int CmdInput { get => cmdInput; set { cmdInput = value; anim.SetInteger("CmdInput", cmdInput); } }
    public bool Attacking { get => attacking; set { attacking = value; anim.SetBool("AttackStance", attacking); } }
    public Rigidbody Rbody { get => rbody; set => rbody = value; }
    public bool Jumping { get => jumping; set { jumping = value; anim.SetBool("Jump", jumping); } }
    public bool Grounded { get => grounded; set { grounded = value; anim.SetBool("Grounded", grounded); if (!value) { StartCoroutine(WaitToResetGround()); } } }
    public bool ReadyJump { get => readyJump; set { readyJump = value; anim.SetBool("ReadyJump", readyJump); } }
    public int SkillId { get => skillId; set { skillId = value; anim.SetInteger("SkillId", skillId); } }

    public bool TeleportButton { get => teleportButton; set { teleportButton = value; anim.SetBool("TeleportButton", teleportButton); } }

    public GameObject DefaultLockOnPoint { get => defaultLockOnPoint; set => defaultLockOnPoint = value; }
    public bool CancelCamMovement { get => cancelCamMovement; set => cancelCamMovement = value; }
    public bool LockedOn { get => lockedOn; set { lockedOn = value;CancelCamMovement = value; if (lockOn != null) { lockOn(value); } } }
    public bool SkillButton { get => skillButton; set { skillButton = value; SkillTrigger(); } }

    public int LStickX { get => lStickX; set { lStickX = value; anim.SetInteger("X", lStickX); } }
    public int LStickY { get => lStickY; set { lStickY = value; anim.SetInteger("Y", lStickY); } }

    public bool Teleport { get => teleport; set { teleport = value; anim.SetBool("Teleport", teleport); } }

    public bool Shoot { get => shoot; set { shoot = value;anim.SetBool("Shoot",shoot); } }

    public bool TestButton { get => testButton; set => testButton = value; }
    public GameObject Body { get => body; set => body = value; }
    #endregion
    public static Player GetPlayer() => instance;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        }
        else {
            instance = this;
        }
        anim = GetComponent<Animator>();
        Rbody = GetComponent<Rigidbody>();
        current = meshRef.GetComponent<SkinnedMeshRenderer>();
        comm = GetComponent<PlayerCommands>();
        playerTarget = GetComponent<PlayerLockon>();
    }
    void Start() {
        stats.SetStatsDefault();
        LegControlState.legLayer += LegLayerControl;
        GroundChecker.grounded += GroundCheck;
        WallChecker.stickToWall += WallCheck;
        Dash.bodyControl += BodyControl;
        //Cursor.lockState = CursorLockMode.Locked;
        ChargeJump.jump += Jumps;
        Jump.jumped += Jumped;
        SummonAzaSword.summonWeapon += SummonWeapon;
        // ThrowingPortal.sendSpot += TeleportHere;
        GameManager.spawnPlayer += TeleportHere;
        SceneDialogue.sealPlayerInput += SetInputSeal;
        GameManager.sealPlayer += SetInputSeal;
        ChainInput.sendChain += ChainControl;
        AirCombos.gravity += GravityControl;
        SlamState.gravity += GravityControl;
    }
    void Update() {
        if (!inputSeal) {
            GetInput();
        }
        Pause();
    }
    private IEnumerator SwitchMaterial() {

        while (isActiveAndEnabled) {
            yield return null;
            float lerp = Mathf.PingPong(Time.time, 0.2f) / 0.2f;
            GetComponentInChildren<SkinnedMeshRenderer>().material.Lerp(normal, shiny, lerp);
        }
    }
    private void GetInput() {
        if (Input.GetButtonDown("TestButton")) {
            TestButton = true;
        }
        if (cmdInput == 0 && !cancelCamMovement) {
            MovementControls();
        }

        //WallJumping();
        if (skillButton) {
            Skills();

        }
        else {
            if (!teleportButton) {
                JumpCharge();
            }
            Attack();
            ShootLight();
            Teleportation();
            Phase();
            Interact();
        }
        LockOn();
        SkillButtonControl();
    }
    #region Movement 
    private void MovementControls() {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Displacement = Vector3.Normalize(new Vector3(x, 0, y));
        Displacement = cam.transform.TransformDirection(displacement);
        displacement.y = 0;
        //Displacement = displacement;
        Move(x, y);
        LstickControlX(x);
        LstickControlY(y);
    }
    private void Move(float x, float y) {
        if (x != 0 || y != 0) {

            Moving = true;
            transform.rotation = Quaternion.LookRotation(displacement);
        }
        else {
            Moving = false;
        }
    }
    private void LstickControlX(float x) {
        LStickX = (int)x;
    }
    private void LstickControlY(float y) {
        LStickY = (int)y;
    }
    #endregion
    #region Inputs

    private void Pause() {
        if (Input.GetButtonDown("Pause")) {
            if (pause != null) {
                pause();
            }
        }
    }
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
    private void Attack() {//square and triangle
        if (Input.GetButtonDown("Fire1")) {
            if (!attacking) {
                Attacking = true;
                CmdInput = 0;
                return;
            }
        }
        if (attacking) {
            if (Input.GetButtonDown("Fire1")) {
                CmdInput = 1;
            }
            if (Input.GetButtonDown("Fire3")) {
                Debug.Log("triangle");
                CmdInput = 2;
            }
        }
        if (Input.GetButtonDown("Fire2")) {
            Attacking = false;
        }
    }
    private void WallJumping() {
        if (wallInReach) {

            if (Input.GetButtonDown("Fire1")) {
                rbody.useGravity = false;
                rbody.velocity = new Vector3(0, 0, 0);

                wallStuck = true;
            }
            if (Input.GetButton("Fire1")) {

            }
            if (Input.GetButtonUp("Fire1")) {
                Debug.Log("up");
                wallStuck = false;
                rbody.useGravity = true;
            }
        }
        if (wallStuck) {
            if (Input.GetButtonDown("Jump")) {
                wallStuck = false;

                //UnWall();
                //StartCoroutine(WaitToResetWallCheck());
                rbody.useGravity = true;
                wallInReach = false;
                Debug.Log("wall hop");
                rbody.AddForce(new Vector3(0, 15, 0), ForceMode.VelocityChange);
            }
        }
    }
    private void JumpCharge() {//X
        if (Input.GetButtonDown("Jump") && !jumping && grounded) {
            UnGround();
            Jumping = true;
            
            //Grounded = false;
            //StartCoroutine(WaitToResetGround());
        }

    }
     
    private void Teleportation() {//L2
        if (L2.GetButtonDown()) {
            TeleportButton = true;
        }
        if (L2.GetButtonUp()) {
            TeleportButton = false;
        }
        if (TeleportButton) {
            if (Input.GetButtonDown("Fire1")) {

            }
            if (Input.GetButtonDown("Fire2")) {

            }
            if (Input.GetButtonDown("Fire3")) {

            }
            if (Input.GetButtonDown("Jump")) {
                Teleport = true;
                stats.MpLeft--;
                Instantiate(teleportSparks,transform.position,teleportSparks.transform.rotation);
                //transform.position = displacement * 5 * Time.deltaTime;
            }
        }
    }
    private void Skills() {//R2
        if (Input.GetButtonDown("Fire1") && stats.MpLeft >= square.MpRequired) {
            SkillId = square.ID;
            stats.MpLeft -= square.MpRequired;
        }
        if (Input.GetButtonDown("Fire3") && stats.MpLeft >= triangle.MpRequired) {
            SkillId = triangle.ID;
            stats.MpLeft -= triangle.MpRequired;
            Debug.Log("Skill for triangle used");
        }
        if (Input.GetButtonDown("Fire2") && stats.MpLeft >= circle.MpRequired) {
            SkillId = circle.ID;
            stats.MpLeft -= circle.MpRequired;
        }
        if (Input.GetButtonDown("Jump") && stats.MpLeft >= x.MpRequired) {
            SkillId = x.ID;
            stats.MpLeft -= x.MpRequired;
        }
    }
    private void LockOn() {//R1
        if (Input.GetButtonDown("R1")) {
            LockedOn = true;
            cancelCamMovement = true;
        }
        if (Input.GetButtonUp("R1")) {
            LockedOn = false;
            cancelCamMovement = false;
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
    }
    private void PhaseUp() {
        mpDrain = StartCoroutine(MpDrain(1));
        current.material = transparent;
        UIAura.SetActive(true);
        hurtBox.SetActive(false);
        dodgeBox.SetActive(true);
        auraExplode.SetActive(true);
        Instantiate(leftDash, transform.position, Quaternion.identity);
        Instantiate(rightDash, transform.position, Quaternion.identity);
    }
    private void PhaseOff() {
        StopCoroutine(mpDrain);
        current.material = normal;
        UIAura.SetActive(false);
        hurtBox.SetActive(true);
        dodgeBox.SetActive(false);
    }
    #endregion
    #region Event Methods
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

        Jumping = true; Debug.Log("fuck yo jump");
        //StartCoroutine(WaitToStopJump());
    }
    private void GroundCheck(bool val) {
        Grounded = val;

    }
    private void WallCheck(bool val) {
        wallInReach = val;
    }
    private void LegLayerControl(int weight) {
        anim.SetLayerWeight(1, weight);
    }

    private void TeleportHere(Transform spot) {
        transform.position = spot.position + new Vector3(0, 1, 0);
    }
    private void ChainControl(int val) {
        comm.Chain = val;
    }
    private void GravityControl(bool val) {
        rbody.useGravity = val;
    }
    #endregion
    private IEnumerator MpDrain(int rate) {
        YieldInstruction wait = new WaitForSeconds(rate);
        while (isActiveAndEnabled) {
            yield return wait;
            if (stats.MpLeft == 0) {
                PhaseOff();
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
    private void Jumped(float val) {
        UnGround();
        Grounded = false;
        Rbody.AddForce(new Vector3(0, val, 0), ForceMode.Impulse);
        StartCoroutine(WaitToResetGround());
    }
    private void UnGround() {
        GroundChecker.grounded -= GroundCheck;
    }
    private void UnWall() {
        WallChecker.stickToWall -= WallCheck;
    }
    private void SummonWeapon(bool val) {
        //Instantiate(swordSpawn, transform.position, Quaternion.identity);
        AzaSword.SetActive(val);
    }
    private void SetInputSeal(bool val) {
        inputSeal = val;
    }
    private void BodyControl(bool val) {
        Body.SetActive(val);
        spawnIn.SetActive(val);
        if (val) {
            UIAura.SetActive(false);
            rbody.velocity = new Vector3(0, 0, 0);
            Instantiate(teleportSparks, transform.position, teleportSparks.transform.rotation);
        }
        else {
            Teleport = false;
            UIAura.SetActive(true);
           
        }

    }
    #region Cooldowns
    private IEnumerator WaitToTeleport() {
        YieldInstruction wait = new WaitForSeconds(4);
        yield return wait;
        //removeinputLock on teleport
    }
    #endregion
}
