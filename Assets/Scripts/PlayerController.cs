using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed;


    private Rigidbody myRigid;
    private CapsuleCollider capsuleCollider;
    private PlayerHP playerHP;
    private Item item;
    private WeaponManager weaponManager;
    private Weapon currentWeapon;
    private float fireDelay; // 총 재사격 가능 시간
    private bool isFireDelay;
    private bool isBorder;
    private bool isDamage;

    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        myRigid = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        playerHP = GetComponent<PlayerHP>();
        item = GetComponent<Item>();
        anim = GetComponent<Animator>();
        weaponManager = FindObjectOfType<WeaponManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Shoot();
    }

    private void Move()
    {
        if (!isDamage)
        {
            float _moveDirX = Input.GetAxisRaw("Horizontal");
            float _moveDirZ = Input.GetAxisRaw("Vertical");

            Vector3 moveVec = new Vector3(_moveDirX, 0, _moveDirZ).normalized;

            if (!isBorder)
            {
                myRigid.MovePosition(transform.position + moveVec * Time.deltaTime * walkSpeed);
            }
            anim.SetBool("isWalk", moveVec != Vector3.zero);
            transform.LookAt(transform.position + moveVec);

        }

    }

    private void Shoot()
    {
        fireDelay += Time.deltaTime;
        currentWeapon = weaponManager.weapons[weaponManager.currentWeaponIndex].GetComponent<Weapon>();
        isFireDelay = fireDelay > currentWeapon.GetComponent<Weapon>().weaponRPM;
        if (Input.GetKey(KeyCode.Space) && isFireDelay)
        {
            anim.SetTrigger("Shoot");
            currentWeapon.Shoot();
            fireDelay = 0;
        }
        
    }

    private void FreezeRotation()
    {
        myRigid.angularVelocity = Vector3.zero;
    }
    private void WallCheck()
    {
        isBorder = Physics.Raycast(transform.position, transform.forward, 1.5f, LayerMask.GetMask("Wall"));
    }
    private void FixedUpdate()
    {
        FreezeRotation();
        WallCheck();
    }

    private void OnTriggerEnter(Collider other)
    {
        
        // enemyBullet 스크립트로 빼줄거임
        if (other.CompareTag("EnemyBullet"))
        {
            if (!isDamage)
            {
                playerHP.CurrentHP -= other.GetComponent<EnemyBullet>().bulletDamage;
                Vector3 reactVec = new Vector3(other.transform.position.x - transform.position.x, 0, other.transform.position.z - transform.position.z);
                StartCoroutine(OnDamage(reactVec));
                Destroy(other.gameObject);

            }
        }
    }

    IEnumerator OnDamage(Vector3 reactVec)
    {
        isDamage = true;
        myRigid.AddForce(reactVec * 10, ForceMode.Impulse);
        yield return new WaitForSeconds(1f);
        isDamage = false;
        
        yield return null;
    }
}
