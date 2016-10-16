using UnityEngine;
using System.Collections;

public class Enemy : Ship
{

    private GameObject player;
    private float distanceToPlayer = 3f;
    private GameManager gameManager;

    private Vector2 targetDestination;

    void Awake()
    {
        player = GameObject.FindObjectOfType<Player>().gameObject;
        gameManager = GameObject.FindObjectOfType<GameManager>();

        GetNextPosition();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector2.Lerp(transform.position, targetDestination, speed * Time.deltaTime);

        if (canShoot)
        {
            canShoot = false;
            ShootLaser();
        }
    }

    // LateUpdate is called every frame, if the Behaviour is enabled
    public void LateUpdate()
    {
        //Keep the Enemy from going off screen
        Vector3 newPosition = transform.position;
        newPosition.x = Mathf.Clamp(newPosition.x, movementRangeMin.x, movementRangeMax.x);
        //newPosition.y = Mathf.Clamp(newPosition.y, movementRangeMin.y, movementRangeMax.y);
        transform.Translate(newPosition - transform.position);
    }

    private void GetNextPosition()
    {
        targetDestination = player.transform.position + Vector3.up * distanceToPlayer + Random.insideUnitSphere * 1;

        Invoke("GetNextPosition", .5f);
    }

    public override void CalculateHit(int amount)
    {
        base.CalculateHit(amount);

        if (hitPoints <= 0)
        {
            gameManager.IncrementScore();
            Destroy(this.gameObject);
        }
    }
}
