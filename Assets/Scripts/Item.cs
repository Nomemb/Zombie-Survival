using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType { HP, Gun };

    [SerializeField]
    private int gunDropRate;

    private Rigidbody myRigid;
    private Vector3 startPos;
    private bool direction;
    private WeaponManager weaponManager;
    private PlayerHP playerHP;
    [SerializeField]
    private Vector3 destinationPos;
    private void Start()
    {
        myRigid = GetComponent<Rigidbody>();
        playerHP = FindObjectOfType<PlayerHP>();
        startPos = transform.position;
        weaponManager = FindObjectOfType<WeaponManager>();

    }
    private void Update()
    {
        if (!direction)
            StartCoroutine(ItemDownCoroutine());
        else
            StartCoroutine(ItemUpCoroutine());
    }
    IEnumerator ItemDownCoroutine()
    {
        Vector3 _destinationPos = new Vector3(startPos.x, destinationPos.y, startPos.z);
        myRigid.position = Vector3.Lerp(transform.position, _destinationPos, Time.deltaTime * 2);
        if (transform.position.y < destinationPos.y + 0.05f)
            direction = true;

        yield return null;
    }
    IEnumerator ItemUpCoroutine()
    {
        myRigid.position = Vector3.Lerp(transform.position, startPos, Time.deltaTime * 2);
        if (transform.position.y > startPos.y - 0.05f)
            direction = false;

        yield return null;
    }

    public void GetItem()
    {
        // 일정 확률로 총이 드롭됐을 땐 현재 enable인 총들 충에서 또 랜덤으로 가져옴.
        if (Random.Range(1, 100) < gunDropRate)
        {
            Debug.Log("총 드랍!");
            weaponManager.GetRandomWeapon();

        }
        // 그 이외의 경우에는 HP가 드롭
        else
        {
            Debug.Log("HP 회복!");

            GetHPItem();
        }
    }

    // 체력을 최대치로 회복 ( 차후에 일정 수치만큼 회복으로 바꿀수도 있음 ) 
    private void GetHPItem()
    {
        playerHP.CurrentHP = playerHP.MaxHP;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetItem();
            Destroy(this.gameObject);
        }
    }
}
