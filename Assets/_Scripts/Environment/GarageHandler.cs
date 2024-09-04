using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerStats;
using static PlayerPopups;


public class GarageHandler : MonoBehaviour
{
    public bool playerNearby = false;
    public bool inGarage = false;

    public string[] upgradeNames;

    public void OpenGarageMenu()
    {
        Time.timeScale = 0f;
        vehicleMovement.garageMenu.SetActive(true);
        vehicleMovement.rigidbody2d.velocity = Vector3.zero;
    }

    public void CloseGarageMenu()
    {
        Time.timeScale = 1f;
        vehicleMovement.garageMenu.SetActive(false);
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

    public void BuyUpgrade(string upgradeName, int price)
    {
        if (money <= price) return;

        switch (upgradeName)
        {
            case "speed":
                vehicleStats.maxSpeed += 0.5f;
                break;

            case "acceleration":
                vehicleStats.acceleration += 1f;
                break;

            case "armor":
                vehicleStats.defence += 0.5f;
                break;

            case "vehicleattack":
                vehicleStats.driveDamage += 0.5f;
                break;

            case "vehiclespecialattack":
                vehicleStats.driftDamageMod += 0.1f;
                vehicleStats.driftBodyRotation += 2.5f;
                break;

            default:
                Debug.LogError(upgradeName + " is not a valid upgrade");
                break;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        SetPopupText("Press 'e' to open the garage");
        playerNearby = true;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        ClearPopupText();
        playerNearby = false;
    }
}
