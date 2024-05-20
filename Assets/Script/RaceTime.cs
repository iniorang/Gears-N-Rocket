using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceTime : MonoBehaviour
{
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

    void Awake()
    {
        checkpointParent = GameObject.Find("Checkpoint").transform;
        checkCount = checkpointParent.childCount;
        checkLayer = LayerMask.NameToLayer("Checkpoint");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CurrentLapTime = lapTimer > 0 ? Time.time - lapTimer : 0;
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
        if (other.gameObject.layer != checkLayer)
        {
            return;
        }

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

        if (other.gameObject.name == (lastCheckPassed + 1).ToString()) {
            lastCheckPassed++;
            Debug.LogWarning("Checkpoint");
        }
    }
}
