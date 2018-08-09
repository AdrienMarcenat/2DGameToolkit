using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2D1 : MonoBehaviour 
{
	[SerializeField]
	protected Transform m_TrackingTarget;

	[SerializeField]
	List<Transform> lanes;

	[SerializeField]
	float xOffset;

	[SerializeField]
	float yOffset;

	[SerializeField]
	protected float followSpeed;

	[SerializeField]
	protected bool isXLocked = false;

	[SerializeField]
	protected bool isYLocked = false;

    private Transform m_Player;

    void Awake ()
    {
        m_Player = GameObject.FindGameObjectWithTag ("Player").transform;
    }

    void Update()
	{
        if (m_TrackingTarget == null)
            m_TrackingTarget = m_Player;

        float xTarget = m_TrackingTarget.position.x + xOffset;
		float yTarget = m_TrackingTarget.position.y + yOffset;

		float xNew = transform.position.x;
		if (!isXLocked)
		{
			xNew = Mathf.Lerp(transform.position.x, xTarget, Time.deltaTime * followSpeed);
		}

		float yNew = transform.position.y;
		if (!isYLocked)
		{
			yNew = Mathf.Lerp (transform.position.y, yTarget, Time.deltaTime * followSpeed);
		}
		else
		{
			if (lanes.Count > 1)
			{
				int i = 0;
				for (i = 0; i < lanes.Count - 1; ++i)
				{
					if ((m_TrackingTarget.position.y > lanes [i].position.y) &&
					    (m_TrackingTarget.position.y <= lanes [i + 1].position.y))
					{
						yNew = lanes [i].position.y;
						break;
					}
				}

				if (i == lanes.Count - 1)
					yNew = lanes [lanes.Count - 1].position.y;
			}
			else if (lanes.Count > 0)
            {
				yNew = lanes [0].position.y;
			}
		}

		yNew = Mathf.Lerp(transform.position.y, yNew + yOffset, Time.deltaTime * followSpeed);
		transform.position = new Vector3(xNew, yNew, transform.position.z);
	}
}
