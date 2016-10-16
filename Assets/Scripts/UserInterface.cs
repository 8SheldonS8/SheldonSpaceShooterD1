using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UserInterface : MonoBehaviour
{

    public Text scoreText;
    public GameObject mainScreen;
    public GameObject gameScreen;
    public GameObject gameManager;
    public Text playButtonText;
    public Text finalScoreText;
    public Text countDownText;

    private List<GameObject> lifeIndicator = new List<GameObject>();

    // Awake is called when the script instance is being loaded
    public void Awake()
    {
        foreach (Transform life in gameScreen.transform.FindChild("Lives").transform)
        {
            lifeIndicator.Add(life.gameObject);
        }
    }

    public void AdjustCountDown(string countDownDisplay)
    {
        countDownText.text = countDownDisplay;
    }

    public void AdjustScore(int amount)
    {
        scoreText.text = string.Format("Score: {0}", amount);
        finalScoreText.text = string.Format("Final Score: {0}", amount);
    }

    public void ResetPlayerHealth()
    {
        foreach (Transform heart in gameScreen.transform.FindChild("Health").transform)
        {
            heart.gameObject.SetActive(true);
        }
    }

    public void ResetPlayerLives(int lives)
    {
        for (int i = 0; i < lives; i++)
        {
            lifeIndicator[i].SetActive(true);
        }
    }

    public void DecreasePlayerHealth()
    {
        foreach (Transform heart in gameScreen.transform.FindChild("Health").transform)
        {
            if (heart.gameObject.activeSelf)
            {
                heart.gameObject.SetActive(false);
                break;
            }
        }
    }

    public void DecreasePlayerLife()
    {
        foreach (Transform life in gameScreen.transform.FindChild("Lives").transform)
        {
            if (life.gameObject.activeSelf)
            {
                life.gameObject.SetActive(false);
                break;
            }
        }
    }

    public void Start_OnClick()
    {
        gameManager.GetComponent<GameManager>().spawnEnemies = true;
        gameManager.SetActive(true);

        AdjustScore(0);
        ResetPlayerLives(gameManager.GetComponent<GameManager>().initialLives);
        ResetPlayerHealth();

        mainScreen.SetActive(false);
        gameScreen.SetActive(true);
    }

    public void EndGame()
    {
        gameManager.GetComponent<GameManager>().spawnEnemies = true;
        gameManager.SetActive(false);
        playButtonText.text = "Retry";
        mainScreen.SetActive(true);
        gameScreen.SetActive(false);
    }

    public void SetCountDownVisible(bool isVisible)
    {
        countDownText.gameObject.SetActive(isVisible);
    }
}
