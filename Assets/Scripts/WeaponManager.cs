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
    private void Start()
    {
        hasWeapons = new bool[weapons.Length];
        enableWeapons = new bool[weapons.Length];
        // 권총은 기본 아이템이므로 시작부터 보유처리해줌
        hasWeapons[0] = true;
        enableWeapons[0] = true;
        currentWeapon = weapons[0];
        currentWeaponIndex = 0;
    }
    void Update()
    {
        EnableWeaponUpdate();
        // 테스트용
        TestWeapon();
        Swap();
    }

    // Item을 먹었을 때 일정 확률로 총이 걸렸을때 실행되는 함수.
    public void GetRandomWeapon()
    {
        // enableWeapon 배열을 통해 현재 획득 가능한 웨폰들 중 랜덤으로 획득하게 함.
        // 해당 웨폰의 장탄 수를 최대로 늘려주고, 만약 보유중이지 않았다면 hasWeapons를 true로 바꿔줘야 함.
        while (true)
        {
            // 권총은 탄창 수가 제한이 없으므로 빼고 랜덤을 돌림.
            int randomWeapon = Random.Range(0, enableWeapons.Length);
            if (enableWeapons[randomWeapon])
            {
                hasWeapons[randomWeapon] = true;
                weapons[randomWeapon].GetComponent<Weapon>().currentBullet = weapons[randomWeapon].GetComponent<Weapon>().maxBullet;
                // UI : weapons[randomWeapon].GetComponent<Weapon>().WeaponName 을 획득했습니다!
                return;
            }

        }

    }

    // 현재 점수에 따라 획득 가능한 총을 업데이트하는 함수.
    private void EnableWeaponUpdate()
    {
        // 아마 경험치 클래스로 바꿔줘야 할 듯
        // 플레이어의 점수를 계속 받아와서 일정 수준의 경험치를 넘게 되면 해당 총을 사용 가능 (enableWeapon(true))으로 바꿔줌.
    }

    // 테스트용
    private void TestWeapon()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            hasWeapons[i] = true;
            enableWeapons[i] = true;
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
        if(currentWeaponIndex != _index)
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
}
