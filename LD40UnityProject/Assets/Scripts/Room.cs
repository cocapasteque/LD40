using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    public int x;
    public int y;
    public bool completed = false;
    public bool bossRoom = false;
    public int enemyAmount = 0;
    public List<GameObject> enemies;
    public List<GameObject> crates;
    public Transform bossPosition;
    public Transform potionPosition;
    public Transform powerupPosition;

    public bool asTop = false;
    public bool asLeft = false;
    public bool asRight = false;
    public bool asBot = false;

    // Use this for initialization
    void Start ()
	{
	    if (!bossRoom)
	    {
	        enemyAmount = Random.Range(1, 5);
	        enemies = new List<GameObject>();
	        for (var i = 0; i < enemyAmount; i++)
	        {
	            var randenemy = GameController.instance.ennemies[Random.Range(0, GameController.instance.ennemies.Count)];
	            enemies.Add(randenemy);
	        }
	    }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
