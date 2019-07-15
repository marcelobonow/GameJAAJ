using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour {

    [Header ("Player")]
    public float readyToRun = 45.0f;

      //  [Header ("Audios")]
     //  public AudioClip BreathSound;
    // public AudioSource Breath_AudioSource;

    [Header("Valores")]
    public float speedDecrease = 2.5f;
    public float speedIncrease = 0.5f;
    public float stamina;

    [Header("Booleans")]
    public bool isRunning2;
    [SerializeField] private bool restoringStamina;
    public bool isWalking2;


    void Start () {

        stamina = 100.0f;
    }

	void Update () {

        float difference = stamina - 100.0f;
        if (isRunning2)
        {
            stamina -= speedDecrease * Time.deltaTime;
        }
        else
        {
            stamina += Time.deltaTime * speedIncrease;
        }
        
        //  
        if (stamina <= 20)
        {
            //Breath_AudioSource.volume = difference / 100f;
        }
        else
        {
            //Breath_AudioSource.volume -= Time.deltaTime;
        }
        if (stamina <= 0)
        {
            stamina = 0;
            StartCoroutine(Restore());
        }
        if (stamina >= 100)
        {
            stamina = 100;
        }
    }

    IEnumerator Restore()
    {
        restoringStamina = true;
        yield return new WaitUntil(() => stamina > readyToRun);
        restoringStamina = false;
    }
}
