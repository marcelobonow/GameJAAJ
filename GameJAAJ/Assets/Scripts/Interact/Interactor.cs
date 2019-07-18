using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private float interactionDistance;
    [SerializeField] private Camera firstPersonCamera;

    private void Update()
    {
        var ray = firstPersonCamera.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));

        if(Physics.Raycast(ray, out var rayCastInfo, interactionDistance))
        {
            if(rayCastInfo.transform.CompareTag("Interactable"))
            {
                var interactableObject = rayCastInfo.transform.GetComponent<InteractableObject>();
                UIManager.EnableInteractMessage();
                return;
            }
        }
        UIManager.DisableInteractMessage();
    }
}
