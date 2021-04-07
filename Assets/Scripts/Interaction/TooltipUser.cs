using PvZRI.Interaction;
using PvZRI.Towers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipUser : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Tower tower;
    [SerializeField] Upgrade upgrade;
    [SerializeField] Tooltip tooltip;

    public enum tooltipType { shop, upgrade1, upgrade2, upgrade3, upgrade4 };
    public tooltipType type;    

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (type.ToString() == "shop")
        {
            tooltip.DisplayShopInfo(tower);
        }
        else
        {
            switch (type.ToString())
            {
                default:
                    upgrade = SelectTower.instance.selected.upgradePath1[0];
                    tooltip.DisplayUpgradeInfo(upgrade);
                    break;

                case "upgrade2":
                    upgrade = SelectTower.instance.selected.upgradePath1[1];
                    tooltip.DisplayUpgradeInfo(upgrade);
                    break;

                case "upgrade3":
                    upgrade = SelectTower.instance.selected.upgradePath1[2];
                    tooltip.DisplayUpgradeInfo(upgrade);
                    break;

                case "upgrade4":
                    upgrade = SelectTower.instance.selected.upgradePath1[3];
                    tooltip.DisplayUpgradeInfo(upgrade);
                    break;
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.HideInfo();
    }
}
