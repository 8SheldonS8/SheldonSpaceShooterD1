using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour
{
    public int damage = 10;
    public bool enemyWeapon;

    // Update is called once per frame
    void Update()
    {
        if (enemyWeapon)
        {
            transform.position = Vector2.Lerp(transform.position, transform.position += Vector3.down, 6.5f * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.Lerp(transform.position, transform.position += Vector3.up, 6.5f * Time.deltaTime);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (!enemyWeapon && collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().CalculateHit(damage);
        }

        if (enemyWeapon && collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().CalculateHit(damage);
        }

        Destroy(this.gameObject);
    }

    // OnTriggerEnter2D is called when the Collider2D other enters the trigger (2D physics only)
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (enemyWeapon && collision.gameObject.tag == "Shield")
        {
            collision.gameObject.GetComponent<Shield>().CalculateHit(damage);
            Destroy(this.gameObject);
        }
    }
}
