using System;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;
using System.Linq;

public class InputManager : MonoBehaviour
{
    // Kontrol pemain atau pemain
    internal enum Driver
    {
        ai, player
    }
    [SerializeField] Driver mode = Driver.player;
    CarController car;
    [SerializeField]Vector2 inputVector = Vector2.zero;

    private void Awake()
    {
        car = GetComponent<CarController>();
    
    }

    private void FixedUpdate()
    {
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
        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.y = Input.GetAxis("Vertical");

        car.Steering(inputVector.x);
        car.MoveCar(inputVector.y);
    }
    void Ai()
    {
        car.Steering(inputVector.x);
        // car.AiSpeed(inputVector.y);
    }
}
