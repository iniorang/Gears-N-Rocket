using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyQuickly : MonoBehaviour
{
    public float lifetime = 2f; // Duration of the explosion effect in seconds

    void Start()
    {
        // Destroy the explosion effect object after the specified lifetime
        Destroy(gameObject, lifetime);
    }
}
