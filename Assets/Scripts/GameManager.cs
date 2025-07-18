using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TMP_Text highScoreText;
    public TMP_Text timerText;
    public TMP_Text scoreText;

    public float gameDuration = 60f;
    private float timer;

    public int score = 0;
    private int highScore = 0;

    private bool gameEnded = false;

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    void Start()
    {
        timer = gameDuration;
        UpdateScoreText();
        UpdateTimerText();
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateHighScoreText();
    }

    void Update()
    {
        if (gameEnded) return;

        if (timer > 0)
        {
            timer -= Time.deltaTime;
            UpdateTimerText();
        }
        else
        {
            gameEnded = true;
            EndGame();
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    public void AdjustTime(float amount)
    {
        timer += amount;
        if (timer < 0) timer = 0;
        UpdateTimerText();
    }

    void EndGame()
    {
        Debug.Log("Game Over!");

        if (FindObjectOfType<PlayerMovement>() != null)
            FindObjectOfType<PlayerMovement>().enabled = false;
        if (FindObjectOfType<Weapon>() != null)
            FindObjectOfType<Weapon>().enabled = false;

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }

        // Oyuna en son hangi Level'de başladığımızı kaydet
        MainMenuManager.lastPlayedLevel = SceneManager.GetActiveScene().name;

        if (score >= 100)
        {
            SceneManager.LoadScene("WinScene");
        }
        else
        {
            SceneManager.LoadScene("LoseScene");
        }
    }


    void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score.ToString();
    }

    void UpdateTimerText()
    {
        if (timerText != null)
            timerText.text = Mathf.Ceil(timer).ToString();
    }

    void UpdateHighScoreText()
    {
        if (highScoreText != null)
            highScoreText.text = "High Score: " + highScore.ToString();
    }
}












