using UnityEngine;
using static Weapon;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; set; }

    public AudioSource relodingSoundP1911;
    public AudioSource relodingSoundM16;
    public AudioSource emptyMagazineSound1911;

    public AudioSource ShootingChannel;
    public AudioClip M16Shot;
    public AudioClip P1911Shot;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void PlayShootingSound(WeaponModel weapon)
    {
        switch (weapon)
        {
            case WeaponModel.Pistol1911:
                ShootingChannel.PlayOneShot(P1911Shot );
                break;
            case WeaponModel.M16:
                ShootingChannel.PlayOneShot(M16Shot );
                break;

        }
    }
    public void PlayReloadSound(WeaponModel weapon)
    {
        switch (weapon)
        {
            case WeaponModel.Pistol1911:
                relodingSoundP1911.Play();
                break;
            case WeaponModel.M16:
                relodingSoundM16.Play();
                break;
        }
    }
}
