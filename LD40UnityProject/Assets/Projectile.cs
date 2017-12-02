using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public Spell spell;
    


    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Object Entered the trigger");
        if (other.tag == "Enemy")
        {
            // Inflige damage
        }
        // Destroy bullet
        Destroy(gameObject);
    }
        
}
