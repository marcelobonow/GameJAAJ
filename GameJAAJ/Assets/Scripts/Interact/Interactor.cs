using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Interactor : MonoBehaviour
{
    [SerializeField] private float interactionDistance;
    [SerializeField] private GameObject interactItemSpawnPoint;
    [SerializeField] private FirstPersonController characterController;
    [SerializeField] private Camera firstPersonCamera;
    [SerializeField] private Camera interactCamera;

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
        SetObjectsState();
    }

    private void OnInteract(RaycastHit rayCastInfo)
    {
        interacting = true;
        var interactableObject = rayCastInfo.transform.GetComponent<InteractableObject>();
        interactableObject.transform.localPosition = Vector3.zero;
        Instantiate(interactableObject, interactItemSpawnPoint.transform);
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
