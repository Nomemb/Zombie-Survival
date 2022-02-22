using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    [SerializeField]
    private ZombieData zombieData;
    private Rigidbody rigidbody;
    private BoxCollider boxCollider;
    public BoxCollider meleeArea;
    public GameObject bulletPrefabs;

    private ScoreManager scoreManager;

    [SerializeField]
    private Transform target; // 쫓을 타겟 ( 플레이어 )

    private Item item;
    
    private NavMeshAgent nav;
    public int zombieHP;
    public int zombieDamage;
    public float zombieAttackSpeed;
    public string zombieName;
    public int zombiePoint;
    private Animator anim;


    private bool isChase;
    private bool isAttack;
    private bool isDie;

    private Renderer[] renderer;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        item = FindObjectOfType<Item>();
        scoreManager = GetComponentInParent<ScoreManager>();

        renderer = GetComponentsInChildren<Renderer>();
        zombieHP = zombieData.ZombieHP;
        zombieDamage = zombieData.ZombieDamage;
        zombieAttackSpeed = zombieData.ZombieAttackSpeed;
        zombieName = zombieData.ZombieName;



        if (zombieName == "Boss Zombie")
        {
            zombiePoint = 1000;
            foreach (Renderer mesh in renderer)
            {
                mesh.material.color = Color.red;
            }            
        }
        else
        {
            zombiePoint = 100;
        }
        Invoke("ChaseStart", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isChase || !isDie)
        {
            nav.SetDestination(target.position);
            anim.SetBool("isChase", isChase);

        }
    }
    private void ChaseStart()
    {
        isChase = true;
    }

    private void Targeting()
    {
        float targetRadius = 0f;
        float targetRange = 0f;

        if(zombieName == "Normal Zombie")
        {
            targetRadius = 1f;
            targetRange = 0.3f;
        }
        else if(zombieName == "Boss Zombie")
        {
            targetRadius = 0.5f;
            targetRange = 5f;
        }


        Debug.DrawRay(transform.position, transform.forward * targetRange, Color.green);
        RaycastHit[] rayHits = Physics.SphereCastAll(transform.position, targetRadius, transform.forward, targetRange, LayerMask.GetMask("Player"));
        if(rayHits.Length > 0 && !isAttack)
        {
            StartCoroutine(AttackCoroutine());
        }
    }

    IEnumerator AttackCoroutine()
    {
        isChase = false;
        isAttack = true;
        nav.isStopped = true;
        anim.SetBool("isAttack", isAttack);


        if (zombieName == "Normal Zombie")
        {
            yield return new WaitForSeconds(0.5f);
            meleeArea.enabled = true;


            yield return new WaitForSeconds(zombieAttackSpeed);
            meleeArea.enabled = false;

        }
        else if (zombieName == "Boss Zombie")
        {
            yield return new WaitForSeconds(0.5f);
            GameObject instantBullet = Instantiate(bulletPrefabs, transform.position, transform.rotation);
            instantBullet.GetComponent<EnemyBullet>().bulletDamage = zombieDamage;
            Rigidbody rigidBullet = instantBullet.GetComponent<Rigidbody>();
            // 플레이어 방향으로 던지게 수정할 예정
            Vector3 velo = new Vector3(target.position.x - transform.position.x, 0, target.position.z - transform.position.z);

            rigidBullet.velocity = velo.normalized * 5;

            yield return new WaitForSeconds(zombieAttackSpeed);
        }


        isChase = true;
        isAttack = false;
        nav.isStopped = false;
        anim.SetBool("isAttack", isAttack);
    }
    private void FixedUpdate()
    {
        if (!isDie)
        {
            FreezeVelocity();
            FreezeRotation();
            Targeting();
        }

    }
    private void FreezeVelocity()
    {
        if (isChase)
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;

        }
    }

    private void FreezeRotation()
    {
        rigidbody.angularVelocity = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("playerBullet") || other.CompareTag("BossBullet"))
        {
            Vector3 reactVec = Vector3.zero;
            if (other.CompareTag("playerBullet"))
            {
                Bullet bullet = other.GetComponent<Bullet>();
                zombieHP -= bullet.bulletDamage;
                reactVec = new Vector3(other.transform.position.x - transform.position.x, 0, other.transform.position.z - transform.position.z);
                // sniper총은 관통탄으로 처리
                if (other.GetComponent<Bullet>().weaponName != "Sniper")
                {
                    Destroy(other.gameObject);
                }
            }
            else
            {
                EnemyBullet bullet = other.GetComponent<EnemyBullet>();
                zombieHP -= (bullet.bulletDamage / 2);
                reactVec = new Vector3(other.transform.position.x - transform.position.x, 0, other.transform.position.z - transform.position.z);
                Destroy(other.gameObject);

            }


            StartCoroutine(OnDamage(reactVec));

        }
    }

    IEnumerator OnDamage(Vector3 reactVec)
    {
        nav.isStopped = true;
        StopCoroutine(AttackCoroutine());
        isChase = true;
        isAttack = false;
        anim.SetBool("isAttack", isAttack);

        if (zombieHP > 0)
        {
            reactVec = reactVec.normalized;
            rigidbody.AddForce(reactVec * 10, ForceMode.Impulse);

            // 잠깐 경직 후 추적 재개
            yield return new WaitForSeconds(0.3f);
            nav.isStopped = false;

            yield return null;
        }
        else
        {
            // 사망처리
            isDie = true;
            nav.isStopped = true;

            anim.SetTrigger("death");
            rigidbody.useGravity = false;
            boxCollider.enabled = false;
            // 스코어 증가            
            scoreManager.IncreaseScore(zombiePoint);
            // 아이템드롭
            item.DropItem(transform.position, zombieData.ZombieDropRate);
            yield return new WaitForSeconds(2.5f);           

            Destroy(gameObject);
        }

        yield return null;
    }
}
