using UnityEngine;
using PvZRI.Interaction;

[CreateAssetMenu(fileName = "Upgrade", menuName = "Upgrade", order = 1)]
public class Upgrade : ScriptableObject
{
    public int cost;
    public float rangeUpgrade;
    public float damageUpgrade;
    public float attackSpeedUpgrade;

    public void AddUpgrades()
    {
        SelectTower.instance.selected.range += rangeUpgrade;
        SelectTower.instance.selected.damage += damageUpgrade;
        SelectTower.instance.selected.timeBetweenAttacks += attackSpeedUpgrade;
    }
}
