using UnityEngine;

public class Ship : MonoBehaviour
{

    public int hitPoints = 30;
    public float fireDelay;
    public float speed;
    public bool enemyShip = false;

    public GameObject laserPrefab;
    public GameObject shieldPrefab;
    public float laserDistanceForward;
    public float laserDistanceRight;
    public Vector3 laserRotation;
    
    protected Vector3 movementRangeMin;
    protected Vector3 movementRangeMax;

    // Start is called just before any of the Update methods is called the first time
    public void Start()
    {
        //Find out the boundaries of the viewalbe screen
        SpriteRenderer renderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        Vector3 bottomLeftWorldCoordinates = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 topRightWorldCoordinates = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));

        movementRangeMin = bottomLeftWorldCoordinates + renderer.bounds.extents;
        movementRangeMax = topRightWorldCoordinates - renderer.bounds.extents;
    }

    protected bool canShoot = true;

    protected void ShootLaser()
    {
        if (enemyShip)
        {
            GameObject.Instantiate(laserPrefab, transform.position + Vector3.down * laserDistanceForward + Vector3.right * laserDistanceRight, Quaternion.Euler(laserRotation));
        }
        else
        {
            GameObject.Instantiate(laserPrefab, transform.position + Vector3.up * laserDistanceForward + Vector3.right * laserDistanceRight, Quaternion.Euler(laserRotation));
        }
        Invoke("EnableShoot", fireDelay);
    }

    protected void EnableShoot()
    {
        canShoot = true;
    }

    public virtual void CalculateHit(int amount)
    {
        hitPoints -= amount;
    }
}
