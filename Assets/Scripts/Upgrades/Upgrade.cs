using UnityEngine;
using PvZRI.Interaction;
using PvZRI.Towers;
using System.Text;

[CreateAssetMenu(fileName = "Upgrade", menuName = "Upgrade", order = 1)]
public class Upgrade : ScriptableObject
{
    public int cost;
    public int killsNeeded;
    [Multiline]
    public string description;
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
    [Space]
    public Projectile changeProjectileTo;
    [Space]
    public float torchwoodDamageIncrese;

    public void AddUpgrades()
    {
        SunTracker sunTracker = SunTracker.instance;

        Tower towerToUpgrade = SelectTower.instance.selected;
        if (sunTracker.HaveEnoughSun(cost) && towerToUpgrade.killCount >= killsNeeded)
        {
            sunTracker.MinusSun(cost);
            towerToUpgrade.range += rangeUpgrade;
            towerToUpgrade.damage += damageUpgrade;
            towerToUpgrade.attackSpeed += attackSpeedUpgrade;
            towerToUpgrade.projectileHealth += projectileHealthUpgrade;
            towerToUpgrade.projectileSpeed += projectileSpeedUpgrade;
            towerToUpgrade.slowAmount += projectileSlowUpgrade;
            towerToUpgrade.slowTime += projectileSlowDurationUpgrade;
            towerToUpgrade.sunReward += sunRewardUpgrade;

            if (towerToUpgrade.name == "Torchwood")
            {
                TorchwoodScript t = towerToUpgrade.GetComponent<TorchwoodScript>();
                t.damageIncrease += torchwoodDamageIncrese;
            }

            if(changeProjectileTo != null)
            towerToUpgrade.projectile = changeProjectileTo;


            towerToUpgrade.sellValue += cost / 2;
            towerToUpgrade.path1Purchased++;
        }
        else
        {
            Debug.Log("not enough sun / kills");  
        }

        if (towerToUpgrade.killCount < killsNeeded)
        {
            sunTracker.FlashKillsText();
        }
    }

    public string GetShopInfoText()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append(cost + " Sun").AppendLine();
        builder.Append("Kills needed: " + killsNeeded).AppendLine();
        builder.Append(description);
        return builder.ToString();
    }
}
