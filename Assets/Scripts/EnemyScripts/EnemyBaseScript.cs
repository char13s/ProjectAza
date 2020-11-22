using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;
#pragma warning disable 0649
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyBaseScript : MonoBehaviour {

    public enum EnemyState { Idle, Chasing, Attacking, Hurt, AttackPoistioned, Null, Stunned, UniqueState,Dead }
    private enum Tiers { Lowest, Low, Mid, High, Boss }
    private EnemyState state;
    private static List<EnemyBaseScript> enemies = new List<EnemyBaseScript>(32);

    #region Outside scripts
    private Animator anim;
    private SkinnedMeshRenderer mesh;
    private Player player;
    private NavMeshAgent nav;
    #endregion

    #region Animation Parameters
    private bool hit;
    private bool dead;
    private bool idle;
    private bool attacking;
    private bool walk;
    private bool targeted;
    #endregion
    #region Object References
    [SerializeField] private EnemyHitBox hitbox;
    [SerializeField] private GameObject arrow;

    #endregion
    #region Stats
    [Header("Stats")]
    [SerializeField] private Tiers tier;
    [SerializeField] private int level;
    [SerializeField] private int attackDelay;
    [SerializeField] private int baseExpYield;
    [SerializeField] private int baseHealth;
    [SerializeField] private float battlePower;

    [SerializeField] private int health;
    private float healthLeft;
    [SerializeField] private float speed;
    [SerializeField] private float attackRange;

    #endregion
    private bool canAttack;

    private float Distance => Vector3.Distance(player.transform.position, transform.position);

    public static event UnityAction<int> sendBp;
        #region Getters and Setters
    public bool Hit { get => hit; set { hit = value; anim.SetBool("Hurt", hit); OnHit(); } }
    public int Health { get => health; set { health = value;  } }
    public EnemyState State { get => state; set { state = value; StateControl(); } }

    public static int TotalCount => enemies.Count;

    public bool Dead { get => dead; set { dead = value; anim.SetBool("Dead", dead); Death(); } }

    public bool Attacking { get => attacking; set { attacking = value; anim.SetBool("Attack", attacking); } }

    public bool Walk { get => walk; set { walk = value; anim.SetBool("Walk", walk); } }

    public bool Targeted { get => targeted; set { targeted = value; IsTargeted(); } }

    public float HealthLeft { get => healthLeft; set { healthLeft = Mathf.Clamp(value, 0, 99999999); if (healthLeft == 0) { Dead = true; } } }
    #endregion
    public virtual void Awake() {
        State = EnemyState.Null;
        anim = GetComponent<Animator>();
        mesh = GetComponent<SkinnedMeshRenderer>();
        
    }
    public virtual void OnEnable() {
        //EnemyHitboxBehavior[] behaviours = anim.GetBehaviours<EnemyHitboxBehavior>();
        //for (int i = 0; i < behaviours.Length; i++)
        //    behaviours[i].HitBox = hitbox;

    }
    public virtual void Start() {
        player = Player.GetPlayer();
        enemies.Add(this);
        //StatControl();
    }

    // Update is called once per frame
    public virtual void Update() {
        if (state != EnemyState.Hurt && state != EnemyState.Null) {
            //StateLogic();
        }
    }

    public static EnemyBaseScript GetEnemy(int i) {
        return enemies[i];
    }

    private void StatControl() {
        HealthLeft = health;
    }
    public virtual void StateLogic() {
        switch (state) {
            case EnemyState.Idle:
                if (Distance > attackRange && Distance < 5f) {//is in chasing range
                    State = EnemyState.Chasing;
                }
                else if (Distance < attackRange) {//is in attacking range, will change to match attack position conditions
                    State = EnemyState.Attacking;
                }
                break;
            case EnemyState.Chasing:
                if (Distance < attackRange) {//is in attacking range, will change to match attack position conditions
                    State = EnemyState.Attacking;
                }
                else if (Distance > 5f) {
                    State = EnemyState.Idle;
                }
                else {
                    State = EnemyState.Chasing;
                }
                break;
            case EnemyState.Attacking:
                if (Distance > attackRange && Distance < 5f) {//is in chasing range
                    State = EnemyState.Chasing;
                }
                else if (Distance > 5f) {
                    State = EnemyState.Idle;
                }
                else {
                    State = EnemyState.Attacking;
                }
                break;
            case EnemyState.AttackPoistioned:
                break;
            default:
                break;
        }
    }
    private void StateControl() {
        switch (state) {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Chasing:
                Chasing();
                break;
            case EnemyState.Attacking:
                Attack();
                break;
            case EnemyState.Hurt:
                StartCoroutine(UnSetHurt());
                break;
            case EnemyState.AttackPoistioned:
                break;
            case EnemyState.Null:
                StartCoroutine(UndoNull());
                break;
            case EnemyState.Stunned:
                StartCoroutine(UndoStun());
                break;
            case EnemyState.UniqueState:
                UniqueState();
                break;
            case EnemyState.Dead:
                break;
        }
    }
    #region State Logic
    public virtual void Idle() {
        Walk = false;
    }
    public virtual void Attack() {
        Walk = false;
        Attacking = true;
        Vector3 delta = (transform.position - player.transform.position);
        delta.y = 0;
        transform.rotation = Quaternion.LookRotation(delta);
        StartCoroutine(UnSetAttack());
    }
    private void Chasing() {
        Walk = true;
        Vector3 delta = (transform.position - player.transform.position);
        delta.y = 0;
        transform.rotation = Quaternion.LookRotation(delta);
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }
    private void OnHit() {
        Walk = false;
        //Debug.Log("Hit!" +
        //    "Health: " + HealthLeft);
        State = EnemyState.Hurt;
        CalculateDamage(0);
    }
    private void Death() {
        
        if (sendBp != null) {
            sendBp((int)BPRanges());
        };
        BPRanges();
        //activate dissolve shader then puff of smoke
        Destroy(gameObject, 4f);
    }
    public virtual void UniqueState() {
        //The basic enemy type doesnt use this, this is a stand in method to be overriden and filled in in child scripts
    }
    #endregion
    #region UNSetters
    private IEnumerator AttackDelay() {//i dont use this.....yet?
        YieldInstruction wait = new WaitForSeconds(3f);
        yield return wait;
    }
    private IEnumerator UnSetHurt() {
        YieldInstruction wait = new WaitForSeconds(1.5f);
        yield return wait;
        Hit = false;
        State = EnemyState.Idle;
    }
    private IEnumerator UndoNull() {
        YieldInstruction wait = new WaitForSeconds(4f);
        yield return wait;
        State = EnemyState.Idle;
    }
    private IEnumerator UndoStun() {
        YieldInstruction wait = new WaitForSeconds(8f);
        yield return wait;
        State = EnemyState.Idle;
    }
    private IEnumerator UnSetAttack() {
        yield return null;
        Attacking = false;
        State = EnemyState.Null;
    }
    #endregion 
    private void IsTargeted() {
        if (arrow != null) {
            if (targeted) {
                arrow.SetActive(true);
            }
            else {
                arrow.SetActive(false);
            }
        }
    }
    private void HitBox(int val) {
        if (val == 1) {
            hitbox.gameObject.SetActive(true);
        }
        else {
            hitbox.gameObject.SetActive(false);
        }

    }
    public void CalculateDamage(float addition) {
        if (!dead) {
            HealthLeft -= Mathf.Clamp((player.stats.BattlePressure / battlePower), 0, 999);
            Debug.Log(Mathf.Clamp((player.stats.BattlePressure / battlePower), 0, 999));
            //Hit = true;
            if (HealthLeft <= Health / 4) {

                //StartCoroutine(StateControlCoroutine());

            }
            //OnHit();
        }

    }//(Mathf.Max(1, (int)(Mathf.Pow(stats.Attack - 2.6f * pc.stats.Defense, 1.4f) / 30 + 3))) / n; }
    public void CalculateAttack() {

        player.stats.HealthLeft -= Mathf.Max(1, battlePower / player.stats.BattlePressure);

    }
    private float BPRanges() {

        switch (tier) {
            case Tiers.Lowest:

                return Mathf.Clamp((player.stats.BattlePressure * 0.15f), 1, 9999999) + player.stats.BattlePressure;
            case Tiers.Low:
                return Mathf.Clamp((player.stats.BattlePressure * 0.20f), 1, 9999999) + player.stats.BattlePressure;

            case Tiers.Mid:
                return Mathf.Clamp((player.stats.BattlePressure * 0.35f), 1, 9999999) + player.stats.BattlePressure;

            case Tiers.High:
                return Mathf.Clamp((player.stats.BattlePressure * 0.5f), 1, 9999999) + player.stats.BattlePressure;

            case Tiers.Boss:
                Debug.Log(player.stats.BattlePressure * 0.75f);
                return Mathf.Clamp((player.stats.BattlePressure * 0.75f), 1, 9999999);
        }
        return 0;

    }
}