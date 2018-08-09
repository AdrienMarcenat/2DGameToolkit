using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour 
{
	[SerializeField]
	float zoomFactor = 1.0f;

	[SerializeField]
	float zoomSpeed = 5.0f;

	private float originalSize = 0f;

	private Camera thisCamera;

	void Start()
	{
		thisCamera = GetComponent<Camera>();
		originalSize = thisCamera.orthographicSize;
	}

	void SetZoom(float zoomFactor)
	{
		this.zoomFactor = zoomFactor;
		StartCoroutine (Zoom());
	}

	IEnumerator Zoom()
	{
		float targetSize = originalSize * zoomFactor;
		while (targetSize != thisCamera.orthographicSize)
		{
			thisCamera.orthographicSize = Mathf.Lerp(thisCamera.orthographicSize, 
				targetSize, Time.deltaTime * zoomSpeed);
			yield return null;
		}
	}
}
