using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieData : ScriptableObject
{
    [SerializeField]
    private string zombieName;
    public string ZombieName { get { return zombieName; } }
    [SerializeField]
    private int zombieHP;
    public int ZombieHP { get { return zombieHP; } }
}
