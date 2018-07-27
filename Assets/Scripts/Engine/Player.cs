using UnityEngine;

public class Player : MonoBehaviour
{
    private int m_SecondsBetweenEvent = 5;
    private float m_Timer = 0f;

    private void Awake ()
    {
        UpdaterProxy.Get ().Register (this, EUpdatePass.BeforeAI);
    }

    public void UpdateBeforeAI ()
    {
        m_Timer += Time.deltaTime;
        if(m_Timer > m_SecondsBetweenEvent / 2f)
        {
            m_Timer = 0f;
            new PlayerEvent ();
            new PlayerHealthEvent ();
        }
    }
}
