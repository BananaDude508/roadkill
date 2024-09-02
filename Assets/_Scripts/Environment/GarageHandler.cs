using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerStats;
using static PlayerPopups;
public class GarageHandler : MonoBehaviour
{
    public bool playerNearby = false;
    public bool inGarage = false;

    public void OpenGarageMenu()
    {
        Time.timeScale = 0f;
    }

    public void CloseGarageMenu()
    {
        Time.timeScale = 1f;
    }

    public void Update()
    {
        if (!playerNearby) return;
        if (!Input.GetKeyDown(KeyCode.E)) return;

        if (inGarage)
        {
            SetPopupText("Press 'e' to open the garage");
            CloseGarageMenu();
            inGarage = false;
            return;
        }

        SetPopupText("Press 'e' to close the garage");
        OpenGarageMenu();
        inGarage = true;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.tag);
        if (!collision.CompareTag("Player")) return;
        SetPopupText("Press 'e' to open the garage");
        playerNearby = true;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        print(collision.tag);
        if (!collision.CompareTag("Player")) return;
        ClearPopupText();
        playerNearby = false;
    }
}
