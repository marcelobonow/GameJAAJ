using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class texts : MonoBehaviour
{
    public GameObject text1;

    void Start()
    {
        text1.SetActive(false);   
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            text1.SetActive(true);
        }
        else
        {
            text1.SetActive(false);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            text1.SetActive(false);
        }
    }
}
