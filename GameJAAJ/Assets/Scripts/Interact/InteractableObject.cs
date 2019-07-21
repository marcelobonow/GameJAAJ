using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Flags]
public enum ItemTypes
{
    key,
    lighter,
}

public class InteractableObject : MonoBehaviour
{
    [SerializeField] public ItemTypes itemType;

}
