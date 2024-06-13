using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public int slot = 3;
    [SerializeField] public PowerBox.PowerType[] powerSlot;
    [SerializeField] int selected = 0;
    public Transform launchPosition;

    // Power-up prefabs
    public GameObject rocketPrefab;
    public GameObject minePrefab;

    // Start is called before the first frame update
    void Start()
    {
        powerSlot = new PowerBox.PowerType[slot];
        for (int i = 0; i < slot; i++)
        {
            powerSlot[i] = PowerBox.PowerType.None;
        }
    }

    public void GivePowerUp(PowerBox.PowerType pb)
    {
        bool given = false; // Flag to track whether power-up has been given

        for (int i = 0; i < slot; i++)
        {
            if (powerSlot[i] == PowerBox.PowerType.None && !given) // Check if slot is empty and power-up hasn't been given yet
            {
                powerSlot[i] = pb;
                Debug.Log($"Collected: {pb} in slot {i + 1}");
                given = true; // Set flag to true to indicate power-up has been given
            }
        }

        if (!given) // If power-up hasn't been given (all slots are occupied)
        {
            Debug.Log("No empty slots available!");
        }
    }

    public void UsePowerUp(int index)
    {
        if (index >= 0 && index < slot && powerSlot[index] != PowerBox.PowerType.None)
        {
            PowerBox.PowerType pt = powerSlot[index];
            switch (pt)
            {
                case PowerBox.PowerType.Rocket:
                    Instantiate(rocketPrefab, launchPosition.position, launchPosition.rotation);
                    break;
                case PowerBox.PowerType.Mine:
                    Instantiate(minePrefab, launchPosition.position - new Vector3(0, 0f, 10f), launchPosition.rotation);
                    break;
                case PowerBox.PowerType.Repair:
                    Debug.Log("Launch Repair");
                    break;
                case PowerBox.PowerType.Shield:
                    Debug.Log("Activate Shield");
                    break;
                case PowerBox.PowerType.Nitro:
                    Debug.Log("Activate Nitro");
                    break;
            }
            powerSlot[index] = PowerBox.PowerType.None;
            Debug.Log($"Used: {pt} in slot {index + 1}");
        }
    }

    public void UseSelectedPowerUp()
    {
        UsePowerUp(selected);
    }

    public void NextPowerUp()
    {
        selected = (selected + 1) % slot;
        Debug.Log($"Selected power-up slot: {selected + 1}");
    }
}
