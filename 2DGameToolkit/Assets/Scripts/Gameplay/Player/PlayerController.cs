using UnityEngine;
using System.Collections;
using NUnit.Framework;

[RequireComponent(typeof(MovingObject))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float m_JumpForce = 5;
    [SerializeField] private bool m_AirControl = false;
    [SerializeField] private LayerMask m_WhatIsGround;

    private MovingObject m_Mover;
    private Transform m_GroundCheck;
    private const float ms_GroundedRadius = 0.2f;
    private bool m_Grounded;

    void Start ()
    {
        m_Mover = GetComponent<MovingObject> ();
        m_GroundCheck = transform.Find ("GroundCheck");
        this.RegisterAsListener ("Player", typeof(PlayerInputGameEvent));
    }

    private void FixedUpdate ()
    {
        m_Grounded = false;
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll (m_GroundCheck.position, ms_GroundedRadius, m_WhatIsGround);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != gameObject)
                m_Grounded = true;
        }
    }

    public void OnGameEvent(PlayerInputGameEvent inputEvent)
    {
        string input = inputEvent.GetInput ();
        EInputState state = inputEvent.GetInputState ();
        switch (input)
        {
            case "Jump":
                if (state == EInputState.Down)
                {
                    Jump ();
                }
                break;
            case "Right":
                Move (state == EInputState.Held ? 1 : 0);
                break;
            case "Left":
                Move (state == EInputState.Held ? -1 : 0);
                break;
            case "Fire":
                if (state == EInputState.Down)
                {
                    Fire ();
                }
                break;
            default:
                break;
        }
    }

    private void Jump()
    {
        if (m_Grounded)
        {
            // Don't wait fo the physic to set m_Grounded to false to ensure applying the force only once
            m_Grounded = false;
            m_Mover.ApplyForce (new Vector2 (0f, m_JumpForce));
        }
    }

    private void Move (float xDir)
    {
        if (m_Grounded || m_AirControl)
        {
            m_Mover.MoveHorizontal (xDir);
        }
    }

    private void Fire ()
    {

    }
}
