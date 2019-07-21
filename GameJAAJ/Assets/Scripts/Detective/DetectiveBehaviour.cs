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

    void Update()
    {
        if(Input.GetButton("Detective") && timeUsedInDetectiveMode <= 0)
            SetDetectiveMode(true, contrastInDetectiveMode);
        if(Input.GetButtonUp("Detective") || timeUsedInDetectiveMode > maxTimeInDetectiveMode)
            SetDetectiveMode(false, 1f);

        if(inDetectiveMode)
            timeUsedInDetectiveMode += Time.deltaTime;
        else if(timeUsedInDetectiveMode > 0)
        {
            timeUsedInDetectiveMode -= Time.deltaTime * maxTimeInDetectiveMode/timeToRecover;
            if(timeUsedInDetectiveMode < 0)
                timeUsedInDetectiveMode = 0;
        }
    }

    private void SetDetectiveMode(bool state, float contrast)
    {
        inDetectiveMode = state;
        SetContrast(contrast);
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
