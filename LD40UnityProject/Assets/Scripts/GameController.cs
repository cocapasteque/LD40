using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    public static GameController instance;

    public GameObject roomPrefab;
    public GameObject doorPrefab;
    public GameObject playerPrefab;

    public List<GameObject> ennemies;

    public int width = 5;
    public int height = 5;
    public int offset = 10;
    public Room[,] map;

    public int score = 0;

    public bool isGameOver = false;
    public bool gameOverDisplayed = false;
    public Room currentRoom;

    void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(this);
    }

    void Start()
    {
        GenerateMap();
    }

    void Update()
    {
        if (isGameOver)
        {
            if (gameOverDisplayed)
                if (Input.anyKey)
                {
                    SceneManager.LoadScene("MainMenu");
                    Time.timeScale = 1;
                }
            
            return;
        }

        var mobs = GameObject.FindGameObjectsWithTag("Enemy");
        if (mobs.Length == 0) currentRoom.completed = true;
    }

    void GenerateMap()
    {
        map = new Room[width, height];
        var mapObject = GameObject.Find("Map");
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                var go = Instantiate(roomPrefab);
                go.transform.parent = mapObject.transform;
                go.transform.position = new Vector3(i * offset, j * offset);
                map[i, j] = go.GetComponent<Room>();
                map[i, j].x = i;
                map[i, j].y = j;

                if (i != 0) AddLeftDoor(go);
                if (i != height - 1) AddRightDoor(go);
                if (j != 0) AddBottomDoor(go);
                if (j != width - 1) AddTopDoor(go);
            }
        }
        currentRoom = map[0, 0];
    }

    void AddBottomDoor(GameObject go)
    {
        var doorplaceholder = go.transform.Find("DoorBot");
        doorplaceholder.transform.Find("DoorCollider").GetComponent <BoxCollider2D>().enabled = false;
        var door = Instantiate(doorPrefab, doorplaceholder);
        door.GetComponent<Door>().position = DoorPosition.Bottom;
        door.GetComponent<Door>().transform.rotation = new Quaternion(0,0,-180,0);
    }
    void AddTopDoor(GameObject go)
    {
        var doorplaceholder = go.transform.Find("DoorTop");
        doorplaceholder.transform.Find("DoorCollider").GetComponent<BoxCollider2D>().enabled = false;

        var door = Instantiate(doorPrefab, doorplaceholder);
        door.GetComponent<Door>().position = DoorPosition.Top;
        door.GetComponent<Door>().transform.rotation = new Quaternion(0, 0, 0, 0);
    }
    void AddRightDoor(GameObject go)
    {
        var doorplaceholder = go.transform.Find("DoorRight");
        doorplaceholder.transform.Find("DoorCollider").GetComponent<BoxCollider2D>().enabled = false;

        var door = Instantiate(doorPrefab, doorplaceholder);
        door.GetComponent<Door>().position = DoorPosition.Right;
        door.GetComponent<Door>().transform.eulerAngles = new Vector3(0,0,-90);

    }
    void AddLeftDoor(GameObject go)
    {
        var doorplaceholder = go.transform.Find("DoorLeft");
        doorplaceholder.transform.Find("DoorCollider").GetComponent<BoxCollider2D>().enabled = false;
            
        var door = Instantiate(doorPrefab, doorplaceholder);
        door.GetComponent<Door>().position = DoorPosition.Left;
        door.GetComponent<Door>().transform.eulerAngles = new Vector3(0, 0, 90);
    }

    void GenerateMobs()
    {
        foreach (var o in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(o);
        }
        var enemyIndex = 0;
        for (var i = 0; i < currentRoom.enemyAmount; i++)
        {

            var x = Random.Range(currentRoom.transform.position.x - 5, currentRoom.transform.position.x + 5);
            var y = Random.Range(currentRoom.transform.position.y - 5, currentRoom.transform.position.y + 5);
            var mob = Instantiate(currentRoom.enemies[enemyIndex++], new Vector2(x,y), Quaternion.identity);
            mob.transform.parent = this.gameObject.transform;
        }
    }

    public void NextRoom()
    {
        if(!currentRoom.completed)
            GenerateMobs();
    }
}
