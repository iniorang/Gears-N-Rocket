using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhoInput : MonoBehaviour
{
    public enum Driver {Player, Ai};
    public Driver TypeDriver = Driver.Player;

    CarUserControl playerControl;
    CarAIControl aIControl;
    WaypointProgressTracker tracker;
    // Start is called before the first frame update
    void Awake()
    {
        aIControl = GetComponent<CarAIControl>();
        tracker = GetComponent<WaypointProgressTracker>();
        playerControl = GetComponent<CarUserControl>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (TypeDriver){
            case Driver.Player:
            aIControl.enabled = false;
            playerControl.enabled = true;
            break;
            case Driver.Ai:
            aIControl.enabled = true;
            playerControl.enabled = false;
            break;
        }
    }
}
