using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public int slot = 3;
    [SerializeField] PowerBox.PowerType[] powerSlot;
    [SerializeField] int selected = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        powerSlot = new PowerBox.PowerType[slot];
        for (int i = 0; i < slot; i++){
            powerSlot[i] = PowerBox.PowerType.None;
        }
    }

    public void GivePowerUp(PowerBox.PowerType pb){
        for (int i = 0; i < slot; i++){
            if (powerSlot[i] == PowerBox.PowerType.None){
                powerSlot[i] = pb;
                Debug.Log($"Collected: {pb} in slot {i + 1}");
            }
        }
        Debug.Log("No empty slots available!");
    }

    public void UsePowerUp(int index){
        if(index >= 0 && index < slot && powerSlot[index] != PowerBox.PowerType.None){
            PowerBox.PowerType pt = powerSlot[index];
            switch(pt){
                case PowerBox.PowerType.Rocket:
                Debug.Log("Launch Rocket");
                break;
                case PowerBox.PowerType.Mine:
                Debug.Log("Launch Mine");
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

    public void NextPowerUp(){
        selected = (selected + 1) % slot;
        Debug.Log($"Selected power-up slot: {selected + 1}");
    }

}
