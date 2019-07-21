using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bookManager : MonoBehaviour

{
    public bool page1, page2, page3, page4, page5, page6;
    public GameObject vitoria, leticia, luana, jefferson, ronaldo, ricardo;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetButtonDown("1"))
        {
            vitoria.SetActive(true);
            leticia.SetActive(false);
            luana.SetActive(false);
            jefferson.SetActive(false);
            ronaldo.SetActive(false);
            ricardo.SetActive(false);
        }
        if (Input.GetButtonDown("2"))
        {
            vitoria.SetActive(false);
            leticia.SetActive(true);
            luana.SetActive(false);
            jefferson.SetActive(false);
            ronaldo.SetActive(false);
            ricardo.SetActive(false);
        }
        if (Input.GetButtonDown("3"))
        {
            vitoria.SetActive(false);
            leticia.SetActive(false);
            luana.SetActive(true);
            jefferson.SetActive(false);
            ronaldo.SetActive(false);
            ricardo.SetActive(false);
        }
        if (Input.GetButtonDown("4"))
        {
            vitoria.SetActive(false);
            leticia.SetActive(false);
            luana.SetActive(false);
            jefferson.SetActive(true);
            ronaldo.SetActive(false);
            ricardo.SetActive(false);
        }
        if (Input.GetButtonDown("5"))
        {
            vitoria.SetActive(false);
            leticia.SetActive(false);
            luana.SetActive(false);
            jefferson.SetActive(false);
            ronaldo.SetActive(true);
            ricardo.SetActive(false);
        }
        if (Input.GetButtonDown("6"))
        {
            vitoria.SetActive(false);
            leticia.SetActive(false);
            luana.SetActive(false);
            jefferson.SetActive(false);
            ronaldo.SetActive(false);
            ricardo.SetActive(true);
        }         
    }
    public void Pagina1()
    {
        page1 = true;
    }
    public void Pagina2()
    {
        page2 = true;
    }
    public void Pagina3()
    {
        page3 = true;
    }
    public void Pagina4()
    {
        page4 = true;
    }
    public void Pagina5()
    {
        page5 = true;
    }
    public void Pagina6()
    {
        page6 = true;
    }
}
