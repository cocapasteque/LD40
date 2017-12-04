using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (other.GetComponent<TopDownController>().health + 30 >= 100)
                other.GetComponent<TopDownController>().health = 100;
            else
                other.GetComponent<TopDownController>().health += 30;
            Destroy(gameObject);
        }
    }
}
