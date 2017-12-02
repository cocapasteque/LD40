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
    }
    public void MoveRight()
    {
    }
    public void MoveUp()
    {
    }
    public void MoveDown()
    {
    }
}
