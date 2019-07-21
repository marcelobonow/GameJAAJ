using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class AdvancedMovementBehaviour : MonoBehaviour
{
    [SerializeField] public FirstPersonController firstPersonController;

    [SerializeField] private Image crosshair;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private bool isCrouch;

    private float oldWalkSpeed;
    private float oldRunSpeed;
    private float oldHeight;

    private void Awake()
    {
        firstPersonController = GetComponent<FirstPersonController>();
    }

    private void Update()
    {
        if(Input.GetButton("Crouch") && !isCrouch)
        {
            isCrouch = true;
            Crouch();
        }
        else if(!Input.GetButton("Crouch") && isCrouch)
        {
            isCrouch = false;
            ResetCrouch();
        }
    }

    private void Crouch()
    {
        oldWalkSpeed = firstPersonController.m_WalkSpeed;
        oldRunSpeed = firstPersonController.m_RunSpeed;
        oldHeight = characterController.height;

        characterController.height *= 0.65f;
        firstPersonController.m_WalkSpeed *= 0.7f;
        firstPersonController.m_RunSpeed *= 0.7f;
        UIManager.SetCrosshairColor(Color.green);
    }
    private void ResetCrouch()
    {
        firstPersonController.m_WalkSpeed = oldWalkSpeed;
        firstPersonController.m_RunSpeed = oldRunSpeed;
        characterController.height = oldHeight;
        UIManager.SetCrosshairColor(Color.white);
    }
}
