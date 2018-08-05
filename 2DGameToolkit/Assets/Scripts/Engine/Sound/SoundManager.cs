using UnityEngine;
using System.Collections;

public class SoundManager
{
    [SerializeField] AudioSource m_EfxSource;
    [SerializeField] AudioSource m_MusicSource;

    public SoundManager ()
    {
        SoundManagerProxy.Open (this);
    }

    ~SoundManager ()
    {
        SoundManagerProxy.Close ();
    }

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

public class SoundManagerProxy : UniqueProxy<SoundManager>
{}