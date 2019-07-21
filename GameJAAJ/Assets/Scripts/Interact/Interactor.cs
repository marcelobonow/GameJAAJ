using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Interactor : MonoBehaviour
{
    [SerializeField] private float interactionDistance;
    [SerializeField] private float holdObjectDistance;
    [SerializeField] private GameObject interactItemSpawnPoint;
    [SerializeField] private FirstPersonController characterController;
    [SerializeField] private InteractionController interactionController;
    [SerializeField] private Camera firstPersonCamera;
    [SerializeField] private Camera interactCamera;

    private GameObject objectHolding;
    private Transform oldObjectHoldingTransform;
    private Quaternion oldObjectRotation;
    private bool holdingObject;
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

        UIManager.SetInteractMessage(interactable);

        if(interactable && Input.GetButtonDown("Interact"))
            OnInteract(rayCastInfo);
        if(Input.GetButtonDown("HoldObject") && interactable && !interacting)
            HoldObject(rayCastInfo);
        if(Input.GetButtonUp("HoldObject"))
            DropObject();

        if(objectHolding != null)
            objectHolding.transform.rotation = oldObjectRotation;

        SetObjectsState();
    }
    private void HoldObject(RaycastHit rayCastInfo)
    {
        var interactableObject = rayCastInfo.transform.GetComponent<InteractableObject>().gameObject;
        objectHolding = interactableObject;
        oldObjectHoldingTransform = objectHolding.transform.parent;
        oldObjectRotation = objectHolding.transform.rotation;
        objectHolding.transform.SetParent(firstPersonCamera.transform);
    }
    private void DropObject()
    {
        objectHolding?.transform.SetParent(oldObjectHoldingTransform);
        objectHolding = null;
    }

    private void OnInteract(RaycastHit rayCastInfo)
    {
        interacting = true;
        var interactableObject = rayCastInfo.transform.GetComponent<InteractableObject>();
        interactableObject.transform.localPosition = Vector3.zero;
        var interactionObject = Instantiate(interactableObject, interactItemSpawnPoint.transform);
        interactionController.EnableInteraction(interactionObject.gameObject);
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
