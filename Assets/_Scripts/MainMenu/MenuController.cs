using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject menuObject;
    public GameObject settingsObject;

	public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void OpenSettings()
    {
		menuObject.SetActive(false);
		settingsObject.SetActive(true);
	}

    public void CloseSettings()
    {
        menuObject.SetActive(true);
        settingsObject.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
