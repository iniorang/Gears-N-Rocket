using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public Rigidbody controller;
    public float Accel, reverse, TopSpeed, handling;
    float SpeedInput, HandlingInput;
    bool isGrounded = false;
    public LayerMask groundLayer;
    [SerializeField] Transform PointTransform;

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
    }

    void FixedUpdate()
    {
        RotateCar();
        if (isGrounded)
        {
            controller.drag = 1f;
            if (Mathf.Abs(SpeedInput) > 0)
            {
                Forward();
            }
            else if (Mathf.Abs(SpeedInput) < 0)
            {
                Reverse();
            }
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, SpeedInput * HandlingInput * handling * Time.deltaTime));
        }
        else
        {
            controller.drag = .1f;
        }
    }

    void Forward()
    {
        controller.AddForce(transform.forward * SpeedInput * Accel * 100f);
    }

    void Reverse()
    {
        controller.AddForce(transform.forward * SpeedInput * reverse * 100f);
    }

    void RotateCar()
    {
        RaycastHit hit;
        isGrounded = Physics.Raycast(PointTransform.position, -transform.up, out hit, .5f, groundLayer);
        transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
    }
}
