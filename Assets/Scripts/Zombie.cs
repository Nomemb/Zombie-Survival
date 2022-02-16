    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    [SerializeField]
    private ZombieData zombieData;
    private Rigidbody rigidbody;
    private CapsuleCollider capsuleCollider;

    public int zombieHP;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        anim = GetComponent<Animator>();
        zombieHP = zombieData.ZombieHP;
    }

    // Update is called once per frame
    void Update()
    {
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("playerBullet"))
        {
            Bullet bullet = other.GetComponent<Bullet>();
            zombieHP -= bullet.bulletDamage;
            // 넉백
            Vector3 reactVec = new Vector3(other.transform.position.x - transform.position.x, 0, other.transform.position.z - transform.position.z);
            // sniper총은 관통탄으로 처리
            if(other.GetComponent<Bullet>().currentGunInfo.GetComponent<Weapon>().weaponName != "Sniper")
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
            rigidbody.AddForce(reactVec * 5, ForceMode.Impulse);
        }
        else
        {
            anim.SetTrigger("death");
            rigidbody.useGravity = false;
            capsuleCollider.enabled = false;
            yield return new WaitForSeconds(2.5f);
            Destroy(gameObject);
        }

        yield return null;
    }
}
