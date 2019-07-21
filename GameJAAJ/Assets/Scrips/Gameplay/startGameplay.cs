using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startGameplay : MonoBehaviour

{
    public float timeAnim;
    public GameObject cena1, cena2, text1, text2;
    private float timer;

    void Awake()
    {
        cena1.SetActive(true);
        cena2.SetActive(false);
    }
    void Start()
    {
        text2.SetActive(false);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 2)
        {
            text1.SetActive(false);
            text2.SetActive(true);
        }
        if(timer >= timeAnim)
        {
            cena2.SetActive(true);
            cena1.SetActive(false);
        }
    }
}
