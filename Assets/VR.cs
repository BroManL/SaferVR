using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR : MonoBehaviour
{
    public Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("GetKeyDown S");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(string.Format("Update, time {0}", Time.time));
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("GetKeyDown S");
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;
                Debug.Log(objectHit);

                // Do something with the object that was hit by the raycast.
            }
        }
            
    }
}
