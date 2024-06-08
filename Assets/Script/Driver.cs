using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class Driver : MonoBehaviour
{
    public enum TypeDriver
    {
        Player,
        Ai
    }
    public TypeDriver type;
    CarUserControl UserControl;
    CarAIControl AIControl;
    void Awake()
    {
        UserControl = gameObject.GetComponent<CarUserControl>();
        AIControl = gameObject.GetComponent<CarAIControl>();
        switch (type)
        {
            case TypeDriver.Player:
                gameObject.tag = "Player";
                this.AddComponent<RaceTime>();
                break;
            case TypeDriver.Ai:
                gameObject.tag = "AI";
                break;
        }
    }

    void Update()
    {
        switch (type)
        {
            case TypeDriver.Player:
                UserControl.enabled = true;
                AIControl.enabled = false;
                break;
            case TypeDriver.Ai:
                UserControl.enabled = false;
                AIControl.enabled = true;
                break;
        }
    }
}
