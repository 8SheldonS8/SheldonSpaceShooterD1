using UnityEngine;

public class Player : Ship
{
    public EnvironmentManager environmentManager;
    public GameManager gameManager;
    public float backgroundMultiplier = 0.008f;
    [Range(0, 3)]
    private int playerLives;

    void Update()
    {
        if (Input.GetAxis("Horizontal") < 0)
        {
            // Left
            transform.position = Vector2.Lerp(transform.position, transform.position + Vector3.left, speed * Time.deltaTime);
            environmentManager.SetMovingHorizontalMultiplier(backgroundMultiplier);
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            // Right
            transform.position = Vector2.Lerp(transform.position, transform.position + Vector3.right, speed * Time.deltaTime);
            environmentManager.SetMovingHorizontalMultiplier(-backgroundMultiplier);
        }
        else
        {
            environmentManager.SetMovingHorizontalMultiplier(0);
        }

        if (Input.GetAxis("Vertical") > 0)
        {
            // Forward
            transform.position = Vector2.Lerp(transform.position, transform.position + Vector3.up, speed * Time.deltaTime);
            environmentManager.SetMovingVerticallyMultiplier(-backgroundMultiplier);
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            // Backwards
            transform.position = Vector2.Lerp(transform.position, transform.position + Vector3.down, speed * Time.deltaTime);
            environmentManager.SetMovingVerticallyMultiplier(backgroundMultiplier);
        }
        else
        {
            environmentManager.SetMovingVerticallyMultiplier(0);
        }

        if (Input.GetAxis("Fire") > 0 && canShoot)
        {
            canShoot = false;
            ShootLaser();
        }
    }

    // LateUpdate is called every frame, if the Behaviour is enabled
    public void LateUpdate()
    {
        //Keep the player from going off screen
        Vector3 newPosition = transform.position;
        newPosition.x = Mathf.Clamp(newPosition.x, movementRangeMin.x, movementRangeMax.x);
        newPosition.y = Mathf.Clamp(newPosition.y, movementRangeMin.y, movementRangeMax.y);
        transform.Translate(newPosition - transform.position);
    }

    public override void CalculateHit(int amount)
    {
        int startHP = hitPoints;
        base.CalculateHit(amount);
        if (hitPoints < startHP)
        {
            gameManager.oUI.DecreasePlayerHealth();
        }
        if (hitPoints <= 0)
        {
            playerLives--;
            gameManager.oUI.DecreasePlayerLife();
            if (playerLives < 0)
            {
                gameManager.EndGame();
            }
            else
            {
                gameManager.oUI.ResetPlayerHealth();
                hitPoints = 30;
            }
        }
    }

    public void SetPlayerLives(int amount)
    {
        playerLives = amount;
    }
}
