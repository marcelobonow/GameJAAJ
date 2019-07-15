using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;


public class Collectable : MonoBehaviour {

    FirstPersonController raycastManager;

    // Use this for initialization
    void Start () {

        raycastManager = FindObjectOfType<FirstPersonController>();

    }

    // Update is called once per frame
    void Update () {
		
	}
}
