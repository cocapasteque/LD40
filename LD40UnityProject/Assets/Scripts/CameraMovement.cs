using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public static CameraMovement instance;

    void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(this);
    }

    public void MoveLeft()
    {
        var offset = GameController.instance.offset;
        var newPos = new Vector3(transform.position.x - offset, transform.position.y, transform.position.z);
        transform.position = newPos;
    }
    public void MoveRight()
    {
        var offset = GameController.instance.offset;
        var newPos = new Vector3(transform.position.x + offset, transform.position.y, transform.position.z);
        transform.position = newPos;
    }
    public void MoveUp()
    {
        var offset = GameController.instance.offset;
        var newPos = new Vector3(transform.position.x, transform.position.y + offset, transform.position.z);
        transform.position = newPos;
    }
    public void MoveDown()
    {
        var offset = GameController.instance.offset;
        var newPos = new Vector3(transform.position.x, transform.position.y - offset, transform.position.z);
        transform.position = newPos;
    }
}
