using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Sprite doorClosed;
    public Sprite doorOpened;

    public DoorPosition position;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            switch (position)
            {
                case DoorPosition.Top:
                    CameraMovement.instance.MoveUp();
                    break;
                case DoorPosition.Bottom:
                    CameraMovement.instance.MoveDown();
                    break;
                case DoorPosition.Left:
                    CameraMovement.instance.MoveLeft();
                    break;
                case DoorPosition.Right:
                    CameraMovement.instance.MoveRight();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            GameController.instance.NextRoom();
        }
    }

    public void SetDoorOpened()
    {
        GetComponent<SpriteRenderer>().sprite = doorOpened;
    }

    public void SetDoorClosed()
    {
        GetComponent<SpriteRenderer>().sprite = doorClosed;
    }
}

public enum DoorPosition
{
    Top,
    Bottom,
    Left,
    Right
}
