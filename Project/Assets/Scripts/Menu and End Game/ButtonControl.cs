using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ButtonControl : MonoBehaviour
{
    [SerializeField] private GameObject option;
    public void OpenOption()
    {
        Time.timeScale = 1;
        option.SetActive(true);
    }

    public void CloseOption()
    {
        Time.timeScale = 1;
        option.SetActive(false);
    }

    public void OpenInGameOption()
    {
        Time.timeScale = 0;
        option.SetActive(true);
    }

    public void CloseInGameOption()
    {
        Time.timeScale = 1;
        option.SetActive(false);
    }

    public void goToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void gameStart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void exitGame()
    {
        Application.Quit();
    }

}
