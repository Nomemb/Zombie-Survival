using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    [SerializeField]
    private int maxHP;
    private int currentHP;
    public int CurrentHP
    {
        get
        {
            return currentHP;
        }
        set
        {
            currentHP = Mathf.Clamp(value, 0, maxHP);
        }
    }
    public int MaxHP
    {
        get
        {
            return maxHP;
        }
    }

    [SerializeField]
    private float hpRegenTime;
    private float perfectGameTime;

    private PlayerController playerController;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        currentHP = maxHP;
    }

    private void Update()
    {
        if (!playerController.isDie)
        {
            HPRegen();
        }


    }

    private void HPRegen()
    {
        perfectGameTime += Time.deltaTime;
        if (playerController.isDamage)
            perfectGameTime = 0;
        if(perfectGameTime > hpRegenTime)
        {
            currentHP += 20;
            perfectGameTime = 0;
        }
    }
}
