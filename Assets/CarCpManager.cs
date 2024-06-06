using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCpManager : MonoBehaviour
{
    public int CarNumber;
    public int cpCrossed;
    public int CarPosition;

    public RaceManager raceManager;

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("CP")){
            cpCrossed+=1;
            raceManager.CarCollectCP(CarNumber,cpCrossed);
        }
    }
}
