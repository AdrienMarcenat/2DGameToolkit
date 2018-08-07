using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health m_Health;
    private float m_Fraction = 1f;

    private void Awake ()
    {
        this.RegisterAsListener (transform.parent.name, typeof (DamageGameEvent));
    }

    private void OnDestroy ()
    {
        this.UnregisterAsListener (transform.parent.name);
    }

    public void OnGameEvent (DamageGameEvent damageEvent)
    {
        m_Fraction = Mathf.Clamp01 (m_Health.GetCurrentHealth () / m_Health.GetTotalHealth ());
        transform.localScale = new Vector3 (m_Fraction, transform.localScale.y, transform.localScale.z);
    }
}
