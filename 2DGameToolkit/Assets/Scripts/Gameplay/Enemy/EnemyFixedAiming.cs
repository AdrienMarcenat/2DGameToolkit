using UnityEngine;
using System.Collections;

public class EnemyFixedAiming : EnemyAI
{
    protected override void Fire ()
    {
        m_WeaponManager.Fire (0, m_FireSalveNumber, m_SizeModifier, m_ShootDirection.position);
    }
}

