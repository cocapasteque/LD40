using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public EnemyType type;
    public float speed = 2f;
    private Transform player;
    public float damage;
    public float criticChance;
    public float criticValue;

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
            case EnemyType.Explode:
                ExplodeBehaviour();
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

    void ExplodeBehaviour()
    {
        var dir = player.position - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        dir.Normalize();
        GetComponent<Rigidbody2D>().MovePosition(transform.position + dir * Time.deltaTime * speed);
    }
    void RangedBehaviour()
    {
        var dir = player.position - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        dir.Normalize();

        var clone = Instantiate(bulletPrefab, transform.position, transform.rotation);
        //clone.GetComponent<Rigidbody2D>().velocity = dir * clone.GetComponent<Projectile>().spell.Speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {       
            float damageDone = damage;
            if (Random.value*100 > criticChance)
            {
                damageDone += criticValue;
            }
            float randomize;
            if((randomize = Random.value) < 0.5)
            {
                damageDone -= damageDone * randomize;
            }
            else
            {
                damageDone += damageDone * (randomize -(float) 0.5);
            }
            

            player.GetComponent<TopDownController>().Hit(damageDone);
        }else if (other.tag == "bullet")
        {
            var canvas = transform.Find("Canvas");
            var cbtxt = Instantiate(combatText, canvas.position, canvas.rotation, canvas);
            cbtxt.GetComponent<Text>().text = ((int)damageReceived).ToString();
        }
    }


}

public enum EnemyType
{
    Melee, Ranged, Static, Explode
}
