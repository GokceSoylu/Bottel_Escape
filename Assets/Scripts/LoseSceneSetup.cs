using UnityEngine;

public class LoseSceneSetup : MonoBehaviour
{
    void Start()
    {
        // Oyuncu kaybettikten sonra zaman durmuş olabilir, düzelt.
        Time.timeScale = 1f;
    }
}
