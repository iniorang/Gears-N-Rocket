using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    //STAS
    public int maxSpeed = 90; //Kecepatan maksimal
    public int maxRevSpeed = 45; //Kecepatan maksimal saat mundur
    public int maxSteerAngle = 30; //Maksimal mobil berbelok
    public float steerSpeed = 1f; //Kecepatan berbelok
    public int brakeForce = 400; //Besar gaya untuk rem
    public int accelMultiplier = 2; //Akseslarasi
    public Vector3 centerOfMass;
    [Space(10)]

    //INPUTS
    [SerializeField] float inputY; //Sebagai inputan untuk maju atau mundur
    [SerializeField] float inputX; //Sebagai inputan untuk maju atau mundur

    [Space(10)]
    //BAN (VISUAL DAN CONTROLLER)
    public GameObject frontLeftMesh;
    public WheelCollider frontLeftCollider;
    public GameObject frontRightMesh;
    public WheelCollider frontRightCollider;
    public GameObject rearLeftMesh;
    public WheelCollider rearLeftCollider;
    public GameObject rearRightMesh;
    public WheelCollider rearRightCollider;

    Rigidbody rb;
    [SerializeField] float carSpeed;
    float throttleAxis;
    float steeringAxis;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass;
    }
    private void Update()
    {
        InputAxis();
        Steering();
        AnimateWheel();
        carSpeed = 2 * (Mathf.PI * frontLeftCollider.radius * frontLeftCollider.rpm * 60) / 1000;
    }
    private void FixedUpdate()
    {
        MoveForward();
    }

    //Inputan untunk membelok mobil
    void Steering()
    {
        float steeringAxis = inputX + (Time.deltaTime * 10f * steerSpeed);
        if(steeringAxis > 1f){
            steeringAxis = 1f;
        }else if(steeringAxis < -1f){
            steeringAxis = -1f;
        }
        var steeringAngle = steeringAxis * maxSteerAngle;
        frontLeftCollider.steerAngle = Mathf.Lerp(frontLeftCollider.steerAngle, steeringAngle, .5f);
        frontRightCollider.steerAngle = Mathf.Lerp(frontRightCollider.steerAngle, steeringAngle, .5f);
        if (inputX == 0)
        {
            frontLeftCollider.steerAngle = Mathf.Lerp(frontLeftCollider.steerAngle, steeringAngle, steerSpeed);
            frontRightCollider.steerAngle = Mathf.Lerp(frontRightCollider.steerAngle, steeringAngle, steerSpeed);
        }
    }

    void MoveForward()
    {
        if (Mathf.RoundToInt(carSpeed) < maxSpeed)
        {
            frontLeftCollider.motorTorque = throttleAxis * (accelMultiplier * 50);
            frontRightCollider.motorTorque = throttleAxis * (accelMultiplier * 50);
            rearLeftCollider.motorTorque = throttleAxis * (accelMultiplier * 50);
            rearRightCollider.motorTorque = throttleAxis * (accelMultiplier * 50);
        }
        else
        {
            frontLeftCollider.motorTorque = 0;
            frontRightCollider.motorTorque = 0;
            rearLeftCollider.motorTorque = 0;
            rearRightCollider.motorTorque = 0;
        }

    }
    void MoveReverse()
    {
        if (Mathf.RoundToInt(carSpeed) < maxSpeed)
        {
            frontLeftCollider.motorTorque = throttleAxis * (accelMultiplier * 50);
            frontRightCollider.motorTorque = throttleAxis * (accelMultiplier * 50);
            rearLeftCollider.motorTorque = throttleAxis * (accelMultiplier * 50);
            rearRightCollider.motorTorque = throttleAxis * (accelMultiplier * 50);
        }
        else
        {
            frontLeftCollider.motorTorque = 0;
            frontRightCollider.motorTorque = 0;
            rearLeftCollider.motorTorque = 0;
            rearRightCollider.motorTorque = 0;
        }

    }

    void AnimateWheel(){
        Quaternion FLRotation;
        Vector3 FLPosition;
        frontLeftCollider.GetWorldPose(out FLPosition,out FLRotation);
        frontLeftMesh.transform.position = FLPosition;
        frontLeftMesh.transform.rotation = FLRotation;

        Quaternion FRRotation;
        Vector3 FRPosition;
        frontRightCollider.GetWorldPose(out FRPosition,out FRRotation);
        frontRightMesh.transform.position = FRPosition;
        frontRightMesh.transform.rotation = FRRotation;

        Quaternion RLRotation;
        Vector3 RLPosition;
        rearLeftCollider.GetWorldPose(out RLPosition,out RLRotation);
        rearLeftMesh.transform.position = RLPosition;
        rearLeftMesh.transform.rotation = RLRotation;

        Quaternion RRRotation;
        Vector3 RRPosition;
        rearRightCollider.GetWorldPose(out RRPosition,out RRRotation);
        rearRightMesh.transform.position = RRPosition;
        rearRightMesh.transform.rotation = RRRotation;
    }


    void InputAxis()
    {
        inputY = Input.GetAxis("Vertical");
        inputX = Input.GetAxis("Horizontal");
        throttleAxis = inputY + (Time.deltaTime * 3f);
        steeringAxis = inputX + (Time.deltaTime * 10f * steerSpeed);
    }
}
