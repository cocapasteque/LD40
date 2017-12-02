using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyType type;
    public float speed = 2f;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    // Update is called once per frame
    void Update()
    {
        switch (type)
        {
            case EnemyType.Melee:
                MeleeBehaviour();
                break;
            case EnemyType.Ranged:
                break;
            case EnemyType.Static:
                break;
        }
    }

    void MeleeBehaviour()
    {
        var dir = player.position - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        dir.Normalize();
        GetComponent<Rigidbody2D>().MovePosition(transform.position + dir * Time.deltaTime * speed);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            player.GetComponent<TopDownController>().Hit();
        }
    }
}

public enum EnemyType
{
    Melee, Ranged, Static
}
