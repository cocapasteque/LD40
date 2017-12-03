using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThanksScene : MonoBehaviour {

    public float time = 5f;
    private float current = 0f;

    void Update()
    {
        current += Time.deltaTime;
        if(current >= time)
        {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
