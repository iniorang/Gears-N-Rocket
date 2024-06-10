using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int checkpointIndex; // Index dari checkpoint ini

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("Player") || other.CompareTag("AI"))
    //     {
    //         CheckpointManager.Instance.CheckpointReached(other.gameObject, checkpointIndex);
    //     }
    // }
}
