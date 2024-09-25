using UnityEngine;
using TMPro;
using static PlayerStats;
using static PlayerPopups;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    public TextMeshProUGUI popupText;
    public TextMeshProUGUI moneyText;
    public GameObject garageMenu;
    public GameObject pauseMenu;
    public GameObject UI;
    public Slider healthSlider;

    // Start is called before the first frame update
    void Awake()
    {
        playerController = this;
        InitPopup(popupText);
        ClearPopupText();
        moneyText.text = $"${money}";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HurtEnemy(BasicEnemyController enemy, bool isDrifting = false, float speedRatio = 1)
    {
        enemy.EnemyDamage(vehicleStats.driveDamage * speedRatio * (isDrifting ? vehicleStats.driftDamageMod : 1));
    }

    public void UpdateHealthSliderLimits()
    {
        healthSlider.minValue = 0;
        healthSlider.maxValue = maxHealth;
    }
}
