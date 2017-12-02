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
        var player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = GameController.instance.currentRoom.transform.Find("DoorRight").position;
        player.transform.position = new Vector3(player.transform.position.x - 1f, player.transform.position.y, player.transform.position.z);
    }
    public void MoveRight()
    {
        GameController.instance.currentRoom = GameController.instance.map[GameController.instance.currentRoom.x +1,
            GameController.instance.currentRoom.y];

        var offset = GameController.instance.offset;
        var newPos = new Vector3(transform.position.x + offset, transform.position.y, transform.position.z);
        transform.position = newPos;
        var player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = GameController.instance.currentRoom.transform.Find("DoorLeft").position;
        player.transform.position = new Vector3(player.transform.position.x + 1f, player.transform.position.y, player.transform.position.z);
    }
    public void MoveUp()
    {
        GameController.instance.currentRoom = GameController.instance.map[GameController.instance.currentRoom.x,
            GameController.instance.currentRoom.y+1];
        var offset = GameController.instance.offset;
        var newPos = new Vector3(transform.position.x, transform.position.y + offset, transform.position.z);
        transform.position = newPos;
        var player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = GameController.instance.currentRoom.transform.Find("DoorBot").position;
        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 1f, player.transform.position.z);
    }
    public void MoveDown()
    {
        GameController.instance.currentRoom = GameController.instance.map[GameController.instance.currentRoom.x,
            GameController.instance.currentRoom.y - 1];
        var offset = GameController.instance.offset;
        var newPos = new Vector3(transform.position.x, transform.position.y - offset, transform.position.z);
        transform.position = newPos;
        var player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = GameController.instance.currentRoom.transform.Find("DoorTop").position;
        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 1f, player.transform.position.z);
    }
}
