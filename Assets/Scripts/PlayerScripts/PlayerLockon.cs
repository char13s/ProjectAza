using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Cinemachine;
#pragma warning disable 0649
public class PlayerLockon : MonoBehaviour {
    [Header("Obj refs")]
    [SerializeField] private GameObject leftPoint;
    [SerializeField] private GameObject aimPoint;
    //[SerializeField] private CinemachineVirtualCamera lockOnCam;
    [Space]
    [SerializeField] private float moveSpeed;
    private Player player;
    private List<EnemyBaseScript> enemies = new List<EnemyBaseScript>(16);
    private EnemyBaseScript closestEnemy;
    private static PlayerLockon instance;
    private float rotationSpeed;
    private bool rotLock;
    private int t;
    public float RotationSpeed { get => rotationSpeed; set { rotationSpeed = value; Mathf.Clamp(value, 5, 8); } }

    public int T { get => t; set => t = value; }

    public static UnityAction<bool> enemyDetected;
    public List<EnemyBaseScript> Enemies { get => enemies; set => enemies = value; }

    public static PlayerLockon GetLockon() => instance;
    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        }
        else {
            instance = this;
        }
        player = GetComponent<Player>();
    }
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        UpdateEnemyList();
        EnemyFound();
        if (player.LockedOn) {
            if (Enemies.Count == 0 && player.CmdInput == 0) {
                BasicMovement();
            }
            else {
        
                GetInput();
            }
        }
    }
    private void UpdateEnemyList() {
        Vector3 position = transform.position;
        for (int i = 0; i < EnemyBaseScript.TotalCount; i++) {
            EnemyBaseScript current = EnemyBaseScript.GetEnemy(i);
            bool shouldBeInList = false;
            if (current != null) { shouldBeInList = Vector3.SqrMagnitude(current.transform.position - position) <= 361; }

            int index = Enemies.IndexOf(current);
            if (shouldBeInList != index >= 0) {
                if (shouldBeInList) {
                    Enemies.Add(current);
                    if (Enemies.Count > 1) {
                        GetClosestEnemy();
                    }
                }
                else { Enemies.RemoveAt(index); }
            }
        }
    }
    private void GetInput() {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (Enemies.Count != 0 && T < Enemies.Count) {

            LockOn(x, y, Enemies[T]);
        }
        MovementInputs(x, y);
    }
    public EnemyBaseScript EnemyLockedTo() { return Enemies[T]; }
    private void LockOn(float x, float y, EnemyBaseScript target) {
        EnemyLockedTo();
        target.Targeted = true;
        if (target != null) {
            RotationSpeed = 18 - EnDist(target.gameObject);
            Vector3 delta = target.transform.position - player.transform.position;
            delta.y = 0;
            if (!rotLock) {
                transform.rotation = Quaternion.LookRotation(delta, Vector3.up);
            }
            //transform.LookAt(Enemies[T].transform.position,Vector3.up);
            if (player.CmdInput==0) {
              transform.RotateAround(target.transform.position, target.transform.up, -x * rotationSpeed * moveSpeed * Time.deltaTime);
              transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * y * Time.deltaTime);
            }
        }
        if (Enemies[T].Dead) {
            GetClosestEnemy();
        }
    }
    private void BasicMovement() {

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        RotationSpeed = 3 - EnDist(aimPoint);
        Vector3 delta = aimPoint.transform.position - player.transform.position;
        delta.y = 0;
        if (!rotLock) {
            transform.rotation = Quaternion.LookRotation(delta, Vector3.up);

        }
        if (player.CmdInput == 0) {
            transform.position = Vector3.MoveTowards(transform.position, leftPoint.transform.position, moveSpeed * -x * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, aimPoint.transform.position, moveSpeed * y * Time.deltaTime);
            MovementInputs(x, y);
        }
    }
    private void EnemyFound() {
        if (enemies.Count > 0) {
            if (enemyDetected != null) {
                enemyDetected(true);
            }
        }
        else {
            if (enemyDetected != null) {
                enemyDetected(false);
            }
        }
    }
    private void GetClosestEnemy() {
        if (T < Enemies.Count) {
            float enDist = EnDist(Enemies[T].gameObject);

            foreach (EnemyBaseScript en in Enemies) {
                closestEnemy = en;
                if (EnDist(en.gameObject) < enDist) {
                    T = Enemies.IndexOf(en);
                    player.DefaultLockOnPoint.transform.position = Enemies[T].transform.position;
                    player.DefaultLockOnPoint.transform.SetParent(Enemies[T].transform);
                }
            }
        }
    }
    private float EnDist(GameObject target) => Vector3.Distance(target.transform.position, player.transform.position);
    private void SetLock() {
        rotLock = true;
    }
    private void ResetLock() {
        rotLock = false;
    }
    private void MovementInputs(float x, float y) {
        //if (x == 0) {
        //    if (y > 0)//forward
        //    {
        //        player.Direction = 0;
        //
        //    }
        //
        //    if (y < 0)//back
        //    {
        //
        //        player.Direction = 2;
        //    }
        //}
        //
        //if (x > 0.3)//right
        //{
        //    player.Direction = 3;
        //
        //}
        //
        //if (x < -0.3)//left
        //{
        //    player.Direction = 1;
        //
        //}
        if (Mathf.Abs(x) >= 0.001 || Mathf.Abs(y) >= 0.001) {

            player.Moving = true;
        }
        else {

            player.Moving = false;
        }
    }
}
