using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLibrary : MonoBehaviour
{
    public static WeaponData[] weaponLibrary;
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        weaponLibrary = gameObject.GetComponentsInChildren<WeaponData>();
    }
    /* Weapondata order
     * 0 - Slinky
     * 1 - Blowout
     */
}
