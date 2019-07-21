using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 5f;

    private GameObject interactableObject = null;
    private float rotationX = 0;
    private float rotationY = 0;
    private Vector3 firstMousePosition = Vector3.zero;
    private bool clicked = false;

    public void EnableInteraction(GameObject interactableObject)
    {
        enabled = true;
        this.interactableObject = interactableObject;
    }

    private void Update()
    {
        if(Input.GetButton("RotateObject"))
        {
            if(!clicked)
                firstMousePosition = Input.mousePosition;
            clicked = true;
            rotationX += Input.GetAxis("Mouse X")*rotationSpeed*Mathf.Deg2Rad;
            rotationY -= Input.GetAxis("Mouse Y")*rotationSpeed*Mathf.Deg2Rad;

            var desiredRotation = Quaternion.Euler(rotationY, -rotationX, 0);
            interactableObject.transform.rotation = desiredRotation;
        }
        else
            clicked = false;
    }
}
