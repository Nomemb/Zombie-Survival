using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed;


    private Rigidbody myRigid;
    private Item item;
    private WeaponManager weaponManager;
    private Weapon currentWeapon;
    private float fireDelay; // 총 재사격 가능 시간
    private bool isFireDelay; 
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        myRigid = GetComponent<Rigidbody>();
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
        float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirZ = Input.GetAxisRaw("Vertical");

        Vector3 moveVec = new Vector3(_moveDirX, 0, _moveDirZ).normalized;

        
        myRigid.MovePosition(transform.position + moveVec * Time.deltaTime * walkSpeed);
        anim.SetBool("isWalk", moveVec != Vector3.zero);
        transform.LookAt(transform.position + moveVec);

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
    private void FixedUpdate()
    {
        FreezeRotation();
    }
}
