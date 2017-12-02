using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

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
}

public enum DoorPosition
{
    Top,
    Bottom,
    Left,
    Right
}
