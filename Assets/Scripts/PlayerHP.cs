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
    void Start()
    {
        currentHP = maxHP;
    }

    
}
