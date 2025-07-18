using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GlobalReferances : MonoBehaviour
{
    public static GlobalReferances Instance { get; set; }

    public GameObject bulletImpactEffectPrefab;


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

}
