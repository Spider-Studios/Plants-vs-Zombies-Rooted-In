using UnityEngine;
using PvZRI.Interaction;
using PvZRI.Towers;

[CreateAssetMenu(fileName = "Upgrade", menuName = "Upgrade", order = 1)]
public class Upgrade : ScriptableObject
{
    public int cost;
    //[Range(1,2)]
    //public int path;
    public float rangeUpgrade;
    public float damageUpgrade;
    public float attackSpeedUpgrade;
    public float projectileSpeedUpgrade;
    public int projectileHealthUpgrade;

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

            towerToUpgrade.path1Purchased++;
        }
        else
        {
            Debug.Log("not enough sun");  
        }
    }
}
