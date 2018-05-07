using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ButtonControl : MonoBehaviour
{
    [SerializeField] private GameObject option;
    public void OpenOption()
    {
        option.SetActive(true);
    }

    public void CloseOption()
    {
        option.SetActive(false);
    }

    public void goToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void gameStart()
    {
        SceneManager.LoadScene(1);
    }

    public void exitGame()
    {
        Application.Quit();
    }

}
