using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{

  [Space(20)]
  [Space(10)]
  [Range(20, 190)]
  public int maxSpeed = 90;
  [Range(10, 120)]
  public int maxReverseSpeed = 45;
  [Range(1, 10)]
  public int accelerationMultiplier = 2;
  [Space(10)]
  [Range(10, 45)]
  public int maxSteeringAngle = 27;
  [Range(0.1f, 1f)]
  public float steeringSpeed = 0.5f;
  [Space(10)]
  [Range(100, 600)]
  public int brakeForce = 350;
  [Range(1, 10)]
  public int decelerationMultiplier = 2;
  [Range(1, 10)]
  public int handbrakeDriftMultiplier = 5;
  [Space(10)]
  public Vector3 bodyMassCenter;
  public GameObject frontLeftMesh;
  public WheelCollider frontLeftCollider;
  [Space(10)]
  public GameObject frontRightMesh;
  public WheelCollider frontRightCollider;
  [Space(10)]
  public GameObject rearLeftMesh;
  public WheelCollider rearLeftCollider;
  [Space(10)]
  public GameObject rearRightMesh;
  public WheelCollider rearRightCollider;

  [Space(20)]
  [Space(10)]
  public bool useEffects = false;


  public ParticleSystem RLWParticleSystem;
  public ParticleSystem RRWParticleSystem;

  [Space(10)]

  public TrailRenderer RLWTireSkid;
  public TrailRenderer RRWTireSkid;


  [Space(20)]
  [Space(10)]
  public bool useUI = false;
  public Text carSpeedText;

  [Space(20)]
  [Space(10)]
  public bool useSounds = false;
  public AudioSource carEngineSound;
  public AudioSource tireScreechSound;
  float initialCarEngineSoundPitch;


  [HideInInspector]
  public float carSpeed;
  [HideInInspector]
  public bool isDrifting;
  [HideInInspector]
  bool isTractionLocked;
  bool deceleratingCar;
  [HideInInspector]
  float steeringAxis;
  [HideInInspector]
  float throttleAxis;

  InputManager im;
  Rigidbody carRigidbody;
  float driftingAxis;
  float localVelocityZ;
  float localVelocityX;

  WheelFrictionCurve FLwheelFriction;
  float FLWextremumSlip;
  WheelFrictionCurve FRwheelFriction;
  float FRWextremumSlip;
  WheelFrictionCurve RLwheelFriction;
  float RLWextremumSlip;
  WheelFrictionCurve RRwheelFriction;
  float RRWextremumSlip;

  // Start is called before the first frame update
  void Start()
  {
    carRigidbody = gameObject.GetComponent<Rigidbody>();
    im = gameObject.GetComponent<InputManager>();
    carRigidbody.centerOfMass = bodyMassCenter;

    FLwheelFriction = new WheelFrictionCurve();
    FLwheelFriction.extremumSlip = frontLeftCollider.sidewaysFriction.extremumSlip;
    FLWextremumSlip = frontLeftCollider.sidewaysFriction.extremumSlip;
    FLwheelFriction.extremumValue = frontLeftCollider.sidewaysFriction.extremumValue;
    FLwheelFriction.asymptoteSlip = frontLeftCollider.sidewaysFriction.asymptoteSlip;
    FLwheelFriction.asymptoteValue = frontLeftCollider.sidewaysFriction.asymptoteValue;
    FLwheelFriction.stiffness = frontLeftCollider.sidewaysFriction.stiffness;
    FRwheelFriction = new WheelFrictionCurve();
    FRwheelFriction.extremumSlip = frontRightCollider.sidewaysFriction.extremumSlip;
    FRWextremumSlip = frontRightCollider.sidewaysFriction.extremumSlip;
    FRwheelFriction.extremumValue = frontRightCollider.sidewaysFriction.extremumValue;
    FRwheelFriction.asymptoteSlip = frontRightCollider.sidewaysFriction.asymptoteSlip;
    FRwheelFriction.asymptoteValue = frontRightCollider.sidewaysFriction.asymptoteValue;
    FRwheelFriction.stiffness = frontRightCollider.sidewaysFriction.stiffness;
    RLwheelFriction = new WheelFrictionCurve();
    RLwheelFriction.extremumSlip = rearLeftCollider.sidewaysFriction.extremumSlip;
    RLWextremumSlip = rearLeftCollider.sidewaysFriction.extremumSlip;
    RLwheelFriction.extremumValue = rearLeftCollider.sidewaysFriction.extremumValue;
    RLwheelFriction.asymptoteSlip = rearLeftCollider.sidewaysFriction.asymptoteSlip;
    RLwheelFriction.asymptoteValue = rearLeftCollider.sidewaysFriction.asymptoteValue;
    RLwheelFriction.stiffness = rearLeftCollider.sidewaysFriction.stiffness;
    RRwheelFriction = new WheelFrictionCurve();
    RRwheelFriction.extremumSlip = rearRightCollider.sidewaysFriction.extremumSlip;
    RRWextremumSlip = rearRightCollider.sidewaysFriction.extremumSlip;
    RRwheelFriction.extremumValue = rearRightCollider.sidewaysFriction.extremumValue;
    RRwheelFriction.asymptoteSlip = rearRightCollider.sidewaysFriction.asymptoteSlip;
    RRwheelFriction.asymptoteValue = rearRightCollider.sidewaysFriction.asymptoteValue;
    RRwheelFriction.stiffness = rearRightCollider.sidewaysFriction.stiffness;

    if (carEngineSound != null)
    {
      initialCarEngineSoundPitch = carEngineSound.pitch;
    }

    if (useUI)
    {
      InvokeRepeating("CarSpeedUI", 0f, 0.1f);
    }
    else if (!useUI)
    {
      if (carSpeedText != null)
      {
        carSpeedText.text = "0";
      }
    }

    if (useSounds)
    {
      InvokeRepeating("CarSounds", 0f, 0.1f);
    }
    else if (!useSounds)
    {
      if (carEngineSound != null)
      {
        carEngineSound.Stop();
      }
      if (tireScreechSound != null)
      {
        tireScreechSound.Stop();
      }
    }

    if (!useEffects)
    {
      if (RLWParticleSystem != null)
      {
        RLWParticleSystem.Stop();
      }
      if (RRWParticleSystem != null)
      {
        RRWParticleSystem.Stop();
      }
      if (RLWTireSkid != null)
      {
        RLWTireSkid.emitting = false;
      }
      if (RRWTireSkid != null)
      {
        RRWTireSkid.emitting = false;
      }
    }
  }

  // Update is called once per frame
  void Update()
  {
    carSpeed = 2 * Mathf.PI * frontLeftCollider.radius * frontLeftCollider.rpm * 60 / 1000;
    localVelocityX = transform.InverseTransformDirection(carRigidbody.velocity).x;
    localVelocityZ = transform.InverseTransformDirection(carRigidbody.velocity).z;

    AnimateWheelMeshes();
  }

  public void CarSpeedUI()
  {

    if (useUI)
    {
      try
      {
        float absoluteCarSpeed = Mathf.Abs(carSpeed);
        carSpeedText.text = Mathf.RoundToInt(absoluteCarSpeed).ToString();
      }
      catch (Exception ex)
      {
        Debug.LogWarning(ex);
      }
    }

  }

  public void CarSounds()
  {

    if (useSounds)
    {
      try
      {
        if (carEngineSound != null)
        {
          float engineSoundPitch = initialCarEngineSoundPitch + (Mathf.Abs(carRigidbody.velocity.magnitude) / 25f);
          carEngineSound.pitch = engineSoundPitch;
        }
        if (isDrifting || (isTractionLocked && Mathf.Abs(carSpeed) > 12f))
        {
          if (!tireScreechSound.isPlaying)
          {
            tireScreechSound.Play();
          }
        }
        else if ((!isDrifting) && (!isTractionLocked || Mathf.Abs(carSpeed) < 12f))
        {
          tireScreechSound.Stop();
        }
      }
      catch (Exception ex)
      {
        Debug.LogWarning(ex);
      }
    }
    else if (!useSounds)
    {
      if (carEngineSound != null && carEngineSound.isPlaying)
      {
        carEngineSound.Stop();
      }
      if (tireScreechSound != null && tireScreechSound.isPlaying)
      {
        tireScreechSound.Stop();
      }
    }

  }

  public void Steering(float steerinput)
  {
    if (steerinput == 0f)
    {
      if (steeringAxis < 0f)
      {
        steeringAxis += Time.deltaTime * 10f * steeringSpeed;
      }
      else if (steeringAxis > 0f)
      {
        steeringAxis -= Time.deltaTime * 10f * steeringSpeed;
      }

      if (Mathf.Abs(steeringAxis) < 0.1f)
      {
        steeringAxis = 0f;
      }
    }
    else
    {

      steeringAxis = steerinput + (Time.deltaTime * 10f * steeringSpeed);
      steeringAxis = Mathf.Clamp(steeringAxis, -1f, 1f);
    }

    var steeringAngle = steeringAxis * maxSteeringAngle;
    frontLeftCollider.steerAngle = Mathf.Lerp(frontLeftCollider.steerAngle, steeringAngle, steeringSpeed);
    frontRightCollider.steerAngle = Mathf.Lerp(frontRightCollider.steerAngle, steeringAngle, steeringSpeed);
  }

  void AnimateWheelMeshes()
  {
    try
    {
      Quaternion FLWRotation;
      Vector3 FLWPosition;
      frontLeftCollider.GetWorldPose(out FLWPosition, out FLWRotation);
      frontLeftMesh.transform.position = FLWPosition;
      frontLeftMesh.transform.rotation = FLWRotation;

      Quaternion FRWRotation;
      Vector3 FRWPosition;
      frontRightCollider.GetWorldPose(out FRWPosition, out FRWRotation);
      frontRightMesh.transform.position = FRWPosition;
      frontRightMesh.transform.rotation = FRWRotation;

      Quaternion RLWRotation;
      Vector3 RLWPosition;
      rearLeftCollider.GetWorldPose(out RLWPosition, out RLWRotation);
      rearLeftMesh.transform.position = RLWPosition;
      rearLeftMesh.transform.rotation = RLWRotation;

      Quaternion RRWRotation;
      Vector3 RRWPosition;
      rearRightCollider.GetWorldPose(out RRWPosition, out RRWRotation);
      rearRightMesh.transform.position = RRWPosition;
      rearRightMesh.transform.rotation = RRWRotation;
    }
    catch (Exception ex)
    {
      Debug.LogWarning(ex);
    }
  }

  public void MoveCar(float input)
  {
    if (Mathf.Abs(localVelocityX) > 2.5f)
    {
      isDrifting = true;
      DriftCarPS();
    }
    else
    {
      isDrifting = false;
      DriftCarPS();
    }

    // Adjust throttle axis based on input
    if (input > 0)
    {
      throttleAxis += Time.deltaTime * 3f;
      throttleAxis = Mathf.Clamp(throttleAxis, 0f, 1f);
    }
    else if (input < 0)
    {
      throttleAxis -= Time.deltaTime * 3f;
      throttleAxis = Mathf.Clamp(throttleAxis, -1f, 0f);
    }
    else
    {
      // Decelerate if no input
      if (throttleAxis != 0f)
      {
        if (throttleAxis > 0f)
        {
          throttleAxis -= Time.deltaTime * 10f;
        }
        else if (throttleAxis < 0f)
        {
          throttleAxis += Time.deltaTime * 10f;
        }
        if (Mathf.Abs(throttleAxis) < 0.15f)
        {
          throttleAxis = 0f;
        }
      }
    }

    // Apply throttle or brake based on throttleAxis
    if (throttleAxis > 0)
    {
      // Forward movement
      if (localVelocityZ < -1f && input > 0)
      {
        Brakes();
      }
      else if (Mathf.RoundToInt(carSpeed) < maxSpeed)
      {
        ApplyMotorTorque(throttleAxis);
      }
      else
      {
        ThrottleOff();
      }
    }
    else if (throttleAxis < 0)
    {
      // Reverse movement
      if (localVelocityZ > 1f && input < 0)
      {
        Brakes();
      }
      else if (Mathf.Abs(Mathf.RoundToInt(carSpeed)) < maxReverseSpeed)
      {
        ApplyMotorTorque(throttleAxis);
      }
      else
      {
        ThrottleOff();
      }
    }
    else
    {
      // Throttle off
      ThrottleOff();
    }
  }

  private void ApplyMotorTorque(float axis)
  {
    float motorTorque = accelerationMultiplier * 50f * axis;

    frontLeftCollider.brakeTorque = 0;
    frontLeftCollider.motorTorque = motorTorque;

    frontRightCollider.brakeTorque = 0;
    frontRightCollider.motorTorque = motorTorque;

    rearLeftCollider.brakeTorque = 0;
    rearLeftCollider.motorTorque = motorTorque;

    rearRightCollider.brakeTorque = 0;
    rearRightCollider.motorTorque = motorTorque;
  }

  private void Decelerate()
  {
    if (throttleAxis != 0f)
    {
      if (throttleAxis > 0f)
      {
        throttleAxis -= Time.deltaTime * 10f;
      }
      else if (throttleAxis < 0f)
      {
        throttleAxis += Time.deltaTime * 10f;
      }

      if (Mathf.Abs(throttleAxis) < 0.15f)
      {
        throttleAxis = 0f;
      }
    }

    carRigidbody.velocity *= 1f / (1f + (0.025f * decelerationMultiplier));
    ThrottleOff();

    if (carRigidbody.velocity.magnitude < 0.25f)
    {
      carRigidbody.velocity = Vector3.zero;
      CancelInvoke(nameof(Decelerate));
    }
  }

  private void ThrottleOff()
  {
    frontLeftCollider.motorTorque = 0;
    frontRightCollider.motorTorque = 0;
    rearLeftCollider.motorTorque = 0;
    rearRightCollider.motorTorque = 0;
  }

  public void Brakes()
  {
    frontLeftCollider.brakeTorque = brakeForce;
    frontRightCollider.brakeTorque = brakeForce;
    rearLeftCollider.brakeTorque = brakeForce;
    rearRightCollider.brakeTorque = brakeForce;
  }

  public void Handbrake()
  {
    CancelInvoke("RecoverTraction");
    driftingAxis = driftingAxis + (Time.deltaTime);
    float secureStartingPoint = driftingAxis * FLWextremumSlip * handbrakeDriftMultiplier;

    if (secureStartingPoint < FLWextremumSlip)
    {
      driftingAxis = FLWextremumSlip / (FLWextremumSlip * handbrakeDriftMultiplier);
    }
    if (driftingAxis > 1f)
    {
      driftingAxis = 1f;
    }
    if (Mathf.Abs(localVelocityX) > 2.5f)
    {
      isDrifting = true;
    }
    else
    {
      isDrifting = false;
    }
    if (driftingAxis < 1f)
    {
      FLwheelFriction.extremumSlip = FLWextremumSlip * handbrakeDriftMultiplier * driftingAxis;
      frontLeftCollider.sidewaysFriction = FLwheelFriction;

      FRwheelFriction.extremumSlip = FRWextremumSlip * handbrakeDriftMultiplier * driftingAxis;
      frontRightCollider.sidewaysFriction = FRwheelFriction;

      RLwheelFriction.extremumSlip = RLWextremumSlip * handbrakeDriftMultiplier * driftingAxis;
      rearLeftCollider.sidewaysFriction = RLwheelFriction;

      RRwheelFriction.extremumSlip = RRWextremumSlip * handbrakeDriftMultiplier * driftingAxis;
      rearRightCollider.sidewaysFriction = RRwheelFriction;
    }
    isTractionLocked = true;
    DriftCarPS();

  }

  public void DriftCarPS()
  {

    if (useEffects)
    {
      try
      {
        if (isDrifting)
        {
          RLWParticleSystem.Play();
          RRWParticleSystem.Play();
        }
        else if (!isDrifting)
        {
          RLWParticleSystem.Stop();
          RRWParticleSystem.Stop();
        }
      }
      catch (Exception ex)
      {
        Debug.LogWarning(ex);
      }

      try
      {
        if ((isTractionLocked || Mathf.Abs(localVelocityX) > 5f) && Mathf.Abs(carSpeed) > 12f)
        {
          RLWTireSkid.emitting = true;
          RRWTireSkid.emitting = true;
        }
        else
        {
          RLWTireSkid.emitting = false;
          RRWTireSkid.emitting = false;
        }
      }
      catch (Exception ex)
      {
        Debug.LogWarning(ex);
      }
    }
    else if (!useEffects)
    {
      if (RLWParticleSystem != null)
      {
        RLWParticleSystem.Stop();
      }
      if (RRWParticleSystem != null)
      {
        RRWParticleSystem.Stop();
      }
      if (RLWTireSkid != null)
      {
        RLWTireSkid.emitting = false;
      }
      if (RRWTireSkid != null)
      {
        RRWTireSkid.emitting = false;
      }
    }

  }

  public void RecoverTraction()
  {
    isTractionLocked = false;
    driftingAxis = driftingAxis - (Time.deltaTime / 1.5f);
    if (driftingAxis < 0f)
    {
      driftingAxis = 0f;
    }
    if (FLwheelFriction.extremumSlip > FLWextremumSlip)
    {
      FLwheelFriction.extremumSlip = FLWextremumSlip * handbrakeDriftMultiplier * driftingAxis;
      frontLeftCollider.sidewaysFriction = FLwheelFriction;

      FRwheelFriction.extremumSlip = FRWextremumSlip * handbrakeDriftMultiplier * driftingAxis;
      frontRightCollider.sidewaysFriction = FRwheelFriction;

      RLwheelFriction.extremumSlip = RLWextremumSlip * handbrakeDriftMultiplier * driftingAxis;
      rearLeftCollider.sidewaysFriction = RLwheelFriction;

      RRwheelFriction.extremumSlip = RRWextremumSlip * handbrakeDriftMultiplier * driftingAxis;
      rearRightCollider.sidewaysFriction = RRwheelFriction;

      Invoke("RecoverTraction", Time.deltaTime);

    }
    else if (FLwheelFriction.extremumSlip < FLWextremumSlip)
    {
      FLwheelFriction.extremumSlip = FLWextremumSlip;
      frontLeftCollider.sidewaysFriction = FLwheelFriction;

      FRwheelFriction.extremumSlip = FRWextremumSlip;
      frontRightCollider.sidewaysFriction = FRwheelFriction;

      RLwheelFriction.extremumSlip = RLWextremumSlip;
      rearLeftCollider.sidewaysFriction = RLwheelFriction;

      RRwheelFriction.extremumSlip = RRWextremumSlip;
      rearRightCollider.sidewaysFriction = RRwheelFriction;

      driftingAxis = 0f;
    }
  }

}
