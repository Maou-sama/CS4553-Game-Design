using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager gm = null;

    private PlayerNew playerData;
    private GameObject player;

    private List<GameObject> doorToBeSaved;
    private List<GameObject> doorSaved;

    [Header("Initialization")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform startPosition;
    [SerializeField] GameObject[] allDoors;

    [Header("Text Field")]
    public Text hp;
    public Text battery;

    void Awake()
    {
        /*
			this is the standard way of setting up a singleton
			it will make sure there is only one of these objects and that it exists between scene loads
		*/
        if (gm == null)
        {
            DontDestroyOnLoad(this);
            gm = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {
        //Clone player and get the PlayerNew script and record the spawnPoint
        player = Instantiate(playerPrefab, new Vector3(startPosition.position.x, startPosition.position.y, -1), Quaternion.identity);
        playerData = player.GetComponent<PlayerNew>();
        playerData.setSavePointPos(startPosition.position);
        allDoors = GameObject.FindGameObjectsWithTag("Door");
        doorToBeSaved = new List<GameObject>();
        doorSaved = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        hp.text = "HP: " + playerData.hp;
        battery.text = "x " + playerData.Battery;
    }

    public void AddDoorToBeSaved(GameObject door)
    {
        doorToBeSaved.Add(door);
    }

    public void SaveDoor()
    {
        foreach(GameObject door in doorToBeSaved)
        {
            doorSaved.Add(door);
        }
        doorToBeSaved = new List<GameObject>();
    }

    public void ResetDoor()
    {
        foreach(GameObject door in allDoors)
        {
            if (!doorSaved.Contains(door))
            {
                door.SetActive(true);
            }
        }
        doorToBeSaved = new List<GameObject>();
    }
}
