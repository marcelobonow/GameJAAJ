using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] private bool isClosed = true;
    [SerializeField] private int keyToOpen;
    [SerializeField] private float timeToOpen = 1;
    [SerializeField] private GameObject door;
    [SerializeField] private Vector3 openRotation;
    [SerializeField] private Vector3 closedRotation;

    private void Awake()
    {
        if(isClosed)
            closedRotation = door.transform.rotation.eulerAngles;
        else
            openRotation = door.transform.rotation.eulerAngles;
    }

    public IEnumerator Open()
    {
        var initialTime = Time.time;
        while(Time.time < initialTime + timeToOpen)
        {
            door.transform.rotation = Quaternion.Euler(Vector3.Lerp(closedRotation, openRotation, (Time.time-initialTime)/timeToOpen));
            yield return new WaitForEndOfFrame();
        }
        isClosed = false;
    }
    public IEnumerator Close()
    {
        var initialTime = Time.time;
        while(Time.time < initialTime + timeToOpen)
        {
            door.transform.rotation = Quaternion.Euler(Vector3.Lerp(closedRotation, openRotation, (Time.time-initialTime)/timeToOpen));
            yield return new WaitForEndOfFrame();
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Interactable"))
        {
            var interactableObject = collision.transform.GetComponent<InteractableObject>();
            if(interactableObject.itemType == ItemTypes.key
                && interactableObject.GetComponent<Key>().keyId == keyToOpen)
            {
                StartCoroutine(Open());
                Destroy(collision.gameObject);
            }
        }
    }
}
