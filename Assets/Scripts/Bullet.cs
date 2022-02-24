using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public string weaponName;
    public int bulletDamage;

    private WeaponManager weaponManager;
    private Rigidbody rigid;
    private Vector3 direction;


    private void Start()
    {
        weaponManager = FindObjectOfType<WeaponManager>();
        rigid = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        bulletDamage = weaponManager.weapons[weaponManager.currentWeaponIndex].GetComponent<Weapon>().weaponDamage;
        weaponName = weaponManager.weapons[weaponManager.currentWeaponIndex].GetComponent<Weapon>().weaponName;
        // rigid.velocity = direction * 50 * Time.deltaTime;
        
    }
    public void Shoot(Vector3 _dir)
    {
        direction = _dir;

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
