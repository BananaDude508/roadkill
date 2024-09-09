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
        playerController.garageMenu.SetActive(true);
        vehicleMovement.rigidbody2d.velocity = Vector3.zero;
    }

    public void CloseGarageMenu()
    {
        Time.timeScale = 1f;
        playerController.garageMenu.SetActive(false);
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

    public void BuyUpgrade(string upgradeName)
    {
        switch (upgradeName)
        {
            case "speed":
                if (money <= 150) break;
                vehicleStats.maxSpeed += 0.5f;
                print($"Upgraded maxSpeed ({vehicleStats.maxSpeed})");
                break;

            case "acceleration":
                if (money <= 200) break;
                vehicleStats.acceleration += 1f;
                print($"Upgraded acceleration ({vehicleStats.acceleration})");
                break;

            case "armor":
                if (money <= 250) break;
                vehicleStats.defence += 0.5f;
                print($"Upgraded defence ({vehicleStats.defence})");
                break;

            case "vehicleattack":
                if (money <= 250) break;
                vehicleStats.driveDamage += 0.5f;
                print($"Upgraded driveDamage ({vehicleStats.driveDamage})");
                break;

            case "vehiclespecialattack":
                if (money <= 250) break;
                vehicleStats.driftDamageMod += 0.1f;
                vehicleStats.driftBodyRotation += 2.5f;
                print($"Upgraded driftDamageMod ({vehicleStats.driftDamageMod})");
                print($"Upgraded driftBodyRotation ({vehicleStats.driftBodyRotation})");
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
