using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestForce : MonoBehaviour
{
    public GameObject Msg;
    Rigidbody rb;
    bool flagEnd = true;

    float thrustTotal = 0.0f;
    float prevTime = 0.0f;
    float calcDistance = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        float accel = thrustTotal / rb.mass;
        float dt = Time.time - prevTime;
        calcDistance += (accel * dt * dt) / 2;
        prevTime = Time.time;

        if (Time.time < 10.0f)
        {
            float force = 10.0f;
            thrustTotal += force;
            rb.AddForce(force * this.transform.forward, ForceMode.Force);
            //Debug.Log(string.Format("Force time: {0}, Force: {1}", Time.time, force));
        }
        if (Time.time >= 10.0f && Time.time < 20.0f)
        {
            float force = -10.0f;
            thrustTotal += force;
            rb.AddForce(force * this.transform.forward);
            //Debug.Log(string.Format("Force time: {0}, Force: {1}", Time.time, force));
        }
        if (flagEnd && Time.time >= 20.0f)
        {
            flagEnd = false;
            Debug.Log(string.Format("Force time: {0}, Total Force: {1}, Dist: {2}", Time.time, thrustTotal, transform.position.z));
        }
    }
}
