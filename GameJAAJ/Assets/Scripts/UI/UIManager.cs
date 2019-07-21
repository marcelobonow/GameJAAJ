using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI interactableText;
    [SerializeField] protected TextMeshProUGUI detectiveModeTimeRemaning;
    [SerializeField] protected TextMeshProUGUI detectiveModeTimeToRecharge;

    protected static UIManager instance;

    private void Awake()
    {
        if(instance != null)
            Destroy(this);
        instance = this;
    }

    public static void SetInteractMessage(bool active) => instance.interactableText.gameObject.SetActive(active);
    public static void SetEnableDetectiveMode(bool state)
    {
        instance.detectiveModeTimeRemaning.gameObject.SetActive(state);
        instance.detectiveModeTimeToRecharge.gameObject.SetActive(!state);
    }
    public static void SetDetectiveModeTimeRemaning(float timeRemaning) => instance.detectiveModeTimeRemaning.text = Mathf.Ceil(timeRemaning).ToString();
    public static void SetDetectiveModeTimeToRecharge(string timeToRecharge, Color color)
    {
        instance.detectiveModeTimeToRecharge.text = timeToRecharge;
        instance.detectiveModeTimeToRecharge.color = color;
    }
}
