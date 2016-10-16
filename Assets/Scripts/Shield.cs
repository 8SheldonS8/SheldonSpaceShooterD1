using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour
{
    public int initialHitPoints = 30;

    private int hitPoints;
    private SpriteRenderer spriteRender;
    private Color shieldYellow = new Color(Color.yellow.r, Color.yellow.g, Color.yellow.b, .6f);
    private Color shieldOrange = new Color(1, .3f, .1f, .4f);
    private Color shieldRed = new Color(Color.red.r, Color.red.g, Color.red.b, .3f);
    
    public void RefreshShield()
    {
        spriteRender = GetComponentInChildren<SpriteRenderer>();
        if (hitPoints < initialHitPoints)
        {
            hitPoints = initialHitPoints;
        }
        spriteRender.color = shieldYellow;
    }

    public virtual void CalculateHit(int amount)
    {
        hitPoints -= amount;
        spriteRender.color = DetermineShieldColor();
        if (hitPoints <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private Color DetermineShieldColor()
    {
        if(hitPoints < initialHitPoints && hitPoints > 10)
        {
            return shieldOrange;
        }
        if(hitPoints <= 10)
        {
            return shieldRed;
        }
        return shieldYellow;
    }
}
