using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string weaponName;
    public int weaponDamage;
    public float weaponRPM; // 연사속도
    public float weaponRange; // 총알이 살아있는 시간( 사거리 )

    public int maxBullet; // 최대 장탄수
    public int currentBullet;

    public Transform bulletPos;
    public GameObject bulletPrefabs;

    private PlayerController playerController;
    private WeaponManager weaponManager;
    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        weaponManager = GetComponentInParent<WeaponManager>();

    }
    public void Shoot()
    {
        if (currentBullet > 0)
        {
            StartCoroutine(ShootCoroutine());
        }
        else
        {
            OutOfAmmo();
        }
    }

    IEnumerator ShootCoroutine()
    {
        GameObject instantBullet = Instantiate(bulletPrefabs, bulletPos.position, playerController.transform.rotation);
        Rigidbody bulletRigid = instantBullet.GetComponent<Rigidbody>();
        // 권총은 탄알이 무제한이기 때문에 예외처리함
        if(weaponName != "Pistol")
            currentBullet -= 1;

        bulletRigid.velocity = bulletPos.forward * 50;
        yield return new WaitForSeconds(weaponRange);
        Destroy(instantBullet);
        yield return null;
    }

    private void OutOfAmmo()
    {
        Debug.Log(weaponName + " is Out of Ammo!");
        weaponManager.OutOfAmmo();

    }
}
