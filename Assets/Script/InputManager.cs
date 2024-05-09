using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    internal enum driver
    {
        ai, player
    }
    [SerializeField] driver mode;
    CarController car;
    [SerializeField,Range(-1,1)]private float inputX, inputY;
    private void Awake()
    {
        car = GetComponent<CarController>();
    }

    private void Update()
    {
        switch (mode)
        {
            case driver.ai:
                ai();
                break;
            case driver.player:
                player();
                break;
        }
    }

    void player()
    {
        // if (Input.GetKey(KeyCode.W))
        // {
        //     car.CancelInvoke("DecelerateCar");
        //     car.deceleratingCar = false;
        //     car.GoForward();
        // }
        // if (Input.GetKey(KeyCode.S))
        // {
        //     car.CancelInvoke("DecelerateCar");
        //     car.deceleratingCar = false;
        //     car.GoReverse();
        // }

        // if (Input.GetKey(KeyCode.A))
        // {
        //     car.TurnLeft();
        // }
        // if (Input.GetKey(KeyCode.D))
        // {
        //     car.TurnRight();
        // }
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");
        car.AiSteering(inputX);
        car.AiSpeed(inputY);
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
        // if (!Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
        // {
        //     car.ThrottleOff();
        // }
        // if (!Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.Space) && !car.deceleratingCar)
        // {
        //     car.InvokeRepeating("DecelerateCar", 0f, 0.1f);
        //     car.deceleratingCar = true;
        // }
        // if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        // {
        //     car.ResetSteeringAngle();
        // }
    }
    void ai()
    {
        car.AiSteering(inputX);
        car.AiSpeed(inputY);
    }
}
