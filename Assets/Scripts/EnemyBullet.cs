﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int bulletDamage;
    public bool isMelee;

    private void Update()
    {
        if(GetComponentInParent<Zombie>() != null)
        {
            bulletDamage = FindObjectOfType<Zombie>().zombieDamage;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!isMelee && collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
