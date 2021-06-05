using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class force : MonoBehaviour
{
    public GameObject Msg;    
    Rigidbody rb;
    public float Speed = 50.0f;
    public float TorqueSpeed = 5.0f;
    bool mode = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void AddForce(Vector3 vec)
    {
        if (mode)
        {
            rb.AddForce(Speed * vec);
        } else
        {
            rb.AddTorque(TorqueSpeed * vec);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Hand mode

        if (mode)
        {
            Text txtAngle = Msg.GetComponent<Text>();
            //txtAngle.text = "Линейное ускорение";
        }
        else
        {
            Text txtAngle = Msg.GetComponent<Text>();
            //txtAngle.text = "Вращение";
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            mode = !mode;
            if (mode)
            {
                rb.angularDrag = 1.0f;
            } else
            {
                rb.angularDrag = 0.0f;
            }
        }


        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            AddForce(-this.transform.up);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            AddForce(this.transform.up);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            AddForce(-this.transform.right);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            AddForce(this.transform.right);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            AddForce(this.transform.forward);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            AddForce(-this.transform.forward);
        }
        
        /*
        if (Input.GetKeyDown(KeyCode.A))
        {
            rb.AddTorque(TorqueSpeed * Vector3.up);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            rb.AddTorque(TorqueSpeed * Vector3.down);
        }
        */
    }
}
