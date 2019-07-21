using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DetectiveBehaviour : MonoBehaviour
{
    [SerializeField] private float contrastInDetectiveMode = 0.5f;
    [SerializeField] private float maxTimeInDetectiveMode = 2f;
    [SerializeField] private float timeToRecover = 4f;

    private bool inDetectiveMode = false;
    private float timeUsedInDetectiveMode = 0f;

    private void Start()
    {
        SetDetectiveMode(false, 1f);
        UIManager.SetDetectiveModeTimeToRecharge("Ready", Color.green);
    }

    void Update()
    {
        UIManager.SetDetectiveModeTimeRemaning(maxTimeInDetectiveMode - timeUsedInDetectiveMode);
        UIManager.SetDetectiveModeTimeToRecharge(
                Mathf.Ceil(timeUsedInDetectiveMode * timeToRecover/maxTimeInDetectiveMode).ToString(),
                Color.red);

        if(inDetectiveMode)
            timeUsedInDetectiveMode += Time.deltaTime;
        else
        {
            if(timeUsedInDetectiveMode > 0)
                timeUsedInDetectiveMode -= Time.deltaTime * maxTimeInDetectiveMode/timeToRecover;
            if(timeUsedInDetectiveMode <= 0)
            {
                timeUsedInDetectiveMode = 0;
                UIManager.SetDetectiveModeTimeToRecharge("Ready", Color.green);
            }
        }

        if(Input.GetButtonDown("Detective") && timeUsedInDetectiveMode <= 0)
            SetDetectiveMode(true, contrastInDetectiveMode);
        if(Input.GetButtonUp("Detective") || timeUsedInDetectiveMode > maxTimeInDetectiveMode)
            SetDetectiveMode(false, 1f);
    }

    private void SetDetectiveMode(bool state, float contrast)
    {
        inDetectiveMode = state;
        SetContrast(contrast);
        UIManager.SetEnableDetectiveMode(state);
    }

    private void SetContrast(float newContrast)
    {
        var allMaterials = FindObjectsOfType(typeof(Renderer));

        for(var i = 0; i < allMaterials.Length; i++)
        {
            var material = (Renderer)allMaterials[i];
            for(var o = 0; o < material.materials.Length; o++)
            {
                if(material.materials[o].shader.name == "Shader Graphs/DetetiveModeAssets")
                    material.materials[o].SetFloat("Vector1_AEFDB795", newContrast);
            }
        }
    }
}
