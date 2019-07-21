using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Interactor : MonoBehaviour
{
    [SerializeField] private float interactionDistance;
    [SerializeField] private float timeToHold = 1f;
    [SerializeField] private GameObject interactItemSpawnPoint;
    [SerializeField] private FirstPersonController characterController;
    [SerializeField] private InteractionController interactionController;
    [SerializeField] private Camera firstPersonCamera;
    [SerializeField] private Camera interactCamera;

    private GameObject objectHolding;
    private Transform oldObjectHoldingTransform;
    private Quaternion oldObjectRotation;
    private float clickTime;

    private bool interacting = false;

    private void Awake() => SetObjectsState();
    private void Update()
    {
        var ray = firstPersonCamera.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
        var interactable = Physics.Raycast(
            ray,
            out var rayCastInfo,
            interactionDistance)
            && rayCastInfo.transform.CompareTag("Interactable");

        UIManager.SetInteractMessage(interactable && objectHolding == null && !interacting);

        if(interactable && !interacting && Input.GetButtonDown("Interact"))
        {
            clickTime = Time.time;
        }
        if(Input.GetButtonUp("Interact"))
        {
            if(objectHolding != null)
                DropObject();
            else if(interacting)
                EndInteraction();
            else if(interactable && !interacting)
                OnInteract(rayCastInfo);
        }
        if(Input.GetButton("Interact")
            && objectHolding == null
            && interactable
            && !interacting
            && Time.time > clickTime + timeToHold)
            HoldObject(rayCastInfo);

        if(objectHolding != null)
            objectHolding.transform.rotation = oldObjectRotation;

        SetObjectsState();
    }
    private void HoldObject(RaycastHit rayCastInfo)
    {
        var interactableObject = rayCastInfo.transform.gameObject;
        objectHolding = interactableObject;
        objectHolding.GetComponent<Rigidbody>().useGravity = false;
        oldObjectHoldingTransform = objectHolding.transform.parent;
        oldObjectRotation = objectHolding.transform.rotation;
        objectHolding.transform.SetParent(firstPersonCamera.transform);
    }
    private void DropObject()
    {
        if(objectHolding != null)
        {
            objectHolding.transform.SetParent(oldObjectHoldingTransform);
            objectHolding.GetComponent<Rigidbody>().useGravity = true;
            objectHolding = null;
        }
    }

    private void OnInteract(RaycastHit rayCastInfo)
    {
        interacting = true;
        interactionController.EnableInteraction(rayCastInfo.transform.gameObject, interactItemSpawnPoint.transform);
        UIManager.SetEnableCrosshair(false);
    }
    private void EndInteraction()
    {
        interacting = false;
        interactionController.EndInteraction();
        UIManager.SetEnableCrosshair(true);
    }
    private void SetObjectsState()
    {
        firstPersonCamera.gameObject.SetActive(!interacting);
        interactCamera.gameObject.SetActive(interacting);
        characterController.enabled = !interacting;
        Cursor.visible = interacting;
        Cursor.lockState = interacting ? CursorLockMode.None : CursorLockMode.Locked;

    }
}
