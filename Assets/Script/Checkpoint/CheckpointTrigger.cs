using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    private RacePosManager racePositionManager;

    void Awake()
    {
        // Temukan RacePositionManager di scene
        racePositionManager = FindObjectOfType<RacePosManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            racePositionManager.PlayerHitCheckpoint();
            Debug.Log("Player hit checkpoint");
        }
        else if (other.CompareTag("AI"))
        {
            racePositionManager.EnemyHitCheckpoint(other.gameObject);
            Debug.Log(other.gameObject.name + " hit checkpoint");
        }
    }
}
