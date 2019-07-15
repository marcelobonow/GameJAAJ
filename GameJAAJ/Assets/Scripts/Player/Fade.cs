using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour {

    private float timer;
	void Start () {
		
	}
	
	void Update () {
        timer += Time.deltaTime;
        if(timer >= 2)
        {
            this.gameObject.SetActive(false);
        }
	}
}
