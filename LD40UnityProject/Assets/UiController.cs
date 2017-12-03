using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{

    public Transform health;
    public Transform insanity;

    public Text GameOverText;
    public Text WinText;
    public Text ScoreText;

    public Color from;
    public Color to;

	// Update is called once per frame
	void Update ()
	{
	    ScoreText.text = "Score : " + GameController.instance.score;
	    if (GameController.instance.isGameOver)
	    {
	        if (!GameOverText.gameObject.activeSelf) GameOverText.gameObject.SetActive(true);
	        StartCoroutine(FadeText(GameOverText, to));
	    }
        else if (GameController.instance.winGame)
	    {
	        if (!WinText.gameObject.activeSelf) WinText.gameObject.SetActive(true);
	        StartCoroutine(FadeText(WinText, to));
        }
        else
	    {
	        if (GameOverText.gameObject.activeSelf) GameOverText.gameObject.SetActive(false);
	        if (WinText.gameObject.activeSelf) WinText.gameObject.SetActive(false);

            var healthValue = GameObject.Find("Player").GetComponent<TopDownController>().health;
	        var insanityValue = GameObject.Find("Player").GetComponent<TopDownController>().insanity;
	        health.GetComponent<RectTransform>().localScale = new Vector3(healthValue / 100, 1, 1);
	        insanity.GetComponent<RectTransform>().localScale = new Vector3(insanityValue / 100, 1, 1);
	    }
	    
	}

    IEnumerator FadeText(Text text, Color to)
    {
        float t = 0;
        text.color = Color.clear;
        while (text.color != Color.white)
        {
            var newColor = new Color(text.color.r + 0.01f, text.color.g + 0.01f, text.color.b + 0.01f, text.color.a + 0.01f);
            text.color = newColor;
            yield return null;
        }
        GameController.instance.gameOverDisplayed = true;
        GameController.instance.winGameDisplayed = true;
        yield return null;
    }
    
}
