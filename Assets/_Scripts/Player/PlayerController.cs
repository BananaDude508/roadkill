using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static PlayerStats;
using static PlayerPopups;


public class PlayerController : MonoBehaviour
{
    public TextMeshProUGUI popupText;
    public GameObject garageMenu;


    // Start is called before the first frame update
    void Awake()
    {
        playerController = this;
        InitPopup(popupText);
        ClearPopupText();
    }

    // Update is called once per frame
    void Update()
    {
        print(playerHealth);
    }

    public void HurtEnemy(BasicEnemyController enemy, bool isDrifting = false, float speedRatio = 1)
    {
        enemy.TakeDamage(vehicleStats.driveDamage * speedRatio * (isDrifting ? vehicleStats.driftDamageMod : 1));
    }
}
