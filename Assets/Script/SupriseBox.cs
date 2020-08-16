using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupriseBox : MonoBehaviour
{
    public GameObject m_EmptyBox;
    public GameObject m_PowerUp;
    private Bounds m_Bounds;

    private void Start()
    {
        m_Bounds = GetComponent<Collider>().bounds;
    }

    private void OnCollisionEnter(Collision collision)
    {

        Vector3 impact = collision.contacts[0].point;
        if (impact.y <= m_Bounds.min.y)
        {
            if (m_PowerUp)
            {
                Instantiate(m_PowerUp, new Vector3(transform.position.x + 0.3f, transform.position.y + 0.7f, transform.position.z), Quaternion.Euler(90.0f, 0.0f, -90.0f));
            }
            Instantiate(m_EmptyBox, transform.position, Quaternion.identity);


            Destroy(gameObject);
        }
    }
}
