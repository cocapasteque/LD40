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
    }

    void Move(float h, float v)
    {
        var move = new Vector2(transform.position.x + (h * Time.deltaTime * moveSpeed),transform.position.y + (v * Time.deltaTime * moveSpeed));
        rb.MovePosition(move);
    }
}
