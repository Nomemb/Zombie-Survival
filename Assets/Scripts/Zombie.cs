using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    [SerializeField]
    private ZombieData zombieData;
    private Rigidbody rigidbody;
    private CapsuleCollider capsuleCollider;

    [SerializeField]
    private Transform target; // 쫓을 타겟 ( 플레이어 )

    private Item item;
    
    private NavMeshAgent nav;
    public int zombieHP;
    private Animator anim;
    private bool isChase;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        item = FindObjectOfType<Item>();

        zombieHP = zombieData.ZombieHP;
        Invoke("ChaseStart", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isChase)
        {
            nav.SetDestination(target.position);
            anim.SetBool("isChase", isChase);

        }
    }
    private void ChaseStart()
    {
        isChase = true;
    }
    private void FixedUpdate()
    {
        FreezeVelocity();
    }
    private void FreezeVelocity()
    {
        if (isChase)
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("playerBullet"))
        {
            Bullet bullet = other.GetComponent<Bullet>();
            zombieHP -= bullet.bulletDamage;
            // 넉백
            nav.isStopped = true;
            Vector3 reactVec = new Vector3(other.transform.position.x - transform.position.x, 0, other.transform.position.z - transform.position.z);


            // sniper총은 관통탄으로 처리
            if (other.GetComponent<Bullet>().currentGunInfo.GetComponent<Weapon>().weaponName != "Sniper")
            {
                Destroy(other.gameObject);
            }
            StartCoroutine(OnDamage(reactVec));

        }
    }

    IEnumerator OnDamage(Vector3 reactVec)
    {
        if (zombieHP > 0)
        {
            reactVec = reactVec.normalized;
            rigidbody.AddForce(reactVec * 10, ForceMode.Impulse);

            // 잠깐 경직 후 추적 재개
            yield return new WaitForSeconds(0.1f);
            nav.isStopped = false;

            yield return null;
        }
        else
        {
            // 사망처리
            nav.isStopped = true;
            anim.SetTrigger("death");
            rigidbody.useGravity = false;
            capsuleCollider.enabled = false;
            yield return new WaitForSeconds(2.5f);
            // 아이템드롭
            item.DropItem(transform.position, zombieData.ZombieDropRate);
            Destroy(gameObject);
        }

        yield return null;
    }
}
