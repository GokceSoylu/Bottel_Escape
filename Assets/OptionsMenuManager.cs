using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuManager : MonoBehaviour
{
    [Header("Panel")]
    [SerializeField] GameObject optionsPanel;

    [Header("Camera Toggles")]
    [SerializeField] Toggle firstPersonToggle;
    [SerializeField] Toggle thirdPersonToggle;

    [Header("Weapon Toggles")]
    [SerializeField] Toggle handgunToggle;
    [SerializeField] Toggle rifleToggle;

    void Start()
    {
        // Varsa eski tercihleri yükle
        GameSettings.Load();

        // Toggle’ları senkronize et
        firstPersonToggle.isOn = GameSettings.cameraMode == CameraMode.FirstPerson;
        thirdPersonToggle.isOn = GameSettings.cameraMode == CameraMode.ThirdPerson;
        handgunToggle.isOn = GameSettings.weaponType == WeaponType.Handgun;
        rifleToggle.isOn = GameSettings.weaponType == WeaponType.Rifle;
    }

    // === Toggle Event’leri ===
    public void OnFirstPerson(bool v) { if (v) GameSettings.cameraMode = CameraMode.FirstPerson; }
    public void OnThirdPerson(bool v) { if (v) GameSettings.cameraMode = CameraMode.ThirdPerson; }
    public void OnHandgun(bool v) { if (v) GameSettings.weaponType = WeaponType.Handgun; }
    public void OnRifle(bool v) { if (v) GameSettings.weaponType = WeaponType.Rifle; }

    // === Panel Aç‑Kapa ===
    public void OpenOptions() { optionsPanel.SetActive(true); }
    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
        GameSettings.Save();          // Tercihleri sakla (opsiyonel)
    }
}
