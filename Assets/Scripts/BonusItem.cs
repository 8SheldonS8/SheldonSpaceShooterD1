using UnityEngine;
using System.Collections;

public enum AllowedColors
{
    blue, cyan, green, magenta, red, yellow
}
public class BonusItem : MonoBehaviour
{
    public float speed;
    
    public AllowedColors bonusColor;

    // Start is called just before any of the Update methods is called the first time
    public void Start()
    {
        SetMyColor();
    }

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, transform.transform.position + Vector3.down, speed * Time.deltaTime);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ApplyBonus(collision.gameObject);
        }
        Destroy(gameObject);
    }

    public virtual void ApplyBonus(GameObject oPlayer)
    {
        
    }

    void SetMyColor()
    {
        switch(bonusColor)
        {
            case AllowedColors.blue:
                gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                break;
            case AllowedColors.cyan:
                gameObject.GetComponent<SpriteRenderer>().color = Color.cyan;
                break;
            case AllowedColors.green:
                gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                break;
            case AllowedColors.magenta:
                gameObject.GetComponent<SpriteRenderer>().color = Color.magenta;
                break;
            case AllowedColors.red:
                gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                break;
            case AllowedColors.yellow:
                gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
                break;
            default:
                gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                break;
        }
    }
}
