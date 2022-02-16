using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int bulletDamage;

    private WeaponManager weaponManager;
    public GameObject currentGunInfo;
    private void Start()
    {
        weaponManager = FindObjectOfType<WeaponManager>();
    }
    private void Update()
    {
        currentGunInfo = weaponManager.weapons[weaponManager.currentWeaponIndex];
        bulletDamage = currentGunInfo.GetComponent<Weapon>().weaponDamage;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
