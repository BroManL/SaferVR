using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoMode : MonoBehaviour
{
    public GameObject Msg;
    Rigidbody rb;

    public float time1 = 0;
    public float force1 = 0.0f;
    public float time2 = 0;
    public float force2 = 0.0f;
    public float time3 = 0;
    public float force3 = 0.0f;

    bool autoMode = false;
    private float startTime = 0.0f;
    private float prevTime = 0.0f;
    private Vector3 startPos;
    private float thrustTmp = 0.0f;
    private float thrust = 0.0f;
    private float thrustPrev = 0.0f;
    private float thrustTotal = 0.0f;
    private double realDistance = 0.0;
    private double calcDistance = 0.0;
    private double v = 0.0f;
    private int counter = 0;

    bool flagEnd = true;
    float totalDt = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
        calcDistance = 0.0f;
        thrust = 0.0f;
        thrustTotal = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        // Auto mode
        if (Input.GetKeyDown(KeyCode.A))
        {
            autoMode = !autoMode;
            startTime = Time.time;
            startPos = transform.position;
            //rb.AddForce(10 * this.transform.forward);
            //thrustTotal += 10;
        }

        float time = 0.0f;
        if (autoMode)
        {
            if (Time.time - startTime <= time1)
            {
                thrust = force1;
                time = Time.time - startTime;
            }
            else if (Time.time - startTime <= time1 + time2)
            {
                thrust = force2;
                time = Time.time - startTime;
            }
            else if (Time.time - startTime <= time1 + time2 + time3)
            {
                thrust = force3;
                time = Time.time - startTime;
            }
            else
            {
                thrust = 0.0f; 
                autoMode = false;
            }

            if (autoMode)
            {
                rb.AddForce(thrust * this.transform.forward, ForceMode.Force);
                thrustTotal += thrust;
            }
        }

        if (true)
        {
            if (Time.time < 10.0f)
            {
                thrust = 10.0f;
                rb.AddForce(thrust * this.transform.forward, ForceMode.Force);
                //Debug.Log(string.Format("Force time: {0}, Force: {1}", Time.time, force));
            }
            if (Time.time >= 10.0f && Time.time < 20.0f)
            {
                thrust = -10.0f;
                rb.AddForce(thrust * this.transform.forward, ForceMode.Force);
                //Debug.Log(string.Format("Force time: {0}, Force: {1}", Time.time, force));
            }
            if (flagEnd && Time.time >= 20.0f)
            {
                thrust = 0.0f;
                flagEnd = false;
                Debug.Log(string.Format("Force time: {0}, Total Force: {1}, Dist: {2}", Time.time, thrust, transform.position.z));
                Debug.Log(string.Format("Total dt: {0}", totalDt));
            }
        }
                
        ++counter;
        thrustTotal += thrust;

        double accel = thrustTmp / rb.mass;
        double accelPrev = thrustPrev / rb.mass;
        double dt = (Time.time - startTime - prevTime);

        if (thrustTotal != 0)
        {
            v += accel * dt / 2;
            v += accelPrev * dt / 2;

            calcDistance += v * dt + (accelPrev * dt * dt) / 2;
        } else
        {
            v = 0;
        }

        prevTime = Time.time - startTime;
        thrustPrev = thrustTmp;
        thrustTmp = thrust;
        

        Vector3 cPos = transform.position;
        float startDist = Mathf.Sqrt(startPos.x * startPos.x + startPos.y * startPos.y + startPos.z * startPos.z);
        float currentDist = Mathf.Sqrt(cPos.x * cPos.x + cPos.y * cPos.y + cPos.z * cPos.z);
        realDistance = Mathf.Abs(currentDist - startDist);
        if (flagEnd)
        {   
            Debug.Log(string.Format("Real: {0:0.0000}, Calc: {1:0.0000}, Diff: {2:0.0000}, accel: {3:0.00}, v: {4:0.0000}, dt: {5:0.00}, x_v: {6:0.0000}, x_a: {7:0.0000}, Counter: {8}", 
                currentDist, calcDistance, currentDist - calcDistance, accel, v, dt, v * dt, (accel * dt * dt) / 2, counter));
            //Debug.Log(string.Format("Real: {0:0.00000}, Calc: {1:0.00000}, Diff: {2:0.00000}, Thrust: {3:0.00}, Counter: {4}", currentDist, calcDistance, currentDist - calcDistance, thrustTotal, counter));
        }

        Text txtAngle = Msg.GetComponent<Text>();
        string mst = string.Format("Авто режим: время {0:0.00}, тяга: {1}", time, thrust);
        mst += string.Format("\nПройденное расстояние: {0:0.0000}", realDistance);
        mst += string.Format("\nРасчетное расстояние: {0:0.0000}", calcDistance);
        mst += string.Format("\nСоотношение: {0:0.0000}", calcDistance / realDistance);
        mst += string.Format("\nСкорость: {0:0.0000}", v);
        txtAngle.text = mst;
        
    }
}
