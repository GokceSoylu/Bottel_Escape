using UnityEngine;
using System.Collections.Generic;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance { get; private set; }

    public List<GameObject> weaponSlots;
    public GameObject activeWeaponSlot;

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

    private void Start()
    {
        if (weaponSlots.Count > 0)
        {
            activeWeaponSlot = weaponSlots[0];
            UpdateWeaponSlots();
        }
    }

    private void Update()
    {
        // Sayısal tuşlarla silah seçimi (1, 2, 3, ...)
        for (int i = 0; i < weaponSlots.Count; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                SetWeapon(i);
            }
        }
    }

    public void SetWeapon(int index)
    {
        if (index >= 0 && index < weaponSlots.Count)
        {
            activeWeaponSlot = weaponSlots[index];
            UpdateWeaponSlots();
        }
    }

    public void UpdateWeaponSlots()
    {
        for (int i = 0; i < weaponSlots.Count; i++)
        {
            bool isActive = weaponSlots[i] == activeWeaponSlot;
            // Enable sadece görseli ve collider'ı (ya da silahın ateşleme kabiliyetini)
            weaponSlots[i].GetComponent<Weapon>().enabled = isActive;
            weaponSlots[i].transform.GetChild(0).gameObject.SetActive(isActive); // örnek olarak sadece model
        }
    }

    //public void UpdateWeaponSlots()
    //{
    //    foreach (GameObject weaponSlot in weaponSlots)
    //    {
    //        weaponSlot.SetActive(weaponSlot == activeWeaponSlot);
    //    }
    //}

}
