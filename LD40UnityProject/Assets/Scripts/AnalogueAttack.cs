using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalogueAttack : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        var mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z - transform.position.z));
        transform.LookAt(mousePos);
        

    }
}

