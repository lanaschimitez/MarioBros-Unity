using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Android;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool m_isDead;
    public LifeUI lifeUI;
    public CoinUI coinUI;
    public ScoreUI scoreUI;
    public bool m_isSmall;
    public GameObject m_MarioSmall;
    public GameObject m_MarioBig;
    public Animator animator;
    public Avatar MarioBigAvatar;
    public Avatar MarioSmallAvatar;
    private Vector3 m_StartingPosition;

    // Start is called before the first frame update
    void Start()
    {
        m_StartingPosition = gameObject.transform.position;
        lifeUI = GameObject.Find("LifeText").GetComponent<LifeUI>();        //controlar as mortes
        coinUI = GameObject.Find("CoinText").GetComponent<CoinUI>();         //controlar as moedas
        scoreUI = GameObject.Find("ScoreText").GetComponent<ScoreUI>();         //controlar os pontos

        //controlar os niveis
    }

    // Update is called once per frame
    public void Update()
    {
        // verificar a morte do Player
    }

    public void MarioBig()
    {
        //GetComponent<PlayerMovement>().m_IsRunning = true;
        animator.avatar = MarioBigAvatar;
        gameObject.tag = "BigPlayer";
        m_isSmall = false;
        m_MarioSmall.SetActive(false);
        m_MarioBig.SetActive(true);
    }

    public void MarioSmall()
    {
        //GetComponent<PlayerMovement>().m_IsRunning = false;
        animator.avatar = MarioSmallAvatar;
        gameObject.tag = "SmallPlayer";
        m_isSmall = true;
        m_MarioSmall.SetActive(true);
        m_MarioBig.SetActive(false);
    }

    public void Dead()
    {
        if (m_isSmall)
        {
            lifeUI.LifeDown();
            transform.position = m_StartingPosition;
            StartCoroutine(ChangeMario());
            //animacao de morte e mudaça de posição
        }
        else
        {
            MarioSmall();
            StartCoroutine(ChangeMario());
        }
    }

    public void Life()
    {
        lifeUI.LifeUp();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("UpMushroom"))
        {
            Life();
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("SuperMushroom"))
        {
            if (m_isSmall) MarioBig();
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Limit"))
        {
            Dead();
        }
        else if (collision.gameObject.CompareTag("Pole"))
        {
            PoleAnim();
        }
    }

    private void PoleAnim()
    {
        gameObject.GetComponent<PlayerMovement>().enabled = false;
        gameObject.GetComponent<Rigidbody>().useGravity = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            scoreUI.AddScore(1);
            coinUI.AddCoins(1);
            Destroy(other.gameObject);
        }
    }

    //usar quando o BigMario ser atingido e precisar mudar de corpo
    public IEnumerator ChangeMario()
    {
        float counter = 0;
        float waitTime = 12.0f;
        bool active = false;
        while (counter < waitTime)
        {
            foreach (Transform t in m_MarioSmall.transform)
            {
                if (t.GetComponent<SkinnedMeshRenderer>())
                {
                    t.GetComponent<SkinnedMeshRenderer>().enabled = active;
                }
            }
            active ^= true;
            yield return new WaitForSecondsRealtime(0.25f);
            //Increment Timer until counter >= waitTime
            counter++;
        }
        m_MarioSmall.SetActive(true);
    }
}
