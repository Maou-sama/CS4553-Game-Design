using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager gm = null;

    private PlayerNew playerData;
    private GameObject player;

    [Header("Initialization")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform startPosition;

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
    }

    // Update is called once per frame
    void Update()
    {
        hp.text = "HP: " + playerData.hp;
        battery.text = "x " + playerData.Battery;
    }
}
