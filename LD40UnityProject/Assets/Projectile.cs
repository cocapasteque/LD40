using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public Spell spell;
    public bool projectileCollide;


    void OnTriggerEnter2D(Collider2D collision)
    {
        projectileCollide = true; 
    }
}
