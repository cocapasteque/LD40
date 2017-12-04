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
    public GameObject potionPrefab;

    public List<GameObject> powerUps;
    public List<GameObject> ennemies;
    public GameObject boss;

    public int width = 5;
    public int height = 5;
    public int offset = 10;
    public Room[,] map;

    public int score = 0;

    public bool isGameOver = false;
    public bool winGame = false;
    public bool gameOverDisplayed = false;
    public bool winGameDisplayed = false;
    public bool bossRoomAssigned = false;
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
                }
            
            return;
        }

        if (winGame)
        {
            if (winGameDisplayed)
                if (Input.anyKey)
                {
                    SceneManager.LoadScene("MainMenu");
                }

            return;
        }

        var mobs = GameObject.FindGameObjectsWithTag("Enemy");
        if (mobs.Length == 0)
        {
            OpenDoors();
            currentRoom.completed = true;
        }
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

                if ((i > height / 2 || j > width / 2) && !bossRoomAssigned)
                {
                    if(Random.Range(0,5) == 1) { 
                        go.GetComponent<Room>().bossRoom = true;
                        bossRoomAssigned = true;
                    }
                }

                if (Random.Range(0, 3) == 1)
                { 
                    var potion = Instantiate(potionPrefab, go.GetComponent<Room>().potionPosition.position,
                        Quaternion.identity);
                    potion.transform.SetParent(go.GetComponent<Room>().potionPosition);
                }

                if (Random.Range(0, 4) == 1)
                {
                    var pup = Instantiate(powerUps[Random.Range(0, powerUps.Count)],
                        go.GetComponent<Room>().powerupPosition.position, Quaternion.identity);
                    pup.transform.SetParent(go.GetComponent<Room>().powerupPosition);
                }

                if(!go.GetComponent<Room>().bossRoom) // No crates in boss room
                    AddCrates(go);
            }
        }
        if (!bossRoomAssigned)
        {
            map[width, height].bossRoom = true;
        }
        currentRoom = map[0, 0];
    }

    void AddCrates(GameObject go)
    {
        var room = go.GetComponent<Room>();
        foreach (var c in room.crates)
        {
            if (Random.Range(0, 2) == 1)
            {
                c.SetActive(true);
            }
        }
    }

    void AddBottomDoor(GameObject go)
    {
        go.GetComponent<Room>().asBot = true;
        var doorplaceholder = go.transform.Find("DoorBot");
        doorplaceholder.transform.Find("DoorCollider").GetComponent <BoxCollider2D>().enabled = false;
        var door = Instantiate(doorPrefab, doorplaceholder);
        door.GetComponent<Door>().position = DoorPosition.Bottom;
        door.GetComponent<Door>().transform.rotation = new Quaternion(0,0,-180,0);
    }
    void AddTopDoor(GameObject go)
    {
        go.GetComponent<Room>().asTop = true;
        var doorplaceholder = go.transform.Find("DoorTop");
        doorplaceholder.transform.Find("DoorCollider").GetComponent<BoxCollider2D>().enabled = false;

        var door = Instantiate(doorPrefab, doorplaceholder);
        door.GetComponent<Door>().position = DoorPosition.Top;
        door.GetComponent<Door>().transform.rotation = new Quaternion(0, 0, 0, 0);
    }
    void AddRightDoor(GameObject go)
    {
        go.GetComponent<Room>().asRight = true;
        var doorplaceholder = go.transform.Find("DoorRight");
        doorplaceholder.transform.Find("DoorCollider").GetComponent<BoxCollider2D>().enabled = false;

        var door = Instantiate(doorPrefab, doorplaceholder);
        door.GetComponent<Door>().position = DoorPosition.Right;
        door.GetComponent<Door>().transform.eulerAngles = new Vector3(0,0,-90);

    }
    void AddLeftDoor(GameObject go)
    {
        go.GetComponent<Room>().asLeft = true;
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

            var x = Random.Range(currentRoom.transform.position.x - 3, currentRoom.transform.position.x + 3);
            var y = Random.Range(currentRoom.transform.position.y - 3, currentRoom.transform.position.y + 3);
            var mob = Instantiate(currentRoom.enemies[enemyIndex++], new Vector2(x,y), Quaternion.identity);
            mob.transform.parent = this.gameObject.transform;
        }
    }

    void GenerateBoss()
    {
        foreach (var o in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(o);
        }
        var bossgo = Instantiate(boss, currentRoom.bossPosition.position, currentRoom.bossPosition.rotation);
        bossgo.transform.parent = gameObject.transform;
        bossgo.GetComponent<EnemyController>().isBoss = true;
    }

    public void NextRoom()
    {
        if (!currentRoom.completed)
        {
            CloseDoors();
            if (currentRoom.bossRoom)
                GenerateBoss();
            else
                GenerateMobs();

        }
    }

    public void CloseDoors()
    {
        var doorLeft = currentRoom.transform.Find("DoorLeft");
        var doorTop = currentRoom.transform.Find("DoorTop");
        var doorRight = currentRoom.transform.Find("DoorRight");
        var doorBot = currentRoom.transform.Find("DoorBot");

        if (currentRoom.asLeft) doorLeft.transform.Find("DoorCollider").GetComponent<BoxCollider2D>().enabled = true;
        if (currentRoom.asTop) doorTop.transform.Find("DoorCollider").GetComponent<BoxCollider2D>().enabled = true;
        if (currentRoom.asBot) doorBot.transform.Find("DoorCollider").GetComponent<BoxCollider2D>().enabled = true;
        if (currentRoom.asRight) doorRight.transform.Find("DoorCollider").GetComponent<BoxCollider2D>().enabled = true;

        var dl = doorLeft.GetComponentInChildren<Door>(); if(dl != null) dl.SetDoorClosed();
        var dt = doorTop.GetComponentInChildren<Door>(); if(dt != null) dt.SetDoorClosed();
        var db = doorBot.GetComponentInChildren<Door>(); if(db != null) db.SetDoorClosed();
        var dr = doorRight.GetComponentInChildren<Door>(); if(dr != null) dr.SetDoorClosed();
    }
    public void OpenDoors()
    {
        var doorLeft = currentRoom.transform.Find("DoorLeft");
        var doorTop = currentRoom.transform.Find("DoorTop");
        var doorRight = currentRoom.transform.Find("DoorRight");
        var doorBot = currentRoom.transform.Find("DoorBot");

        if (currentRoom.asLeft) doorLeft.transform.Find("DoorCollider").GetComponent<BoxCollider2D>().enabled = false;
        if (currentRoom.asTop) doorTop.transform.Find("DoorCollider").GetComponent<BoxCollider2D>().enabled = false;
        if (currentRoom.asBot) doorBot.transform.Find("DoorCollider").GetComponent<BoxCollider2D>().enabled = false;
        if (currentRoom.asRight) doorRight.transform.Find("DoorCollider").GetComponent<BoxCollider2D>().enabled = false;

        var dl = doorLeft.GetComponentInChildren<Door>(); if (dl != null) dl.SetDoorOpened();
        var dt = doorTop.GetComponentInChildren<Door>(); if (dt != null) dt.SetDoorOpened();
        var db = doorBot.GetComponentInChildren<Door>(); if (db != null) db.SetDoorOpened();
        var dr = doorRight.GetComponentInChildren<Door>(); if (dr != null) dr.SetDoorOpened();
    }
}
