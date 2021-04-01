using UnityEngine;
using PvZRI.Interaction;
using PvZRI.Towers;

[CreateAssetMenu(fileName = "Upgrade", menuName = "Upgrade", order = 1)]
public class Upgrade : ScriptableObject
{
    public int cost;
    [Space]
    public float rangeUpgrade;
    public float damageUpgrade;
    public float attackSpeedUpgrade;
    [Space]
    public float projectileSpeedUpgrade;
    [Space]
    public float projectileSlowUpgrade;
    public float projectileSlowDurationUpgrade;
    [Space]
    public int projectileHealthUpgrade;
    [Space]
    public int sunRewardUpgrade;

    public void AddUpgrades()
    {
        SunTracker sunTracker = SunTracker.instance;
        if (sunTracker.HaveEnoughSun(cost))
        {
            sunTracker.MinusSun(cost);
            Tower towerToUpgrade = SelectTower.instance.selected;
            towerToUpgrade.range += rangeUpgrade;
            towerToUpgrade.damage += damageUpgrade;
            towerToUpgrade.timeBetweenAttacks += attackSpeedUpgrade;
            towerToUpgrade.projectileHealth += projectileHealthUpgrade;
            towerToUpgrade.projectileSpeed += projectileSpeedUpgrade;
            towerToUpgrade.slowAmount += projectileSlowUpgrade;
            towerToUpgrade.slowTime += projectileSlowDurationUpgrade;
            towerToUpgrade.sunReward += sunRewardUpgrade;

            towerToUpgrade.path1Purchased++;
        }
        else
        {
            Debug.Log("not enough sun");  
        }
    }
}
