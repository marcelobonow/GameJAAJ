using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class AdvancedMovementBehaviour : MonoBehaviour
{
    public FirstPersonController firstPersonController;

    [SerializeField] private Image crosshair;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private bool isCrounch;

    private float oldWalkSpeed;
    private float oldRunSpeed;
    private float oldHeight;

    private void Awake()
    {
        
    }

    private void Update()
    {
        if(Input.GetButton("Crounch") && !isCrounch)
        {
            isCrounch = true;
            Crounch();
        }
        else if(!Input.GetButton("Crounch") && isCrounch)
        {
            isCrounch = false;
            ResetCrounch();
        }
    }

    private void Crounch()
    {
        oldWalkSpeed = firstPersonController.m_WalkSpeed;
        oldRunSpeed = firstPersonController.m_RunSpeed;
        oldHeight = characterController.height;

        characterController.height *= 0.65f;
        firstPersonController.m_WalkSpeed *= 0.7f;
        firstPersonController.m_RunSpeed *= 0.7f;
        crosshair.GetComponent<Image>().color = Color.green;
    }
    private void ResetCrounch()
    {
        firstPersonController.m_WalkSpeed = oldWalkSpeed;
        firstPersonController.m_RunSpeed = oldRunSpeed;
        characterController.height = oldHeight;
        crosshair.GetComponent<Image>().color = Color.white;
    }
}
