using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpMoviment : MonoBehaviour
{
    [Header("Move")]
    public float m_SpeedMove = 5.0f;
    public Vector3 m_Movement = Vector3.right;
    private Rigidbody m_Body;

    [Header("Others")]
    public float m_BounceTime = 0.5f;
    public float m_BounceDistance = 1.0f;
    public AnimationCurve m_Curve;

    private Vector3 m_Source;
    public bool m_CanBounce;
    private float m_StartTime;
    void Start()
    {
        m_StartTime = Time.time;
        m_CanBounce = true;
        m_Source = transform.position;
        m_Body = GetComponent<Rigidbody>();
        m_Body.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (m_CanBounce) return;
        if (collision.gameObject.CompareTag("Gound") || collision.gameObject.CompareTag("Brick")) return;
        else
        {
            m_Movement.x *= -1;
        }
    }
    void Update()
    {
        if (!m_CanBounce) return;
        float time = (Time.time - m_StartTime) / m_BounceTime;
        transform.position = m_Source + Vector3.up * m_BounceDistance * m_Curve.Evaluate(time);
        m_CanBounce = time < 1.0f;
    }

    private void FixedUpdate()
    {
        if (m_CanBounce) return;
        m_Body.useGravity = true;
        Move();
    }
    private void Move()
    {
        m_Body.MovePosition(m_Body.position + m_Movement * m_SpeedMove * Time.fixedDeltaTime);
    }
}
