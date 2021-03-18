using UnityEngine;
using PvZRI.Interaction;
using PvZRI.Towers;

[CreateAssetMenu(fileName = "Upgrade", menuName = "Upgrade", order = 1)]
public class Upgrade : ScriptableObject
{
    public int cost;
    [Range(1,2)]
    public int path;
    public float rangeUpgrade;
    public float damageUpgrade;
    public float attackSpeedUpgrade;
    public int projectileHealthUpgrade;

    public void AddUpgrades()
    {
        Tower towerToUpgrade = SelectTower.instance.selected;
        towerToUpgrade.range += rangeUpgrade;
        towerToUpgrade.damage += damageUpgrade;
        towerToUpgrade.timeBetweenAttacks += attackSpeedUpgrade;
        towerToUpgrade.projectileHealth += projectileHealthUpgrade;

        if(path == 1)
        {
            towerToUpgrade.path1Purchased++;
        }
        else
        {
            towerToUpgrade.path2Purchased++;
        }
    }
}
