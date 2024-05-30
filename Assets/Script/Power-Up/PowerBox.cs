using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBox : MonoBehaviour
{
    public enum PowerType {None, Rocket, Repair, Shield, Mine, Nitro}
    public PowerType selectedType;

    void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent<PowerUpManager>(out var pm))
        {
            pm.GivePowerUp(selectedType);
            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
            Invoke(nameof(RespawnBox), 5f);
        }
    }

    private void RespawnBox()
    {
        GetComponent<Renderer>().enabled = true;
        GetComponent<Collider>().enabled = true;
    }
}
