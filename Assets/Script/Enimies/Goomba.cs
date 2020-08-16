using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Goomba : MonoBehaviour
{
    [Header("Move")]
    public float m_SpeedMove = 5.0f;
    public float m_SpeedRotation = 15.0f;
    public Vector3 m_Movement = Vector3.left;
    private Rigidbody m_Body;
    public float m_ImpulseFoce = 5.0f;
    public int m_Score = 100;
    private bool m_Died;
    public GameObject GoombaAvatar;
    void Start()
    {
        m_Body = GetComponent<Rigidbody>();
        m_Body.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (m_Died) return;
        if (collision.gameObject.CompareTag("Gound")) return;

        if (collision.gameObject.tag.Contains("Player"))
        {
            collision.gameObject.GetComponent<GameManager>().Dead();
            // mario tomar dano 
        }
        else
        {
            m_Movement.x *= -1;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //ativar animacao amassando o bixinho e marcar ponto

        //criar game manager
        
        if (other.tag.Contains("Player"))
        {
            m_Died = true;
            GoombaAvatar.transform.localScale = new Vector2(1, 0.4f);
            Rigidbody body = other.GetComponent<Rigidbody>();
            Vector3 velocity = body.velocity;
            velocity.y = 0.0f;
            body.AddForce(Vector3.up * m_ImpulseFoce , ForceMode.Impulse);
            ScoreUI score = FindObjectOfType(typeof(ScoreUI)) as ScoreUI;
            score.AddScore(m_Score);
            Destroy(this.gameObject);
        }
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
    }
    private void Move()
    {
        m_Body.MovePosition(m_Body.position + m_Movement * m_SpeedMove * Time.fixedDeltaTime);

    }

    private void Rotate()
    {
        if (m_Movement.sqrMagnitude > 0.001f)
        {
            var forwardRotation = Quaternion.Euler(0, -90, 0) * Quaternion.LookRotation(m_Movement);
            m_Body.MoveRotation(Quaternion.Slerp(m_Body.rotation, forwardRotation, m_SpeedRotation * Time.fixedDeltaTime));
        }
    }
}
