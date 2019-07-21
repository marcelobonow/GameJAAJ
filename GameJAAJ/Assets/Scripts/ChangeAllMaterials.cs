using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ChangeAllMaterials : MonoBehaviour
{
    private void Awake()
    {
        var allMaterials = FindObjectsOfType(typeof(Renderer));
        var shader = Shader.Find("Shader Graphs/DetetiveModeAssets");

        for(var i = 0; i < allMaterials.Length; i++)
        {
            var material = (Renderer)allMaterials[i];
            for(var o = 0; o < material.materials.Length; o++)
            {
                Debug.Log(material.materials[o].shader.name);
                if(material.materials[o].shader.name == "Standard")
                {
                    var oldColor = material.materials[o].GetColor("_Color");
                    Debug.Log("Material " + material.gameObject.name + " old color: " + oldColor);
                    material.materials[o].shader = shader;
                    material.materials[o].SetColor("Color_316AD9AF", oldColor);
                }
                //if(material.materials[o].shader.name == "Shader Graphs/DetetiveModeAssets")
                //    material.materials[o].SetFloat("Vector1_AEFDB795", newContrast);
            }
        }
    }
}
