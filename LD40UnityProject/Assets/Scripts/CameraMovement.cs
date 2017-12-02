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
        GameController.instance.currentRoom = GameController.instance.map[GameController.instance.currentRoom.x - 1,
            GameController.instance.currentRoom.y];

        var offset = GameController.instance.offset;
        var newPos = new Vector3(transform.position.x - offset, transform.position.y, transform.position.z);
        transform.position = newPos;
    }
    public void MoveRight()
    {
        GameController.instance.currentRoom = GameController.instance.map[GameController.instance.currentRoom.x +1,
            GameController.instance.currentRoom.y];

        var offset = GameController.instance.offset;
        var newPos = new Vector3(transform.position.x + offset, transform.position.y, transform.position.z);
        transform.position = newPos;
    }
    public void MoveUp()
    {
        GameController.instance.currentRoom = GameController.instance.map[GameController.instance.currentRoom.x,
            GameController.instance.currentRoom.y+1];
        var offset = GameController.instance.offset;
        var newPos = new Vector3(transform.position.x, transform.position.y + offset, transform.position.z);
        transform.position = newPos;
    }
    public void MoveDown()
    {
        GameController.instance.currentRoom = GameController.instance.map[GameController.instance.currentRoom.x,
            GameController.instance.currentRoom.y - 1];
        var offset = GameController.instance.offset;
        var newPos = new Vector3(transform.position.x, transform.position.y - offset, transform.position.z);
        transform.position = newPos;
    }
}
