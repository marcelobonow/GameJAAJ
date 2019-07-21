using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI interactableText;

    protected static UIManager instance;

    private void Awake()
    {
        if(instance != null)
            Destroy(this);
        instance = this;
    }

    public static void SetInteractMessage(bool active) => instance.interactableText.gameObject.SetActive(active);
}
