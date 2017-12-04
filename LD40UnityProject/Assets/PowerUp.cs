using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public PowerUpType type;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            switch (type)
            {
                case PowerUpType.Frost:
                    other.GetComponent<TopDownController>().projectile = Resources.Load<GameObject>("Frostball");
                    other.GetComponent<TopDownController>().shootRate = 0.5f;
                    break;
                case PowerUpType.Arcane:
                    other.GetComponent<TopDownController>().projectile = Resources.Load<GameObject>("ArcaneShot");
                    other.GetComponent<TopDownController>().shootRate = 1.5f;
                    break;
                case PowerUpType.Fire:
                    other.GetComponent<TopDownController>().projectile = Resources.Load<GameObject>("Fireball");
                    other.GetComponent<TopDownController>().shootRate = 1f;
                    break;
                case PowerUpType.Cosmic:
                    other.GetComponent<TopDownController>().projectile = Resources.Load<GameObject>("CosmicShot");
                    other.GetComponent<TopDownController>().shootRate = 2f;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            Destroy(gameObject);
        }
    }


}

public enum PowerUpType
{
    Frost, Arcane, Fire, Cosmic
}
