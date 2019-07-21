using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlaCutscene : MonoBehaviour
{
    public Animator fadeAnim;
    public GameObject  Player;
    private float timer;
    public float maxTimeAnim, maxTimeFade=2;

    
    void Start()
    {
    }

    void Update()
    {
        if (timer >= 0)
        {
            fadeAnim.SetBool("fadeIn", true);
        }
        timer += Time.deltaTime;
    }
}
