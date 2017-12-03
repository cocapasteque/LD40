using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class TopDownController : MonoBehaviour
{
    #region Public properties
    public float moveSpeed = 5f;
    public float health = 100f;
    public float insanity = 0f;
    public bool slowed = false;
    #endregion
    #region Private properties
    private Rigidbody2D rb;
    #endregion
    public GameObject projectile;
    public GameObject combatText;
    public Animator anim;

    public GameObject shootPosition;
    public GameObject shootParent;
    public SpriteRenderer playerSprite;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0 || insanity >= 100)
        {
            GameController.instance.isGameOver = true;
            return;
        }

        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        Move(h, v);
        Attack();

        if (h != 0 || v != 0) anim.SetBool("isRunning", true);
        else anim.SetBool("isRunning", false);

        if (insanity >= 70) anim.SetInteger("madnessLevel", 2);
        else if (insanity >= 50) anim.SetInteger("madnessLevel", 1);
        else anim.SetInteger("madnessLevel", 0);

        if (Input.GetButton("Fire2")) slowed = true;
        else slowed = false;

        

        var canvas = transform.Find("Canvas");
        canvas.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
        canvas.rotation = Quaternion.identity;
    }

    void FixedUpdate()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
        Vector3 dir = mousePos - transform.position;

        if (dir.x < 0) playerSprite.flipX = true;
        else playerSprite.flipX = false;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        shootParent.transform.localRotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void Move(float h, float v)
    {
        Debug.Log("H: "+h+" | V: " + v);
        Vector2 move;
        if (slowed)
        {
            move = new Vector2(h * Time.deltaTime * (moveSpeed / 3),v * Time.deltaTime * (moveSpeed / 3));
            rb.velocity = move;
            return;
        }
        move = new Vector2(h * Time.deltaTime * moveSpeed, v * Time.deltaTime * moveSpeed);
        rb.velocity = move;
    }

    void Attack()
    {
        if (Input.GetButton("Fire2"))
        {

            if (insanity > 0) insanity -= 0.5f;
            return;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (insanity < 100) insanity += 10;
            var clone = Instantiate(projectile, shootPosition.transform.position, transform.rotation);

            var mousePos =
                Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,
                    Input.mousePosition.z));
            Vector2 dir = mousePos - transform.position;
            dir.Normalize();
            if (insanity > 70)
            {
                Vector2 dir2 = new Vector2(dir.x + Random.Range(-1f + (dir.x), 1f - (dir.x)), dir.y + Random.Range(-1f + (dir.y), 1f - (dir.y)));
                dir = dir2;
            }
            else if (insanity > 50)
            {
                Vector2 dir2 = new Vector2(dir.x + Random.Range(-0.5f, 0.5f), dir.y + Random.Range(-0.5f, 0.5f));
                dir = dir2;
            }else if (insanity > 25)
            {
                Vector2 dir2 = new Vector2(dir.x + Random.Range(-0.35f, 0.35f), dir.y + Random.Range(-0.35f, 0.35f));
                dir = dir2;
            }
            clone.GetComponent<Rigidbody2D>().velocity = dir * clone.GetComponent<Projectile>().spell.Speed;
        }
    }

    public void Hit(float damageReceived)
    {
        if (health >= 0) health -= damageReceived;
        if (insanity <= 100) insanity += 20;
        var canvas = transform.Find("Canvas");
        var cbtxt = Instantiate(combatText, canvas.position, canvas.rotation, canvas);
        cbtxt.GetComponent<Text>().text = ((int)damageReceived).ToString();
    }

}
