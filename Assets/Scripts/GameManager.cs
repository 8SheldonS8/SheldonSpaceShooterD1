using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GameManager : MonoBehaviour {
    [Header("Player")]
    public GameObject playerPrefab;
    [Range(0, 3)]
    public int initialLives;

    [Header("Spawning")]
    public bool spawnEnemies;
    public GameObject shieldBonus;
    public List<GameObject> enemyPrefabs = new List<GameObject>();
    public List<GameObject> spawnPoints = new List<GameObject>();
    public List<GameObject> bonusSpawnPoints = new List<GameObject>();
    public UserInterface oUI;
    public int countDown;

    private int _score;
    private bool isDelaySpawning = false;

	// Use this for initialization
	void Start () {
        isDelaySpawning = false;
        TogglePlayer(true);
        _score = 0;
        playerPrefab.GetComponent<Player>().hitPoints = 30;
        playerPrefab.GetComponent<Player>().SetPlayerLives(initialLives);
        playerPrefab.GetComponent<Transform>().position = Vector2.zero;

        if (spawnEnemies)
        {
            oUI.SetCountDownVisible(true);
            StartCoroutine(DelaySpawn());
        }
	}

    public void OnEnable()
    {
        Start();
    }

    public void IncrementScore()
    {
        _score++;
        oUI.AdjustScore(_score);
    }
	
	public void SpawnEnemy()
    {
        int enemy = UnityEngine.Random.Range(0, enemyPrefabs.Count);
        int spawnLocation = UnityEngine.Random.Range(0, spawnPoints.Count);

        if (spawnEnemies)
        {
            GameObject.Instantiate(enemyPrefabs[enemy], spawnPoints[spawnLocation].transform.position, Quaternion.identity);
        }

        Invoke("SpawnEnemy", UnityEngine.Random.Range(3, 10));
    }

    public void SpawnBonus()
    {
        int spawnLocation = UnityEngine.Random.Range(0, bonusSpawnPoints.Count);

        GameObject.Instantiate(shieldBonus, bonusSpawnPoints[spawnLocation].transform.position, Quaternion.identity);

        Invoke("SpawnBonus", UnityEngine.Random.Range(15, 25));
    }

    public void TogglePlayer(bool visibile)
    {
        playerPrefab.SetActive(visibile);
    }
    
    public void EndGame()
    {
        // spawnEnemies = false;
        TogglePlayer(false);

        CancelInvoke("SpawnEnemy");
        CancelInvoke("SpawnBonus");

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(enemy);
        }

        foreach (GameObject powerUp in GameObject.FindGameObjectsWithTag("PowerUp"))
        {
            Destroy(powerUp);
        }

        oUI.EndGame();
    }

    private IEnumerator DelaySpawn()
    {
        int loopCountown = countDown;

        while (loopCountown > -1)
        {
            oUI.AdjustCountDown(loopCountown > 0 ? loopCountown.ToString() : "GO!");
            loopCountown--;
            yield return new WaitForSeconds(1);
        }

        if (!isDelaySpawning)
        {
            oUI.SetCountDownVisible(false);
            isDelaySpawning = true;
            SpawnEnemy();
            Invoke("SpawnBonus", UnityEngine.Random.Range(12, 18));
        }
    }
}
