using System;

[Serializable]
public class EnemyStats
{
    public float health ;
    public float maxHealth = 5;
    public float damage = 1f;
    public float damageTime = 1f;
    public float speed = 100f;
    public int moneyReward = 5;
    public int rewardVariance = 1;
    private static System.Random random = new System.Random();

    public EnemyStats()
    {
        health = 3f;
        damage = 1f;
        damageTime = 1f;
        speed = 100f;
        moneyReward = 5;
        rewardVariance = 1;
    }

    public int GetReward()
    {
        return moneyReward + random.Next(-rewardVariance, rewardVariance);
    }
}
