using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public Rigidbody controller;
    public float Accel, reverse, TopSpeed, handling;
    float SpeedInput, HandlingInput;
    bool isGrounded;
    public LayerMask groundLayer;

    void Start()
    {
        controller = GetComponentInChildren<Rigidbody>();
        controller.transform.parent = null;
    }

    void Update()
    {
        HandlingInput = Input.GetAxis("Horizontal");
        SpeedInput = Input.GetAxis("Vertical");
        transform.position = controller.transform.position;
        RaycastHit hit;
        isGrounded = Physics.Raycast(transform.position, -transform.up, out hit, 1f, groundLayer);
        transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
    }

    void FixedUpdate()
    {
        if (SpeedInput > 0)
        {
            Forward();
        }
        else if (SpeedInput < 0)
        {
            Reverse();
        }
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, SpeedInput * HandlingInput * handling * Time.deltaTime));
    }

    void Forward()
    {
        if (isGrounded)
        {
            controller.AddForce(transform.forward * SpeedInput * Accel * 1000f);
        }
    }

    void Reverse()
    {
        if (isGrounded)
        {
            controller.AddForce(transform.forward * SpeedInput * reverse * 1000f);
        }
    }
}
