using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovingObject : MonoBehaviour
{
    [SerializeField] private float m_SmoothSpeed;
    private Rigidbody2D m_RigidBody;

	void Start ()
	{
        m_RigidBody = GetComponent <Rigidbody2D> ();
	}

	public void Move (float xDir, float yDir)
	{
        m_RigidBody.velocity = m_SmoothSpeed * (new Vector2 (xDir, yDir).normalized);
	}
}