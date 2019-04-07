using UnityEngine;

public class SoundManager : ISoundManager
{
    [SerializeField] AudioSource m_EfxSource;
    [SerializeField] AudioSource m_MusicSource;

    public void PlaySingle (AudioClip clip)
    {
        m_EfxSource.clip = clip;
        m_EfxSource.Play ();
    }

    public void PlayMultiple (AudioClip clip)
    {
       m_EfxSource.PlayOneShot (clip);
    }

    public void PlayMusic (AudioClip clip)
    {
        if (m_MusicSource.clip != clip)
        {
           m_MusicSource.clip = clip;
           m_MusicSource.Play ();
           m_MusicSource.loop = true;
        }
    }
}