// LevelPlayerSpawner.cs  ‑‑ GameManager’den tamamen bağımsız
using UnityEngine;

public class LevelPlayerSpawner : MonoBehaviour
{
    public GameObject playerFirstPrefab;
    public GameObject playerThirdPrefab;
    public Transform spawnPoint;   // Opsiyonel

    void Start()
    {
        GameObject prefab = GameSettings.cameraMode == CameraMode.FirstPerson
                          ? playerFirstPrefab
                          : playerThirdPrefab;

        Instantiate(prefab,
                    spawnPoint ? spawnPoint.position : Vector3.zero,
                    spawnPoint ? spawnPoint.rotation : Quaternion.identity);
    }
}
