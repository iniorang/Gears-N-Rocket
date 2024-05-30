using System;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

public class InputManager : MonoBehaviour
{
    // Kontrol pemain atau pemain
    internal enum Driver
    {
        ai, Player
    }
    [SerializeField] Driver mode = Driver.Player;
    [SerializeField] Vector2 inputVector = Vector2.zero;

    //Inventori

    //Reference
    CarController car;
    PowerUpManager pm;

    //AI
    [SerializeField] WaypoinNode waypoints;
    [SerializeField] Transform currentNode;
    [SerializeField] List<Transform> nodes = new List<Transform>();
    [SerializeField][Range(0, 10)] int offset;
    [SerializeField][Range(0, 10)] float steer;

    private void Awake()
    {
        car = GetComponent<CarController>();
        pm = GetComponent<PowerUpManager>();
    }

    private void Start()
    {
        waypoints = GameObject.FindGameObjectWithTag("Path").GetComponent<WaypoinNode>();
        nodes = waypoints.node;
    }

    private void Update()
    {
        //Change AI or Player Driving this vehicle
        switch (mode)
        {
            case Driver.ai:
                Ai();
                break;
            case Driver.Player:
                Player();
                break;
        }
    }

    void Player()
    {
        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.y = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.E))
        {
            pm.UseSelectedPowerUp();
        }
        if (Input.GetKeyDown(KeyCode.Q)){
            pm.NextPowerUp();
        }

        car.Steering(inputVector.x);
        car.MoveCar(inputVector.y);
    }
    void Ai()
    {
        calculateDistance();
        inputVector.x = AiSteer();
        car.MoveCar(inputVector.y);
        car.Steering(inputVector.x);
    }

    private float AiSteer()
    {
        Vector3 relative = transform.InverseTransformPoint(currentNode.transform.position);
        relative /= relative.magnitude;
        inputVector.x = relative.x / relative.magnitude * steer;
        return inputVector.x;
    }


    private void calculateDistance()
    {
        Vector3 position = gameObject.transform.position;
        float distance = Mathf.Infinity;
        for (int i = 0; i < nodes.Count; i++)
        {
            Vector3 diff = nodes[i].position - position;
            float currentDistance = diff.magnitude;
            if (currentDistance < distance)
            {
                distance = currentDistance;
                currentNode = nodes[i];
            }
        }
    }
}
