using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class PlayerStats
{
    public static int money = 0;
    public static float maxHealth = 10;
    public static float playerHealth = 10;
    public static float defence = 1;
    public static int Schange = 4;

    public static PostProcessing postProcessing;

    public static PlayerController playerController;

    public static ParticleSystem Nuke;

    public static VehicleMovement vehicleMovement;

    public static VehicleStats vehicleStats = new VehicleStats();

    public static WeaponStats activeWeaponStats = new WeaponStats();

    public static void Reset()
    {
        money = 0;
        defence = 1;
        maxHealth = 10;
        playerHealth = 10;
        vehicleStats = new VehicleStats();
        activeWeaponStats = new WeaponStats();
    }

	public static void PlayerDamage(float damage)
	{
        if (vehicleMovement.spinningOut) return; // Spinning out causes invincibility

        playerHealth -= damage / defence;
		playerController.healthSlider.value = playerHealth;

        if (playerHealth <= 0)
        {
            
            playerController.StartCoroutine("OnDeath");
            
        }
    }

    public static void ChangeMoney(int amount)
	{
		money += amount;
		playerController.moneyText.text = $"${money}";
	}
}