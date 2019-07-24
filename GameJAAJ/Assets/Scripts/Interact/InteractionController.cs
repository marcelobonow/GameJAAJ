using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 5f;

    private Transform oldObjectInteractedParent;
    private Vector3 oldObjectInteractedPosition;

    private GameObject interactableObject = null;
    private float rotationX = 0;
    private float rotationY = 0;
    private Vector3 firstMousePosition = Vector3.zero;
    private bool clicked = false;

    private void Awake() => enabled = false;

    public void EnableInteraction(GameObject interactableObject, Transform itemInspectionHolder)
    {
        enabled = true;
        oldObjectInteractedPosition = interactableObject.transform.position;
        oldObjectInteractedParent = interactableObject.transform.parent;
        interactableObject.transform.SetParent(itemInspectionHolder);
        interactableObject.transform.localPosition = Vector3.zero;

        this.interactableObject = interactableObject;
        interactableObject.GetComponent<Rigidbody>().useGravity = false;
    }
    public void EndInteraction()
    {
        enabled = false;
        interactableObject.transform.SetParent(oldObjectInteractedParent);
        interactableObject.transform.position = oldObjectInteractedPosition;
        interactableObject.GetComponent<Rigidbody>().useGravity = true;

        rotationX = 0;
        rotationY = 0;
    }

    private void Update()
    {
        if(Input.GetButton("RotateObject"))
        {
            if(!clicked)
                firstMousePosition = Input.mousePosition;
            clicked = true;

            Vector3 offset = (Input.mousePosition - firstMousePosition);
            Vector3 deltaRotation = new Vector3(0, -offset.x, -offset.y);

            interactableObject.transform.Rotate(deltaRotation * rotationSpeed, Space.World);
        }
        else
            clicked = false;
    }
}
