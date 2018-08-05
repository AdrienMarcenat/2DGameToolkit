using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Camera))]
public class Camera2D : MonoBehaviour
{
    [SerializeField] float m_FollowSpeed;
    [SerializeField] float m_ZoomFactor = 1.0f;
    [SerializeField] float m_ZoomSpeed = 5.0f;
    [SerializeField] Transform m_TrackingTarget;

    private Camera m_MainCamera;
    private Transform m_Player;

    void Awake ()
    {
        m_MainCamera = GetComponent<Camera> ();
        m_Player = GameObject.FindGameObjectWithTag ("Player").transform;
        SetZoom (m_ZoomFactor);
    }

    void Update ()
    {
        if (m_TrackingTarget == null)
            m_TrackingTarget = m_Player;

        float xTarget = m_TrackingTarget.position.x;
        float yTarget = m_TrackingTarget.position.y;

        float xNew = Mathf.Lerp (transform.position.x, xTarget, Time.deltaTime * m_FollowSpeed);
        float yNew = Mathf.Lerp (transform.position.y, yTarget, Time.deltaTime * m_FollowSpeed);

        transform.position = new Vector3 (xNew, yNew, transform.position.z);
    }

    public void SetZoom (float m_ZoomFactor)
    {
        if (m_MainCamera == null)
        {
            m_MainCamera = GetComponent<Camera> ();
        }
        this.m_ZoomFactor = m_ZoomFactor;
        StartCoroutine (Zoom ());
    }

    IEnumerator Zoom ()
    {
        float targetSize = m_ZoomFactor;
        if (targetSize < m_MainCamera.orthographicSize)
        {
            while (targetSize < GetComponent<Camera> ().orthographicSize)
            {
                m_MainCamera.orthographicSize -= Time.deltaTime * m_ZoomSpeed;
                yield return null;
            }
        }
        else
        {
            while (targetSize > m_MainCamera.orthographicSize)
            {
                m_MainCamera.orthographicSize += Time.deltaTime * m_ZoomSpeed;
                yield return null;
            }
        }
    }

    public void SetTrackingTarget (Transform newTarget)
    {
        m_TrackingTarget = newTarget;
    }
}
