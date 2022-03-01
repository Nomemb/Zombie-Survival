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
    private StageManager stage;
    public AudioSource audio;
    private Weapon currentWeapon;
    private float fireDelay; // 총 재사격 가능 시간
    private bool isFireDelay;
    private bool isBorder;
    public bool isDamage;
    public bool isDie;

    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        myRigid = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        playerHP = GetComponent<PlayerHP>();
        item = GetComponent<Item>();
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        audio.volume = 0.5f;

        weaponManager = FindObjectOfType<WeaponManager>();
        stage = FindObjectOfType<StageManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDie)
        {
            Move();
            Shoot();
        }
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
            audio.clip = currentWeapon.gunSound;
            audio.Play();
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
        if (other.CompareTag("EnemyBullet") || other.CompareTag("BossBullet"))
        {
            if (!isDamage)
            {
                playerHP.CurrentHP -= other.GetComponent<EnemyBullet>().bulletDamage;
                Vector3 reactVec = new Vector3(other.transform.position.x - transform.position.x, 0, other.transform.position.z - transform.position.z);

                StartCoroutine(OnDamage(reactVec));
            }
        }
        if (other.CompareTag("BossBullet"))
        {
            if (other.GetComponent<Rigidbody>() != null)
            {
                Destroy(other.gameObject);
            }
        }
    }

    IEnumerator OnDamage(Vector3 reactVec)
    {
        if (playerHP.CurrentHP > 0)
        {
            isDamage = true;

            myRigid.AddForce(reactVec * 10, ForceMode.Impulse);
            yield return new WaitForSeconds(0.5f);

            isDamage = false;
        }
        else
        {
            StartCoroutine(OnDie());
        }
        yield return null;
    }

    IEnumerator OnDie()
    {
        isDie = true;
        anim.SetTrigger("Die");
        myRigid.useGravity = false;
        capsuleCollider.enabled = false;
        yield return new WaitForSeconds(2.5f);
        // 퍼즈
        stage.GameOver();
    }
}
