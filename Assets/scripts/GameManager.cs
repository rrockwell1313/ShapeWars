using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public ScoreManager scoreManager;
    public CurrencyManager currencyManager;
    public AudioManager audioManager;
    public float gameTime = 120f;
    public TextMeshProUGUI timerText;
    public GameObject player1VictoryText;
    public GameObject player2VictoryText;
    public GameObject tryAgainButton;
    public bool gameEnded;

    private float currentTime;

    void Start()
    {
        currentTime = gameTime;
        player1VictoryText.SetActive(false);
        player2VictoryText.SetActive(false);
        tryAgainButton.SetActive(false);
    }

    void Update()
    {
        if (gameEnded) return;

        currentTime -= Time.deltaTime;
        timerText.text = Mathf.Clamp(currentTime, 0, gameTime).ToString("F0");

        if (currencyManager.currency1 == 0 && GameObject.FindGameObjectsWithTag("Player").Length == 0 ||
            currencyManager.currency2 == 0 && GameObject.FindGameObjectsWithTag("Player2").Length == 0 ||
            currentTime <= 0)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        gameEnded = true;
        Time.timeScale = 0f;

        if (currentTime <= 0 || currencyManager.currency1 == 0 || currencyManager.currency2 == 0)
        {
            if (scoreManager.player1Score > scoreManager.player2Score)
            {
                player1VictoryText.SetActive(true);
            }
            else if (scoreManager.player1Score < scoreManager.player2Score)
            {
                player2VictoryText.SetActive(true);
            }
            else
            {
                // Draw scenario, display draw message or any other action you'd like to take
            }
        }

        tryAgainButton.SetActive(true);
        audioManager.SetGameOver();

    }

    public void TryAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}


