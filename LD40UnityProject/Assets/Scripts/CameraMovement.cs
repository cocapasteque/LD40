using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public static CameraMovement instance;

    void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != null) Destroy(this);
    }

    public void MoveLeft()
    {
        Debug.Log("Move left");
    }

    public void MoveRight()
    {
        Debug.Log("Move right");
    }

    public void MoveUp()
    {
        Debug.Log("Move up");
    }
    public void MoveDown()
    {
        Debug.Log("Move down");

    }
}
