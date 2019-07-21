using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DetectiveBehaviour : MonoBehaviour
{
    [SerializeField] private float contrastInDetectiveMode = 0.5f;
    private bool inDetectiveMode = false;

    void Update()
    {
        if(Input.GetButton("Detective"))
        {
            inDetectiveMode = true;
            SetConstrast(contrastInDetectiveMode);
        }
        if(Input.GetButtonUp("Detective"))
            SetConstrast(1);
    }

    private void SetConstrast(float newContrast)
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
