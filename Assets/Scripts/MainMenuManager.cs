using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Son oynanan level'in adını geçici olarak burada saklıyoruz
    public static string lastPlayedLevel = "";

    public void PlayGame()
    {
        lastPlayedLevel = "Level1";  // Oyuna ilk buradan başlıyoruz
        SceneManager.LoadScene(lastPlayedLevel);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
    public void Exit()
    {

        SceneManager.LoadScene("MainMenu");
    }
    public void Retry()
    {
        Debug.Log("Retry");

        if (!string.IsNullOrEmpty(lastPlayedLevel))
            SceneManager.LoadScene(lastPlayedLevel);
        else
            SceneManager.LoadScene("MainMenu"); // Güvenlik amaçlı fallback
    }

    public void NextLevel()
    {
        Debug.Log("Next Level");

        // WinScene'deysek bir sonraki level'e geç
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "WinScene")
        {
            // Mevcut oynanan level'in build index'ini bul
            int lastLevelIndex = SceneManager.GetSceneByName(lastPlayedLevel).buildIndex;

            int nextLevelIndex = lastLevelIndex + 2; // +1 WinScene, +1 sonraki level

            if (nextLevelIndex < SceneManager.sceneCountInBuildSettings)
            {
                string nextLevelName = SceneManager.GetSceneByBuildIndex(nextLevelIndex).name;
                lastPlayedLevel = nextLevelName; // Güncelle
                SceneManager.LoadScene(nextLevelName);
            }
            else
            {
                // Oyun bitti, menüye dön
                SceneManager.LoadScene("MainMenu");
            }
        }
        else
        {
            // Diğer sahnelerde sıradaki sahneye geç
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextSceneIndex);
            }
            else
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
    // class içinde:
    [SerializeField] GameObject optionsPanel;

    // Yeni public methodlar
    public void OpenOptions() { optionsPanel.SetActive(true); }
    public void CloseOptions() { optionsPanel.SetActive(false); }

}
