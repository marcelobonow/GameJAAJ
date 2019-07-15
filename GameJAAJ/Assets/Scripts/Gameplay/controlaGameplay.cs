using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class controlaGameplay : MonoBehaviour {

    float timer;
    bool startTimer;
    public Text textTimer;

    void Start () {
        timer = 300;	
	}
	
	void Update () {
        textTimer.text = ((int)timer).ToString();
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            timer = 0;
            fimdeGame();
        }
	}
    void fimdeGame()
    {

    }
}
