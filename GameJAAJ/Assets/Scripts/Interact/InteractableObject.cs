using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public GameObject reference;

    private void Awake()
    {
        reference = gameObject;
    }
}
