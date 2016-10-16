using UnityEngine;
using System.Collections;

public class ShieldBonus : BonusItem
{
    // Awake is called when the script instance is being loaded
    public void Awake()
    {
        bonusColor = AllowedColors.yellow;
    }

    public override void ApplyBonus(GameObject oPlayer)
    {
        GameObject oShield = oPlayer.GetComponent<Ship>().shieldPrefab;
        oShield.SetActive(true);
        oShield.GetComponent<Shield>().RefreshShield();
    }
}
