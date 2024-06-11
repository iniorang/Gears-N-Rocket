using UnityEngine;

public class LapTimer : MonoBehaviour
{
    // public float BestLapTime { get; private set; } = Mathf.Infinity;
    // public float LastLapTime { get; private set; } = 0f;
    // public float CurrentLapTime { get; private set; } = 0f;
    // public int CurrentLap { get; private set; } = 0;
    // public int MaxLap { get; private set; } = 3;

    // private float lapTimer;
    // private int lastCheckPassed = 0;
    // private CheckpointManager checkpointManager;

    // private void Awake()
    // {
    //     checkpointManager = CheckpointManager.Instance;
    // }

    // private void FixedUpdate()
    // {
    //     if (lapTimer > 0)
    //     {
    //         CurrentLapTime = Time.time - lapTimer;
    //     }
    // }

    // public void StartLap()
    // {
    //     Debug.Log("Start New Lap");
    //     CurrentLap++;
    //     lastCheckPassed = 0;
    //     lapTimer = Time.time;
    // }

    // public void EndLap()
    // {
    //     LastLapTime = Time.time - lapTimer;
    //     BestLapTime = Mathf.Min(LastLapTime, BestLapTime);
    //     Debug.Log("Lap " + CurrentLap + " Time: " + LastLapTime + " Best Time: " + BestLapTime);

    //     if (CurrentLap < MaxLap)
    //     {
    //         lapTimer = Time.time;
    //     }
    // }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (!checkpointManager.IsCheckpointLayer(other.gameObject.layer))
    //     {
    //         return;
    //     }

    //     int checkpointIndex = checkpointManager.GetCheckpointIndex(other.transform);

    //     if (checkpointIndex == (lastCheckPassed + 1) % checkpointManager.GetCheckpointCount())
    //     {
    //         lastCheckPassed = checkpointIndex;

    //         if (checkpointIndex == 0)
    //         {
    //             checkpointManager.CheckpointReached(gameObject, checkpointIndex);
    //             EndLap();
    //             if (CurrentLap < MaxLap)
    //             {
    //                 StartLap();
    //             }
    //         }
    //         else
    //         {
    //             checkpointManager.CheckpointReached(gameObject, checkpointIndex);
    //         }
    //     }
    // }

    // public void OnLapCompleted()
    // {
    //     EndLap();
    //     if (CurrentLap < MaxLap)
    //     {
    //         StartLap();
    //     }
    // }
}
