using System;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;
using System.Linq;
using System.Collections.Generic;

public class InputManager : MonoBehaviour
{
    // Kontrol pemain atau pemain
    internal enum Driver
    {
        ai, Player
    }
    [SerializeField] Driver mode = Driver.Player;
    [SerializeField] Vector2 inputVector = Vector2.zero;
    CarController car;

    [SerializeField] WaypoinNode waypoints;
    [SerializeField] List<Transform> nodes;
    [SerializeField] int currentNode;
    [SerializeField][Range(0, 10)] float offset;


    private void Awake()
    {
        car = GetComponent<CarController>();
    }

    void Start() {
        waypoints = GetComponent<WaypoinNode>();
        nodes = waypoints.node;
        currentNode = 0;
    }

    private void FixedUpdate()
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

        car.Steering(inputVector.x);
        car.MoveCar(inputVector.y);
    }
    void Ai()
    {
        inputVector.x = FindNode();
        inputVector.y = .5f;
        car.MoveCar(inputVector.y);
        car.Steering(inputVector.x);
    }

    // private float AiSteer()
    // {
    //     Vector3 relative = transform.InverseTransformPoint(currentNode.transform.position);
    //     relative /= relative.magnitude;
    //     inputVector.x = relative.x / relative.magnitude;
    //     return inputVector.x;
    // }


    private float FindNode()
    {
        if(Vector3.Distance(nodes[currentNode].position, transform.position) < offset){
            currentNode++;
            if(currentNode == nodes.Count) currentNode = 0;
        }
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        float angle = Vector3.SignedAngle(fwd,nodes[currentNode].position-transform.position,Vector3.up);
        return angle;
    }
}
