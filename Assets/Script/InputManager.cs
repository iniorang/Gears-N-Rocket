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
    [SerializeField] Transform currentNode;
    [SerializeField] List<Transform> nodes = new List<Transform>();
    [SerializeField][Range(0, 10)] int offset;
    [SerializeField][Range(0, 10)] float steer;

    private void Awake()
    {
        car = GetComponent<CarController>();
    }

    private void Start() {
        waypoints = GameObject.FindGameObjectWithTag("Path").GetComponent<WaypoinNode>();
        nodes = waypoints.node;
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
        calculateDistance();
        inputVector.x = AiSteer();
        inputVector.y = .5f;
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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if (currentNode != null)
            Gizmos.DrawSphere(currentNode.transform.position, 0.5f);
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
                distance = currentDistance; // Update the minimum distance
                currentNode = nodes[i];
            }
        }
    }
}
