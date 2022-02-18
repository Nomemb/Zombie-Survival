using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ZombieData", menuName = "ZombieData", order = 1)]
public class ZombieData : ScriptableObject
{
    [SerializeField]
    private string zombieName;
    public string ZombieName { get { return zombieName; } }
    [SerializeField]
    private int zombieHP;
    public int ZombieHP { get { return zombieHP; } }
    [SerializeField]
    private int zombieDamage;
    public int ZombieDamage { get { return zombieDamage; } }
    [SerializeField]
    private float zombieAttackSpeed;
    public float ZombieAttackSpeed { get { return zombieAttackSpeed; } }
    [SerializeField]
    private int zombieDropRate;
    public int ZombieDropRate { get { return zombieDropRate; } }

}
