using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomBehaviour : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;


    private bool isZoom;
    private float oldFOV;

    private void Awake() => oldFOV = playerCamera.fieldOfView;

    private void Update()
    {
        if(Input.GetButton("Zoom"))
        {
            if(!isZoom)
            {
                isZoom = true;
                oldFOV = playerCamera.fieldOfView;
            }
            playerCamera.fieldOfView = oldFOV/2f;
        }
        else if(!Input.GetButton("Zoom"))
        {
            isZoom = false;
            playerCamera.fieldOfView = oldFOV;
        }
    }
}
