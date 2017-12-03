using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    public int x;
    public int y;
    public bool completed = false;
    public int enemyAmount = 0;
    public List<GameObject> enemies;
	// Use this for initialization
	void Start ()
	{
	    enemyAmount = Random.Range(1, 5);
        enemies = new List<GameObject>();
	    for(var i = 0; i < enemyAmount; i++)
	    {
	        var randenemy = GameController.instance.ennemies[Random.Range(0, GameController.instance.ennemies.Count)];
	        enemies.Add(randenemy);
	    }

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
