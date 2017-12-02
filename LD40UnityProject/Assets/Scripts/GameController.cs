using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public GameObject roomPrefab;

    public int width = 5;
    public int height = 5;
    public int offset = 10;
    public Room[,] map;
    public List<GameObject> rooms;

    void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(this);
    }

    void Start()
    {
        GenerateMap();
    }

    void GenerateMap()
    {
        map = new Room[width, height];
        var mapObject = GameObject.Find("Map");
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                var go = Instantiate(roomPrefab);
                go.transform.parent = mapObject.transform;
                go.transform.position = new Vector3(i * offset, j * offset);
                map[i, j] = go.GetComponent<Room>();
                rooms.Add(go);
            }
        }
    }
}
