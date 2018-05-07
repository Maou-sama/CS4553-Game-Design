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
    private GameObject[] allDoors;
    private GameObject[] allKeys;

    private List<GameObject> doorToBeSaved;
    private List<GameObject> doorSaved;
    private List<GameObject> keyToBeSaved;
    private List<GameObject> keySaved;

    private Vector3 sprintBarPos;

    [Header("Initialization")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform startPosition;

    [Header("Text Field")]
    public Text saved;
    public Slider hp;
    public Slider battery;
    public Slider sprint;

    void Awake()
    {
        /*
			this is the standard way of setting up a singleton
			it will make sure there is only one of these objects and that it exists between scene loads
		*/
        if (gm == null)
        {
            //DontDestroyOnLoad(this);
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
        allKeys = GameObject.FindGameObjectsWithTag("Key");

        keyToBeSaved = new List<GameObject>();
        keySaved = new List<GameObject>();
        doorToBeSaved = new List<GameObject>();
        doorSaved = new List<GameObject>();

        sprint.maxValue = playerData.MaxStamina;
        
        battery.maxValue = playerData.Battery;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerData.MaxHP <= 0)
        {
            SceneManager.LoadScene(2);
        }

        hp.maxValue = playerData.MaxHP;
        hp.value = playerData.hp;
        battery.value = playerData.Battery;
        sprint.value = playerData.stamina;

        sprintBarPos = Camera.main.WorldToScreenPoint(player.transform.position + new Vector3(0, 1, 0));
        sprint.transform.position = sprintBarPos;
    }

    public void ShowSaveText()
    {
        saved.gameObject.SetActive(true);
        StartCoroutine(DisableSaveText());
    }

    private IEnumerator DisableSaveText()
    {
        yield return new WaitForSeconds(1);
        saved.gameObject.SetActive(false);
    }

    public void AddKeyToBeSaved(GameObject key)
    {
        keyToBeSaved.Add(key);
    }

    public void SaveKey()
    {
        foreach (GameObject key in keyToBeSaved)
        {
            keySaved.Add(key);
        }
        keyToBeSaved = new List<GameObject>();
    }

    public void ResetKey()
    {
        foreach (GameObject key in allKeys)
        {
            if (!keySaved.Contains(key))
            {
                key.SetActive(true);
            }
        }
        keyToBeSaved = new List<GameObject>();
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
