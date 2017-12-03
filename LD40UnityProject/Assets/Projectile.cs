using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public Spell spell;
    


    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Object Entered the trigger");
        if (other.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<TopDownController>().Hit(spell.Damage);
        }
        // Destroy bullet
        Destroy(gameObject);
        var o = Instantiate(spell.explosion, transform.position, Quaternion.identity);
        Destroy(o, 0.5f);
    }
       
}
