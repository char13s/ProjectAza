using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649
public class PlayerLockon : MonoBehaviour {
    [Header("Obj refs")]
    [SerializeField] private GameObject leftPoint;
    [SerializeField] private GameObject aimPoint;
    [Space]
    [SerializeField] private float moveSpeed;
    private Player player;
    private List<EnemyBaseScript> enemies = new List<EnemyBaseScript>(16);
    private EnemyBaseScript closestEnemy;
    private float rotationSpeed;
    private bool rotLock;
    private int t;
    public float RotationSpeed { get => rotationSpeed; set { rotationSpeed = value; Mathf.Clamp(value, 5, 8); } }

    public int T { get => t; set => t = value; }

    private void Awake() {
        player = GetComponent<Player>();
    }
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        UpdateEnemyList();
        if (player.LockedOn) {
            if (enemies.Count == 0 && player.CmdInput == 0) {
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

            int index = enemies.IndexOf(current);
            if (shouldBeInList != index >= 0) {
                if (shouldBeInList) {
                    enemies.Add(current);
                    if (enemies.Count > 1) {
                        GetClosestEnemy();
                    }
                }
                else { enemies.RemoveAt(index); }
            }
        }
    }
    private void GetInput() {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (enemies.Count != 0 && T < enemies.Count) {

            LockOn(x, y, enemies[T]);
        }
        MovementInputs(x, y);
    }
    private void EnemyLockedTo() { }
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
            //if (!player.Dashable) {
                transform.RotateAround(target.transform.position, target.transform.up, -x * rotationSpeed * moveSpeed * Time.deltaTime);
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * y * Time.deltaTime);
            //}
        }
        if (enemies[T].Dead) {
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
        transform.position = Vector3.MoveTowards(transform.position, leftPoint.transform.position, moveSpeed * -x * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, aimPoint.transform.position, moveSpeed * y * Time.deltaTime);
        MovementInputs(x, y);
    }

    private void GetClosestEnemy() {
        if (T < enemies.Count) {
            float enDist = EnDist(enemies[T].gameObject);

            foreach (EnemyBaseScript en in enemies) {
                closestEnemy = en;
                if (EnDist(en.gameObject) < enDist) {
                    T = enemies.IndexOf(en);
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
