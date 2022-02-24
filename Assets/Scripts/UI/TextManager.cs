using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    [SerializeField]
    private Text textPrefab;

    private GameObject player;
    private Weapon currentWeapon;
    private WeaponManager weaponManager;
    private string currentWeaponName;
    private int currentWeaponIndex;

    private Text currentWeaponInfo;


    Camera cam = null;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player");
        weaponManager = FindObjectOfType<WeaponManager>();
        currentWeapon = weaponManager.weapons[weaponManager.currentWeaponIndex].GetComponent<Weapon>();
        currentWeaponInfo = Instantiate(textPrefab, player.transform.position, Quaternion.identity, transform);
        currentWeaponInfo.text = string.Format("{0}", currentWeapon.weaponName);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        currentWeapon = weaponManager.weapons[weaponManager.currentWeaponIndex].GetComponent<Weapon>();
        if(currentWeapon.weaponName == "Pistol")
        {
            currentWeaponInfo.text = string.Format("{0}", currentWeapon.weaponName);
            currentWeaponInfo.transform.position = cam.WorldToScreenPoint(player.transform.position + new Vector3(0, 1, 1.4f));

        }
        else
        {
            currentWeaponInfo.text = string.Format("{0} : {1}", currentWeapon.weaponName, currentWeapon.currentBullet);
            currentWeaponInfo.transform.position = cam.WorldToScreenPoint(player.transform.position + new Vector3(0, 1, 1.4f));

        }
    }
}
