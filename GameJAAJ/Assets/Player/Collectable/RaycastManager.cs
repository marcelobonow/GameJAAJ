using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastManager : MonoBehaviour {

    [SerializeField] GameObject crosshair;
    [SerializeField] private float distance;
    [SerializeField] private RaycastHit hit;
    [SerializeField] private Vector3 position;
    [SerializeField] private Vector3 direction;
    [SerializeField] public LayerMask layerMaskInteract;


    void Start () {
		
	}
	
	void Update () {

        // Aqui começa o Raycast.
        Vector3 position = transform.position;
        Vector3 direction = transform.TransformDirection(Vector3.forward);
        if(Physics.Raycast(position, direction, out hit, distance, layerMaskInteract.value))
        {

        } 
    }
}
