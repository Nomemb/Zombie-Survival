using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public string weaponName;
    public int bulletDamage;

    private WeaponManager weaponManager;

    private void Start()
    {
        weaponManager = FindObjectOfType<WeaponManager>();
    }
    private void Update()
    {
        bulletDamage = weaponManager.weapons[weaponManager.currentWeaponIndex].GetComponent<Weapon>().weaponDamage;
        weaponName = weaponManager.weapons[weaponManager.currentWeaponIndex].GetComponent<Weapon>().weaponName;        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
