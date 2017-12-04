using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class EnemyController : MonoBehaviour
{


    public GameObject bulletPrefab;
    public GameObject explodePrefab;
    public GameObject combatText;
    public GameObject shootPosition;
    public AudioClip hurtSound;
    public AudioClip deathSound;

    public bool isBoss;

    public float shootSpeed;
    public float currentTime;

    public EnemyType type;
    public float speed = 2f;
    private Transform player;
    public float health = 200;
    public float damage;
    public float criticChance;
    public float criticValue;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (type == EnemyType.Ranged)
        {
            switch (Random.Range(0, 4))
            {
                case 0:
                    bulletPrefab = Resources.Load<GameObject>("Fireball");
                    break;
                case 1:
                    bulletPrefab = Resources.Load<GameObject>("Frostball");
                    break;
                case 2:
                    bulletPrefab = Resources.Load<GameObject>("ArcaneShot");
                    break;
                case 3:
                    bulletPrefab = Resources.Load<GameObject>("CosmicShot");
                    break;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (GameController.instance.isGameOver) return;
        if (health <= 0)
        {
            Die();
        }
        switch (type)
        {
            case EnemyType.Melee:
                MeleeBehaviour();
                break;
            case EnemyType.Ranged:
                RangedBehaviour();
                break;
            case EnemyType.Static:
                break;
            case EnemyType.Explode:
                ExplodeBehaviour();
                break;
        }

        var canvas = transform.Find("CanvasE");
        canvas.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
        canvas.rotation = Quaternion.identity;
    }




    void MeleeBehaviour()
    {
        var dir = player.position - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        dir.Normalize();
        GetComponent<Rigidbody2D>().velocity = new Vector2(dir.x * Time.deltaTime * speed, dir.y * Time.deltaTime * speed);
    }

    void ExplodeBehaviour()
    {
        var dir = player.position - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        dir.Normalize();
        GetComponent<Rigidbody2D>().velocity = new Vector2(dir.x * Time.deltaTime * speed, dir.y * Time.deltaTime * speed);
    }
    void RangedBehaviour()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
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
        var clone = Instantiate(bulletPrefab, shootPosition.transform.position, transform.rotation);
        clone.GetComponent<Rigidbody2D>().velocity = dir * clone.GetComponent<Projectile>().spell.Speed;
    }

    void Die()
    {
        if (isBoss)
        {
            GameController.instance.score += 150;
            GameController.instance.winGame = true;
        }
        GameController.instance.score += 10;
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            player.GetComponent<TopDownController>().Hit(damage);
            if (type == EnemyType.Explode)
            {
                var explosion = Instantiate(explodePrefab, transform.position, transform.rotation);
                Destroy(explosion, 1);
                Destroy(gameObject);
            }

        }
        else if (other.tag == "Bullet")
        {
            var canvas = transform.Find("CanvasE");
            var cbtxt = Instantiate(combatText, canvas.position, canvas.rotation, canvas);
            cbtxt.GetComponent<Text>().text = ((int)other.GetComponent<Projectile>().spell.Damage).ToString();
            health -= other.GetComponent<Projectile>().spell.Damage;

            AudioSource audio = GetComponent<AudioSource>();

            if (!audio.isPlaying)
            {

                audio.Play();
            }

        }
        else
        {
            //Debug.Log("Unknown collider: "+ other.tag);
        }
    }


}

public enum EnemyType
{
    Melee, Ranged, Static, Explode
}
