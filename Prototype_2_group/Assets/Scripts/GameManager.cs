using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager gm = null;

    public GameObject playerPrefab;
    public GameObject player;
    private PlayerNew playerData;
    public Transform startPosition;
    public Text hp;
    public Text battery;

    public GameObject[] enemies;

    public bool invisible;

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
        player = Instantiate(playerPrefab, new Vector3(startPosition.position.x, startPosition.position.y, -1), Quaternion.identity);
        playerData = player.GetComponent<PlayerNew>();
        playerData.setSavePointPos(startPosition.position);
        invisible = false;
    }

    // Update is called once per frame
    void Update()
    {
        hp.text = "HP: " + playerData.hp;
        battery.text = "x " + playerData.Battery;

        if (invisible == true)
        {
            Debug.Log("Invisible");

            enemies = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<ControlEnemyTransparency>().enabled = true;
                //enemy.GetComponent<ControlEnemyTransparency>().startChanging = true;
            }
            invisible = false;
        }
    }
}
