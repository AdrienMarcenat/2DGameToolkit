using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent (typeof (AudioLowPassFilter))]
public class SoundFilterOnEvent : MonoBehaviour
{
    [SerializeField] private float m_TimeInSeconds = 0.5f;
    [SerializeField] private float m_LowCutoffFrequency = 1000f;
    [SerializeField] private float m_HighCutoffFrequency = 22000f;
    private AudioLowPassFilter m_AudioLowPassFilter;

    void Awake ()
    {
        m_AudioLowPassFilter = GetComponent<AudioLowPassFilter> ();
        this.RegisterAsListener("Player", typeof(DamageGameEvent), typeof(GameOverGameEvent));
    }

    void OnDestroy ()
    {
        this.UnregisterAsListener ("Player");
    }

    public void OnGameEvent (DamageGameEvent damageGameEvent)
    {
        if (damageGameEvent.GetDamage() > 0)
        {
            StopAllCoroutines();
            StartCoroutine(LowPassRoutine());
        }
    }
    public void OnGameEvent(GameOverGameEvent gameOverGameEvent)
    {
        StopAllCoroutines();
        m_AudioLowPassFilter.cutoffFrequency = 22000;
    }

    private IEnumerator LowPassRoutine()
    {
        m_AudioLowPassFilter.cutoffFrequency = m_LowCutoffFrequency;
        yield return new WaitForSecondsRealtime(m_TimeInSeconds);
        m_AudioLowPassFilter.cutoffFrequency = m_HighCutoffFrequency;
    }
}
