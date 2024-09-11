using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static PlayerStats;
using static PlayerPopups;

public class GameManager : MonoBehaviour
{
    public static bool isPaused = false;

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            Time.timeScale = isPaused ? 0 : 1;
            playerController.pauseMenu.SetActive(isPaused);
            popupVisible = !isPaused;
        }
    }
}
