﻿using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected int m_Type;
    [SerializeField] protected int m_PlayerDamageOnCollision;
    [SerializeField] private AudioClip m_Sound;
    [SerializeField] private float m_HitColorTime;

    protected Health m_Health;
    protected SpriteRenderer m_Sprite;
    protected Color m_InitialColor;
    protected bool m_IsDying = false;

    protected void Awake()
    {
        m_Health = GetComponent<Health>();
        m_Sprite = GetComponentInChildren<SpriteRenderer>();
        m_InitialColor = m_Sprite.color;

        this.RegisterAsListener(gameObject.GetInstanceID().ToString(), typeof(GameOverGameEvent), typeof(DamageGameEvent));
    }

    IEnumerator HitRoutine(float damage)
    {
        m_Sprite.color = Color.Lerp(Color.blue, Color.red, damage / 100);
        yield return new WaitForSeconds(m_HitColorTime);
        m_Sprite.color = m_InitialColor;
    }

    public void OnGameEvent(DamageGameEvent damageEvent)
    {
        StartCoroutine(HitRoutine(damageEvent.GetDamage()));
    }

    public void OnGameEvent(GameOverGameEvent gameOverEvent)
    {
        m_IsDying = true;
        OnGameOver(true);
        // Remove collision to avoid hurting player or being hurt
        GetComponent<BoxCollider2D>().enabled = false;
        Rigidbody2D body = GetComponent<Rigidbody2D>();
        if (body != null)
        {
            // Avoid enemy falling through floor
            body.gravityScale = 0f;
        }
        // Let some time for the dying animation to be played
        Destroy(gameObject, 1);
    }

    protected void OnDestroy()
    {
        // Call the cleanup code if it has not been done
        if (!m_IsDying)
        {
            OnGameOver(false);
        }
        this.UnregisterAsListener(gameObject.GetInstanceID().ToString());
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Health playerHealth = other.gameObject.GetComponent<Health>();
            playerHealth.LoseHealth(m_PlayerDamageOnCollision);
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.KnockBack(transform);
            }
            OnPlayerCollision();
        }
    }

    protected virtual void OnPlayerCollision() { }
    protected virtual void OnGameOver(bool real) { }

    public bool IsDying()
    {
        return m_IsDying;
    }
}