using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class notepad : MonoBehaviour {
    public bool isActived;
    public GameObject notepadItem;
	void Start () {
        notepadItem.SetActive(false);

    }
	
	void Update () {
        if (Input.GetButtonDown("Notepad"))
        {
            if (isActived)
            {
                isActived = false;
            }
            else
            {
                isActived = true;
            }
        }
        if (isActived)
        {
            Time.timeScale = 0.0f;
            notepadItem.SetActive(true);
            Cursor.visible = true;
        }
        if(!isActived)
        {
            Time.timeScale = 1.0f;
            notepadItem.SetActive(false);
            Cursor.visible = false;
        }

    }
}
