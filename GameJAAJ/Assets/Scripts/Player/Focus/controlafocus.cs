using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlafocus : MonoBehaviour {

    [SerializeField] private GameObject defaultCamera;
    [SerializeField] public GameObject focusCamera;
    [SerializeField] public GameObject[] focusObject;
    [SerializeField] private GameObject defaultObject;
    [SerializeField] private bool objetosAtivados = false;
    [SerializeField] private GameObject detetiveUI;

    void Start () {
        defaultObject.SetActive(true);
        detetiveUI.SetActive(false);
        objetosAtivados = false;
        for (int x = 0; x < focusObject.Length; x++)
        {
            focusObject[x].SetActive(false);
        }
    }

    void Update () {   
        if (Input.GetButton("Fire2"))
        {
            defaultObject.SetActive(false);
            detetiveUI.SetActive(true);
            if (!objetosAtivados)
            {
                objetosAtivados = true;

            }
            Debug.Log("FOCUSED");
            defaultCamera.SetActive(false);
            focusCamera.SetActive(true);
            for (int x = 0; x < focusObject.Length; x++)
            {
                objetosAtivados = !objetosAtivados;   
                focusObject[x].SetActive(true);                
            }
        }   

        else
        {
            detetiveUI.SetActive(false);
            for (int x = 0; x < focusObject.Length; x++)
            {
                focusObject[x].SetActive(false);
            }

            objetosAtivados = false;
            Debug.Log("unfocused");
            defaultCamera.SetActive(true);
            defaultObject.SetActive(true);

            focusCamera.SetActive(false);
        }
	}
}
