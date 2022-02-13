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
    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }
    public void Shoot()
    {
        StartCoroutine(ShootCoroutine());
    }

    IEnumerator ShootCoroutine()
    {
        float bulletSurvivalTime = 0;
        GameObject instantBullet = Instantiate(bulletPrefabs, bulletPos.position, playerController.transform.rotation);
        Rigidbody bulletRigid = instantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = bulletPos.forward * 50;
        bulletSurvivalTime += Time.deltaTime;

        if (bulletSurvivalTime > weaponRange)
        {
            Destroy(instantBullet);
        }
        yield return null;
    }

    
}
