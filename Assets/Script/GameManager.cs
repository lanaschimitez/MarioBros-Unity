using System.Collections;
using System.Collections.Generic;
using UnityEditor.Android;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int m_life;
    public bool m_isDead;
    public LifeUI lifeUI;
    // Start is called before the first frame update
    void Start()
    {
        lifeUI = GameObject.Find("LifeText").GetComponent<LifeUI>();
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

    public void Dead()
    {
        lifeUI.LifeDown();
    }
}
