using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject[] weapons;
    public bool[] hasWeapons;
    public bool[] enableWeapons;

    private GameObject currentWeapon;
    public int currentWeaponIndex;
    private ScoreManager score;
    private int[] levelSystems;
    public int level;


    private TextInfoManager info;
    private void Start()
    {
        hasWeapons = new bool[weapons.Length];
        enableWeapons = new bool[weapons.Length];
        levelSystems = new int[24]
        {
            900, 2000, 6000, 9000, 11000, 35000, 39000, 70000, 80000,
            260000, 290000, 300000, 400000, 500000, 620000, 680000, 700000, 950000,
            1100000, 1360000, 2000000, 3000000, 4000000, 6000000
        };
        // 테스트용
        // TestWeapon();
        // 권총은 기본 아이템이므로 시작부터 보유처리해줌
        hasWeapons[0] = true;
        enableWeapons[0] = true;
        currentWeapon = weapons[0];
        currentWeaponIndex = 0;
        weapons[0].GetComponent<Weapon>().currentBullet = weapons[0].GetComponent<Weapon>().maxBullet;
        score = FindObjectOfType<ScoreManager>();
        info = FindObjectOfType<TextInfoManager>();
    }
    void Update()
    {
        LevelUP();
        Swap();
    }

    // Item을 먹었을 때 일정 확률로 총이 걸렸을때 실행되는 함수.
    public void GetRandomWeapon()
    {
        // enableWeapon 배열을 통해 현재 획득 가능한 웨폰들 중 랜덤으로 획득하게 함.
        // 해당 웨폰의 장탄 수를 최대로 늘려주고, 만약 보유중이지 않았다면 hasWeapons를 true로 바꿔줘야 함.
        while (true)
        {
            int randomWeapon = Random.Range(1, enableWeapons.Length);
            if (enableWeapons[randomWeapon])
            {
                hasWeapons[randomWeapon] = true;
                weapons[randomWeapon].GetComponent<Weapon>().currentBullet = weapons[randomWeapon].GetComponent<Weapon>().maxBullet;
                info.AddInfo(weapons[randomWeapon].GetComponent<Weapon>().weaponName + " 을 획득했습니다!");
                return;
            }
        }
    }

    private void LevelUP()
    {
        if (levelSystems[level] <= score.currentScore)
        {
            UpgradeWeapon(level);
            level++;
        }

    }
    // 현재 레벨에 따라 획득 가능한 총의 능력치를 향상시키는 함수.
    private void UpgradeWeapon(int level)
    {
        switch (level)
        {
            case 0:
                // Pistol 공격속도 증가
                weapons[0].GetComponent<Weapon>().weaponRPM *= 0.8f;
                info.AddInfo("<color=#258725>" + "Pistol : 공격속도 증가" + "</color>");
                break;

            case 1:
                // UZI 해금
                enableWeapons[1] = true;
                hasWeapons[1] = true;
                weapons[1].GetComponent<Weapon>().currentBullet = weapons[1].GetComponent<Weapon>().maxBullet;
                info.AddInfo("<color=#258725>" + "새로운 무기 : UZI (key2)" + "</color>");
                break;

            case 2:
                // Pistol 공격력 증가
                weapons[0].GetComponent<Weapon>().weaponDamage = (int)(weapons[0].GetComponent<Weapon>().weaponDamage * 1.5);
                info.AddInfo("<color=#258725>" + "Pistol : 공격력 증가" + "</color>");
                break;

            case 3:
                // 샷건 해금
                enableWeapons[2] = true;
                hasWeapons[2] = true;
                weapons[2].GetComponent<Weapon>().currentBullet = weapons[2].GetComponent<Weapon>().maxBullet;
                info.AddInfo("<color=#258725>" + "새로운 무기 : Shotgun (key3)" + "</color>");
                break;

            case 4:
                // UZI 공격속도 증가
                weapons[1].GetComponent<Weapon>().weaponRPM *= 0.8f;
                info.AddInfo("<color=#258725>" + "UZI : 공격속도 증가" + "</color>");
                break;

            case 5:
                // UZI 장탄수 증가
                weapons[1].GetComponent<Weapon>().maxBullet *= 2;
                info.AddInfo("<color=#258725>" + "UZI : 최대 장탄 수 증가" + "</color>");
                break;

            case 6:
                // 샷건 공격속도 증가
                weapons[2].GetComponent<Weapon>().weaponRPM *= 0.8f;
                info.AddInfo("<color=#258725>" + "Shotgun : 공격속도 증가" + "</color>");
                break;

            case 7:
                // 샷건 장탄수 증가
                weapons[2].GetComponent<Weapon>().maxBullet *= 2;
                info.AddInfo("<color=#258725>" + "ShotGun : 최대 장탄 수 증가" + "</color>");
                break;

            case 8:
                // UZI 사거리 증가
                weapons[1].GetComponent<Weapon>().weaponRange *= 1.5f;
                info.AddInfo("<color=#258725>" + "UZI : 사거리 증가" + "</color>");
                break;

            case 9:
                // 샷건 사거리 증가
                weapons[2].GetComponent<Weapon>().weaponRange *= 1.5f;
                info.AddInfo("<color=#258725>" + "ShotGun : 사거리 증가" + "</color>");
                break;

            case 10:
                // UZI 장탄수 증가
                weapons[1].GetComponent<Weapon>().maxBullet *= 2;
                info.AddInfo("<color=#258725>" + "UZI : 최대 장탄 수 증가" + "</color>");
                break;

            case 11:
                // 샷건 장탄수 증가
                weapons[2].GetComponent<Weapon>().maxBullet *= 2;
                info.AddInfo("<color=#258725>" + "ShotGun : 최대 장탄 수 증가" + "</color>");
                break;

            case 12:
                // AK47 해금
                enableWeapons[3] = true;
                hasWeapons[3] = true;
                weapons[3].GetComponent<Weapon>().currentBullet = weapons[3].GetComponent<Weapon>().maxBullet;
                info.AddInfo("<color=#258725>" + "새로운 무기 : AK47 (key4)" + "</color>");
                break;

            case 13:
                // 샷건 공격속도 증가
                weapons[2].GetComponent<Weapon>().weaponRPM *= 0.8f;
                info.AddInfo("<color=#258725>" + "ShotGun : 공격속도 증가" + "</color>");
                break;

            case 14:
                // AK47 공격속도 증가
                weapons[3].GetComponent<Weapon>().weaponRPM *= 0.8f;
                info.AddInfo("<color=#258725>" + "AK47 : 공격속도 증가" + "</color>");
                break;

            case 15:
                // UZI 데미지 증가
                weapons[1].GetComponent<Weapon>().weaponDamage = (int)(weapons[1].GetComponent<Weapon>().weaponDamage * 1.5);
                info.AddInfo("<color=#258725>" + "UZI : 데미지 증가" + "</color>");
                break;

            case 16:
                // 스나이퍼 해금
                enableWeapons[4] = true;
                hasWeapons[4] = true;
                weapons[4].GetComponent<Weapon>().currentBullet = weapons[4].GetComponent<Weapon>().maxBullet;
                info.AddInfo("<color=#258725>" + "새로운 무기 : Sniper (key5)" + "</color>");
                break;

            case 17:
                // 샷건 사거리 증가
                weapons[2].GetComponent<Weapon>().weaponRange *= 1.5f;
                info.AddInfo("<color=#258725>" + "ShotGun : 사거리 증가" + "</color>");
                break;

            case 18:
                // AK47 장탄 수 증가
                weapons[4].GetComponent<Weapon>().maxBullet *= 2;
                info.AddInfo("<color=#258725>" + "AK47 : 최대 장탄 수 증가" + "</color>");
                break;

            case 19:
                // 샷건 데미지 증가
                weapons[2].GetComponent<Weapon>().weaponDamage = (int)(weapons[2].GetComponent<Weapon>().weaponDamage * 1.5);
                info.AddInfo("<color=#258725>" + "Shotgun : 데미지 증가" + "</color>");
                break;

            case 20:
                // 스나이퍼 공격속도 증가
                weapons[5].GetComponent<Weapon>().weaponRPM *= 0.8f;
                info.AddInfo("<color=#258725>" + "Sniper : 공격속도 증가" + "</color>");
                break;

            case 21:
                // UZI 사거리 증가
                weapons[1].GetComponent<Weapon>().weaponRange *= 1.5f;
                info.AddInfo("<color=#258725>" + "UZI : 사거리 증가" + "</color>");
                break;

            case 22:
                // 스나이퍼 장탄 수 증가
                weapons[5].GetComponent<Weapon>().maxBullet *= 2;
                info.AddInfo("<color=#258725>" + "Sniper : 최대 장탄 수 증가" + "</color>");
                break;

            case 23:
                // 스나이퍼 장탄 수 증가
                weapons[5].GetComponent<Weapon>().maxBullet *= 2;
                info.AddInfo("<color=#258725>" + "Sniper : 최대 장탄 수 증가" + "</color>");
                break;

            default:
                break;
        }

        /*
         점수 > 900 : 권총 공격속도 증가                
         점수 > 2000 : UZI 해금
         점수 > 6000 : 권총 공격력 증가
         점수 > 9000 : 샷건 해금
         점수 > 11000 : UZI 공격속도 증가
         점수 > 35000 : UZI 장탄수 증가
         점수 > 39000 : 샷건 공격속도 증가
         점수 > 70000 : 샷건 장탄수 증가
         점수 > 80000 : UZI 사거리 증가
         점수 > 260000 : 샷건 사거리 증가
         점수 > 290000 : UZI 장탄수 증가
         점수 > 300000 : 샷건 장탄수 증가
         점수 > 400000 : AK47 해금
         점수 > 500000 : 샷건 공격속도 증가
         점수 > 620000 : AK47 공격속도 증가
         점수 > 680000 : UZI 데미지 증가
         점수 > 700000 : 스나이퍼 해금
         점수 > 950000 : 샷건 사거리 증가
         점수 > 1100000 : AK47 장탄 수 증가
         점수 > 1360000 : 샷건 데미지 증가
         점수 > 2000000 : 스나이퍼 공격속도 증가
         점수 > 3000000 : UZI 사거리 증가
                 
         점수 > 4000000 : 스나이퍼 장탄 수 증가
         점수 > 6000000 : 스나이퍼 장탄 수 증가
         */
    }

    // 테스트용
    private void TestWeapon()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            hasWeapons[i] = true;
            enableWeapons[i] = true;
            weapons[i].GetComponent<Weapon>().currentBullet = weapons[i].GetComponent<Weapon>().maxBullet;

        }
    }
    // 총 바꿔주는 함수
    private void Swap()
    {
        // 해당 무기가 1) 현재 레벨에서 사용 가능 (enableWeapon(true)) 하고 2) 현재 보유중 (hasWeapon(true)) 이면
        // 착용중이던 무기를 setActive(false)하고 해당 무기로 교체
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwapCheck(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwapCheck(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwapCheck(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SwapCheck(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SwapCheck(5);
        }
    }

    private void SwapCheck(int _num)
    {
        // 배열 0부터 시작하므로 빼줌
        int _index = _num - 1;
        if (currentWeaponIndex != _index)
        {
            if (enableWeapons[_index])
            {
                if (hasWeapons[_index])
                {
                    SwapWeapon(_num);
                }
            }
        }
    }

    private void SwapWeapon(int _num)
    {
        int _index = _num - 1;
        currentWeapon.SetActive(false);
        currentWeapon = weapons[_index];
        currentWeaponIndex = _index;
        currentWeapon.SetActive(true);
    }

    // 총알이 다 떨어졌을 때 실행
    public void OutOfAmmo()
    {
        currentWeapon.SetActive(false);
        hasWeapons[currentWeaponIndex] = false;
        currentWeapon = weapons[0];
        currentWeaponIndex = 0;
        currentWeapon.SetActive(true);
    }
}
