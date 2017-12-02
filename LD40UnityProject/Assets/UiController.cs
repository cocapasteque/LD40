using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiController : MonoBehaviour
{

    public Transform health;
    public Transform insanity;

	// Update is called once per frame
	void Update ()
	{
	    if (!GameController.instance.isGameOver)
	    {
	        var healthValue = GameObject.Find("Player").GetComponent<TopDownController>().health;
	        var insanityValue = GameObject.Find("Player").GetComponent<TopDownController>().insanity;
	        health.GetComponent<RectTransform>().localScale = new Vector3(healthValue / 100, 1, 1);
	        insanity.GetComponent<RectTransform>().localScale = new Vector3(insanityValue / 100, 1, 1);
	    }
	}
}
