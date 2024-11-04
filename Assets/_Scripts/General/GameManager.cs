using UnityEngine;
using static PlayerStats;
using static PlayerPopups;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool isPaused = false;
    public static bool inGarage = false;

    private void Start()
    {
        gameManager = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            Time.timeScale = isPaused ? 0 : 1;
            playerController.pauseMenu.SetActive(isPaused);
            playerController.UI.SetActive(!isPaused);
            playerController.garageMenu.SetActive(false);
            popupVisible = !isPaused;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void EndScene()
    {
        SceneManager.LoadScene("Gameover");
    }
}
