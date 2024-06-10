using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerFocus : MonoBehaviour
{
    private CinemachineFreeLook freeLookCamera;

    void Start()
    {
        freeLookCamera = GetComponent<CinemachineFreeLook>();
        if (freeLookCamera == null)
        {
            Debug.LogError("No CinemachineFreeLook component found on this GameObject.");
            return;
        }

        // Tambahkan penundaan sebelum mencoba menemukan mobil pemain
        StartCoroutine(FindAndFollowPlayerCar());
    }

    IEnumerator FindAndFollowPlayerCar()
    {
        // Tunggu sebentar untuk memastikan mobil pemain di-spawn
        yield return new WaitForSeconds(0.5f);

        GameObject playerCar = GameObject.FindGameObjectWithTag("Player");

        if (playerCar != null)
        {
            freeLookCamera.Follow = playerCar.transform;
            freeLookCamera.LookAt = playerCar.transform;
        }
        else
        {
            Debug.LogError("Player car not found. Make sure the player car is tagged with 'Player'.");
        }
    }
}
