using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class InputController : MonoBehaviour
{
    public static InputController ic = null;

    public bool useJoyStick;

    [SerializeField] private GameObject keyBoardInputs;
    [SerializeField] private GameObject joyStickInputs;

    void Awake()
    {
        /*
			this is the standard way of setting up a singleton
			it will make sure there is only one of these objects and that it exists between scene loads
		*/
        if (ic == null)
        {
            DontDestroyOnLoad(this);
            ic = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void ChangeInput()
    {
        useJoyStick = !useJoyStick;
    }

    void OnEnable()
    {
        Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.buildIndex == 1)
        {
            if (useJoyStick)
            {
                Instantiate(joyStickInputs);
            }
            else
            {
                Instantiate(keyBoardInputs);
            }
        }
    }

    // called when the game is terminated
    void OnDisable()
    {
        Debug.Log("OnDisable");
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
