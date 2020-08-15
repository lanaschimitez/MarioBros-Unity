using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CoinMovement : MonoBehaviour
{
    public float rotateSpeed;
    float t = 0;
    void Start()
    {
        rotateSpeed = 200.0f;
        StartCoroutine(Spin());
    }

    private IEnumerator Spin()
    {
        while (true)
        {

            t += Time.deltaTime;
            Quaternion yAxis = Quaternion.Euler(90f, t * rotateSpeed , 0f);
            transform.rotation = yAxis;
            yield return 0;
        }
    }
}
