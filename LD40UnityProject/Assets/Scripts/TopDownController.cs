using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class TopDownController : MonoBehaviour
{
    #region Public properties
    public float moveSpeed = 5f;
    public float health = 100f;
    public float insanity = 0f;
    #endregion
    #region Private properties
    private Rigidbody2D rb;
    #endregion
    public GameObject projectile;
    public GameObject combatText;

    public GameObject shootPosition;

    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }
	
	// Update is called once per frame
	void Update ()
	{
        var h = Input.GetAxis("Horizontal");
	    var v = Input.GetAxis("Vertical");
        Move(h, v);
        Attack();

        var canvas = transform.Find("Canvas");
        canvas.position = new Vector3(transform.position.x,transform.position.y, transform.position.z-1);
        canvas.rotation = Quaternion.identity;

    }




    void FixedUpdate()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
        Vector3 dir = mousePos - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void Move(float h, float v)
    {
        var move = new Vector2(transform.position.x + (h * Time.deltaTime * moveSpeed),transform.position.y + (v * Time.deltaTime * moveSpeed));
        rb.MovePosition(move);
    }

    void Attack()
    {
        if (Input.GetButton("Fire2"))
        {
            if(insanity > 0) insanity -= 0.5f;
            return;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            if(insanity < 100) insanity += 5;
            var clone = Instantiate(projectile, shootPosition.transform.position, transform.rotation);

            var mousePos =
                Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,
                    Input.mousePosition.z));
            Vector2 dir = mousePos - transform.position;
            dir.Normalize();
            clone.GetComponent<Rigidbody2D>().velocity = dir * clone.GetComponent<Projectile>().spell.Speed;
        }
    }


    public void Hit(float damageReceived)
    {
        health -= damageReceived;
        insanity += 5;
        var canvas = transform.Find("Canvas");
        var cbtxt = Instantiate(combatText, canvas.position, canvas.rotation, canvas);
        cbtxt.GetComponent<Text>().text = ((int)damageReceived).ToString();
        Debug.Log((int)damageReceived);
        //Debug.Log("AIE MORRAY");
    }

}
