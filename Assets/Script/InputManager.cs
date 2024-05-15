using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    internal enum Driver
    {
        ai, player
    }
    [SerializeField] Driver mode = Driver.player;
    CarController car;
    [SerializeField, Range(-1, 1)] private float inputX, inputY;

    //Time and Lap
    public float BestLapTime { get; private set; } = Mathf.Infinity;
    public float LastLapTime { get; private set; } = 0f;
    public float CurrentLapTime { get; private set; } = 0f;
    public int CurrentLap { get; private set; } = 0;

    private float lapTimer;

    //Cekpoint
    private int lastCheckPassed = 0;
    private Transform checkpointParent;
    private int checkCount;
    private int checkLayer;


    private void Awake()
    {
        car = GetComponent<CarController>();
        FindCheckpoint();
    }

    private void Update()
    {
        CurrentLapTime = lapTimer > 0 ? Time.time - lapTimer : 0;

        //Change AI or Player Driving this vehicle
        switch (mode)
        {
            case Driver.ai:
                Ai();
                break;
            case Driver.player:
                player();
                break;
        }
    }

    void player()
    {
        if (Input.GetKey(KeyCode.W))
        {
            car.CancelInvoke("DecelerateCar");
            car.deceleratingCar = false;
            car.GoForward();
        }
        if (Input.GetKey(KeyCode.S))
        {
            car.CancelInvoke("DecelerateCar");
            car.deceleratingCar = false;
            car.GoReverse();
        }

        if (Input.GetKey(KeyCode.A))
        {
            car.TurnLeft();
        }
        if (Input.GetKey(KeyCode.D))
        {
            car.TurnRight();
        }
        if (Input.GetKey(KeyCode.Space))
        {
            car.CancelInvoke("DecelerateCar");
            car.deceleratingCar = false;
            car.Handbrake();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            car.RecoverTraction();
        }
        if (!Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
        {
            car.ThrottleOff();
        }
        if (!Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.Space) && !car.deceleratingCar)
        {
            car.InvokeRepeating("DecelerateCar", 0f, 0.1f);
            car.deceleratingCar = true;
        }
        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            car.ResetSteeringAngle();
        }
        // inputX = Input.GetAxis("Horizontal");
        // inputY = Input.GetAxis("Vertical");
        // car.AiSteering(inputX);
        // car.AiSpeed(inputY);
    }
    void Ai()
    {
        car.AiSteering(inputX);
        car.AiSpeed(inputY);
    }

    void FindCheckpoint()
    {
        checkpointParent = GameObject.Find("Checkpoint").transform;
        checkCount = checkpointParent.childCount;
        checkLayer = LayerMask.NameToLayer("Checkpoint");
    }

    void StartLap()
    {
        Debug.Log("Start New Lap");
        CurrentLap++;
        lastCheckPassed = 1;
        lapTimer = Time.time;
    }

    void EndLap()
    {
        LastLapTime = Time.time - lapTimer;
        BestLapTime = Mathf.Min(LastLapTime, BestLapTime);
        Debug.Log("Lap " + CurrentLap + " Time: " + LastLapTime + " Best Time: " + BestLapTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != checkLayer) return;

        if (other.gameObject.name == "1")
        {
            if (lastCheckPassed == checkCount)
            {
                EndLap();
            }
            if (CurrentLap == 0 || lastCheckPassed == checkCount)
            {
                StartLap();
            }
            return;
        }

        if (other.gameObject.name == (lastCheckPassed + 1).ToString()) lastCheckPassed++;
    }
}
