using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject levelSelection;
    [SerializeField] GameObject optionMenu;

    public void OnStart()
    {
        mainMenu.SetActive(false);
        levelSelection.SetActive(true);
    }

    public void OnBackOption()
    {
        optionMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void OnBackLS()
    {
        levelSelection.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void OnQuit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void OnOptions()
    {
        mainMenu.SetActive(false);
        optionMenu.SetActive(true);
    }

    public void OnLevel1()
    {
        SceneManager.LoadScene("Level01");
        mainMenu.SetActive(false);

        Time.timeScale = 1;
    }

    public void OnLevel2()
    {
        SceneManager.LoadScene("Level02");
        mainMenu.SetActive(false);

        Time.timeScale = 1;
    }
}
