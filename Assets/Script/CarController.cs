using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public Rigidbody controller;
    public float Accel,Reverse,TopSpeed,handling;

    void Start()
    {
        controller = GetComponentInChildren<Rigidbody>();
        controller.transform.parent = null;
    }
    
    void Update()
    {
        transform.position = controller.transform.position;
    }

    void FixedUpdate() {
        controller.AddForce(transform.forward * Accel * 10f);
    }
}
