using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HPBarScript : MonoBehaviour
{
    [SerializeField]
    private Slider hpBarPrefab = null;

    private GameObject player;
    private Slider playerHPBar;
    private PlayerHP playerHP;

    Camera cam = null;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player");
        playerHP = FindObjectOfType<PlayerHP>();
        playerHPBar = Instantiate(hpBarPrefab, player.transform.position, Quaternion.identity, transform);
        playerHPBar.value = (float)playerHP.CurrentHP / playerHP.MaxHP;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerHPBar.value = (float)playerHP.CurrentHP / playerHP.MaxHP;
        playerHPBar.transform.position = cam.WorldToScreenPoint(player.transform.position + new Vector3(0, 0, 1));
    }
}
