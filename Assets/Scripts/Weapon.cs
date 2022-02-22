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
        // 권총은 탄알이 무제한이기 때문에 예외처리함
        if (weaponName != "Pistol")
            currentBullet -= 1;

        if (weaponName != "Shotgun")
        {
            GameObject instantBullet = Instantiate(bulletPrefabs, bulletPos.position, playerController.transform.rotation);
            Rigidbody bulletRigid = instantBullet.GetComponent<Rigidbody>();
            // bulletPrefabs.GetComponent<Bullet>().Shoot(bulletPos.forward);
            bulletRigid.velocity = bulletPos.forward * 50;
            yield return new WaitForSeconds(weaponRange);
            Destroy(instantBullet);

        }
        else
        {
            GameObject[] instantBullets = new GameObject[3];
            Rigidbody[] bulletRigids = new Rigidbody[3];
            for (int i = 0; i < instantBullets.Length; i++)
            {
                instantBullets[i] = Instantiate(bulletPrefabs, bulletPos.position, playerController.transform.rotation);
                bulletRigids[i] = instantBullets[i].GetComponent<Rigidbody>();
                Vector3 bulletDir = BoundaryAngle(10 * (i-1), bulletPos);
                bulletRigids[i].velocity = bulletDir * 50;

            }
            
            yield return new WaitForSeconds(weaponRange);
            for (int i = 0; i < instantBullets.Length; i++)
            {
                Destroy(instantBullets[i]);
            }

        }
        yield return null;
    }

    private void OutOfAmmo()
    {
        Debug.Log(weaponName + " is Out of Ammo!");
        weaponManager.OutOfAmmo();

    }

    private Vector3 BoundaryAngle(float _angle, Transform _object)
    {
        _angle += _object.eulerAngles.y;
        return new Vector3(Mathf.Sin(_angle * Mathf.Deg2Rad), 0f, Mathf.Cos(_angle * Mathf.Deg2Rad));
    }
}
