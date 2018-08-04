﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponManager : MonoBehaviour
{
    private Dictionary<int, Weapon> m_Weapons;

    void Start ()
    {
        m_Weapons = new Dictionary<int, Weapon> ();
        foreach (Weapon w in GetComponentsInChildren<Weapon> ())
        {
            m_Weapons.Add (w.GetWeaponType (), w);
        }
    }

    public void Fire (int weaponType, float numberOfShots, float sizeModifier, Vector3 target)
    {
        if (m_Weapons.ContainsKey (weaponType))
        {
            m_Weapons[weaponType].Fire (numberOfShots, sizeModifier, target);
        }
        else
        {
            Debug.Assert (false, "Wrong weapon Type");
        }
    }
}

