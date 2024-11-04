using UnityEngine;
using TMPro;
using static PlayerStats;
using static PlayerPopups;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;


public class PlayerController : MonoBehaviour
{
    public TextMeshProUGUI popupText;
    public TextMeshProUGUI moneyText;
    public GameObject garageMenu;
    
    public GameObject pauseMenu;
    public GameObject UI;
    public Slider healthSlider;

    public ParticleSystem deathParticles;

    void Start()
    {
        playerController = this;
        AssignPopup(popupText);
        ClearPopupText();
        Reset();
        moneyText.text = $"${money}";
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

    public void OnDeath()
    {
        deathParticles.transform.SetParent(null, true);
        deathParticles.Play();
        gameObject.SetActive(false);
    }
}
