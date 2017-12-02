using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class TopDownController : MonoBehaviour
{
    #region Public properties
    public float moveSpeed = 5f;
    #endregion
    #region Private properties
    private Rigidbody2D rb;
    #endregion
    public GameObject projectile;

    // Use this for initialization
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
<<<<<<< HEAD:LD40UnityProject/Assets/Scripts/TopDownController.cs
        if (Input.GetButtonDown("Fire1")) {
            var clone = Instantiate(projectile, transform.position, transform.rotation);
            var mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
            Vector2 dir = mousePos - transform.position;
            dir.Normalize();
            clone.GetComponent<Rigidbody2D>().velocity = dir * clone.GetComponent<Projectile>().spell.Speed;
        }
           
=======
        if (Input.GetButtonDown("Fire1")) { }      
    }

    public void Hit()
    {
        Debug.Log("AIE MORRAY");
>>>>>>> 02926316b7b22fe5fc6b7551fc373d1d9af7615c:LD40UnityProject/Assets/TopDownController.cs
    }
}
