using UnityEngine;
using System;
using System.Collections;
using TMPro;

public class Weapon : MonoBehaviour
{
    //Shooting
    public bool isShooting, readyToShoot;
    bool allowReset = true;
    public float shootingDelay = 2f;

    //burst
    public int bulletsPerBurst = 3;
    public int burstBulletsLeft;

    //Spread
    public float spreadIntensity;

    //Bullet
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletVelocity = 30f;
    public float bulletPrefablifeTime = 3f;//seconds

    //Loading
    public float reloadTime;
    public int magezineSize, bulletsLeft;
    public bool isReloading;

    //Animation
    private Animator animator;
   
   
    //MuzzleEffect
    // public GameObject MuzzleFlash;

    public enum WeaponModel
    {
        Pistol1911,
        M16
    }
    public WeaponModel thisWeaponModel;
    public enum ShootingMode
    {
        Single,
        Burst,
        Aouto
    }

    public ShootingMode currentShootingMode;

    private void Awake()
    {
        readyToShoot = true;
        burstBulletsLeft = bulletsPerBurst;
        bulletsLeft = magezineSize;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(bulletsLeft == 0 && isShooting)
        {
            SoundManager.Instance.emptyMagazineSound1911.Play();
        }
        if (currentShootingMode == ShootingMode.Aouto)
        {
            //holding down left mouse button
            isShooting = Input.GetKey(KeyCode.Mouse0);
           
        }
        else if (currentShootingMode == ShootingMode.Single ||
                currentShootingMode == ShootingMode.Burst)
        {
            //clicking left mouse button once 
            isShooting = Input.GetKeyDown(KeyCode.Mouse0);
        }
        if (readyToShoot && isShooting && bulletsLeft > 0)
        {
            burstBulletsLeft = bulletsPerBurst;
            FireWeapon();
        }
        //if (AmmoManager.Instance.ammoDisplay != null)
        //{
        //    AmmoManager.Instance.ammoDisplay.text = $"{bulletsLeft / bulletsPerBurst}/{magezineSize / bulletsPerBurst}";
        //}
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magezineSize && isReloading == false)
        {
            Reload();
        }
        //ıf you want to automatically reload when magazine is empty
        if (Input.GetKeyDown(KeyCode.R) && readyToShoot && isShooting == false && isReloading == false && bulletsLeft <= 0)
        {
            Reload();
        }
        //Good try lkrnmsşlndz
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    WeaponManager.Instance.ChangeActiveWeapon();

        //}
    }

    private void Reload()
    {
        //reload sound
        animator.SetTrigger("RELOAD"); 
        SoundManager.Instance.PlayReloadSound(thisWeaponModel);
        isReloading = true;
        Invoke("ReloadCopmleted", reloadTime);
    }
    private void ReloadCopmleted()
    {
        bulletsLeft = magezineSize;
        isReloading = false;
    }
    private void FireWeapon()
    {
        //var ps = MuzzleFlash.GetComponent<ParticleSystem>();
        //ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear); // zorla durdur ve temizle
        //ps.Play(); // sonra yeniden başlat

        animator.SetTrigger("RECOIL");
        bulletsLeft--;
        SoundManager.Instance.PlayShootingSound(thisWeaponModel);

        readyToShoot = false;
        Vector3 shootingDirection = CalculaDirectionAndSpread().normalized;

        //initiate the bullet
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);

        //pointing the bullet to face the shooting direction
        bullet.transform.forward = shootingDirection;

        //shoot the bullet
        bullet.GetComponent<Rigidbody>().AddForce(shootingDirection * bulletVelocity, ForceMode.Impulse);

        //Destroy teh bullet after some time
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletPrefablifeTime));

        //checkinf if we are shooting 
        if (allowReset)
        {
            Invoke("ResetShoot", shootingDelay);
            allowReset = false;
        }

        //Burst Mode
        if (currentShootingMode == ShootingMode.Burst && burstBulletsLeft > 1)//we already shoot once before this check 
        {
            burstBulletsLeft--;
            Invoke("FireWeapon", shootingDelay);
        }


    }
    private void ResetShoot()
    {
        readyToShoot = true;
        allowReset = true;
    }

    public Vector3 CalculaDirectionAndSpread()
    {
        //shooting from the middle of the screen to check where we are pointing at 
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
        {
            //hitting something
            targetPoint = hit.point;
        }
        else
        {
            //shooting at the air 
            targetPoint = ray.GetPoint(100);
        }

        Vector3 direction = targetPoint - bulletSpawn.position;

        float x = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);
        float y = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);

        //returning shooting direction and  spread 
        return direction + new Vector3(x, y, 0);
    }
    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }
}
