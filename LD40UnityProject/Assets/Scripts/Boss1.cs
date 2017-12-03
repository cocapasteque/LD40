using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{

    public GameObject bulletPrefab;
    private Transform player;
    public float speed = 2f;
    public float currentTime;
    public float health = 200;
    public float damage;
    public float criticChance;
    public float criticValue;
    public float shootSpeed = 0.5f;
    Vector2 initAOEdir;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        initAOEdir = player.position - transform.position;
    }


    void Update()
    {
        if (GameController.instance.isGameOver) return;
        if (health <= 0)
        {
            Die();
        }



    }


    void aoe()
    {
        var dir = player.position - transform.position;

        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        dir.Normalize();

        currentTime += Time.deltaTime;
        if (currentTime >= shootSpeed)
        {

            Shoot(dir);

        }

    }

    void Shoot(Vector3 dir)
    {
        currentTime = 0;
     //   var clone = Instantiate(bulletPrefab, shootPosition.transform.position, transform.rotation);
      //  clone.GetComponent<Rigidbody2D>().velocity = dir * clone.GetComponent<Projectile>().spell.Speed;
    }

    void Die()
    {
        Destroy(gameObject);
    }
}

