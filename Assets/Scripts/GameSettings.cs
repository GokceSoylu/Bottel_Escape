using UnityEngine;

public enum CameraMode { FirstPerson, ThirdPerson }
public enum WeaponType { Handgun, Rifle }

public static class GameSettings
{
    public static CameraMode cameraMode = CameraMode.ThirdPerson;
    public static WeaponType weaponType = WeaponType.Rifle;

    // İstersen oyuncu tercihlerini kaydet:
    public static void Save()
    {
        PlayerPrefs.SetInt("CameraMode", (int)cameraMode);
        PlayerPrefs.SetInt("WeaponType", (int)weaponType);
    }
    public static void Load()
    {
        cameraMode = (CameraMode)PlayerPrefs.GetInt("CameraMode", (int)cameraMode);
        weaponType = (WeaponType)PlayerPrefs.GetInt("WeaponType", (int)weaponType);
    }
}
