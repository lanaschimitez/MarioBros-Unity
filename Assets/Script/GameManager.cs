using System.Collections;
using System.Collections.Generic;
using UnityEditor.Android;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool m_isDead;
    public LifeUI lifeUI;
    public CoinUI coinUI;
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
        lifeUI = GameObject.Find("LifeText").GetComponent<LifeUI>();
        coinUI = GameObject.Find("CoinText").GetComponent<CoinUI>();

        //controlar as moedas
        //controlar as mortes
        //controlar os pontos
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
            //animacao de morte e mudaça de posição
        }
        else
        {
            MarioSmall();
        }
        transform.position = m_StartingPosition;
    }

    public void Life()
    {
        lifeUI.LifeUp();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("UpMushroom")) 
        {
            Life();
            Destroy(collision.gameObject);
        }
        else if(collision.gameObject.CompareTag("SuperMushroom"))
        {
            if (m_isSmall) MarioBig();
            Destroy(collision.gameObject);
        }
        else if(collision.gameObject.CompareTag("Coin"))
        {
            coinUI.AddCoins(1);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Limit"))
        {
            Dead();
        }
    }
}
