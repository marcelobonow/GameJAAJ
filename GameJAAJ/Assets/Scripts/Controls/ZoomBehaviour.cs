using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomBehaviour : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;


    private float fovTransitionTime = 0.3f;
    private Coroutine zoom;
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
                if(zoom != null)
                    StopCoroutine(zoom);
                zoom = StartCoroutine(Zoom());
            }
        }
        else if(!Input.GetButton("Zoom"))
        {
            if(isZoom)
            {
                if(zoom != null)
                    StopCoroutine(zoom);
                zoom = StartCoroutine(Unzoom());
            }
            isZoom = false;
        }
    }

    private IEnumerator Zoom()
    {
        var startTime = Time.time;
        while(Time.time < startTime + fovTransitionTime)
        {
            playerCamera.fieldOfView = Mathf.Lerp(oldFOV, oldFOV/2f, (Time.time-startTime) / fovTransitionTime);
            Debug.Log("Aumentando zoom: " + playerCamera.fieldOfView);
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator Unzoom()
    {
        var startTime = Time.time;
        while(Time.time < startTime + fovTransitionTime)
        {
            playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, oldFOV, (Time.time-startTime) /fovTransitionTime);
            yield return new WaitForEndOfFrame();
        }
    }
}
