using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RaceManager : MonoBehaviour
{
    public GameObject Cp;
    public GameObject CheckpointHolder;

    public GameObject[] Cars;
    public Transform[] CheckpointPos;
    public GameObject[] CheckpointsForEachCar;

    int totalCars;
    int totalCheckpoints;
    // Start is called before the first frame update
    void Start()
    {
        totalCars = Cars.Length;
        totalCheckpoints = CheckpointHolder.transform.childCount;
        setCheckpoints();
        SetCarPosition();
    }

    void setCheckpoints()
    {
        CheckpointPos = new Transform[totalCheckpoints];
        for (int i = 0; i < totalCheckpoints; i++)
        {
            CheckpointPos[i] = CheckpointHolder.transform.GetChild(i).transform;
        }
        CheckpointsForEachCar = new GameObject[totalCars];
        for (int i = 0; i < totalCars; i++)
        {
            CheckpointsForEachCar[i] = Instantiate(Cp, CheckpointPos[0].position, CheckpointPos[0].rotation);
            CheckpointsForEachCar[i].name = "Cp " + i;
            CheckpointsForEachCar[i].layer = 8 + i;
        }
    }
    void SetCarPosition()
    {
        for (int i = 0; i < totalCars; i++)
        {
            Cars[i].GetComponent<CarCpManager>().CarPosition = i + 1;
            Cars[i].GetComponent<CarCpManager>().CarNumber = i;
        }
    }
    public void CarCollectCP(int carNumber, int cpNumber)
    {
        CheckpointsForEachCar[carNumber].transform.position = CheckpointPos[cpNumber].transform.position;
        CheckpointsForEachCar[carNumber].transform.rotation = CheckpointPos[cpNumber].transform.rotation;
    }

    void ComparePosition(int carNumber)
    {
        if (Cars[carNumber].GetComponent<CarCpManager>().CarPosition > 1)
        {
            GameObject currentCar = Cars[carNumber];
            int currentCarPos = currentCar.GetComponent<CarCpManager>().CarPosition;
            int currentCarcp = currentCar.GetComponent<CarCpManager>().cpCrossed;

            GameObject carInFront = null;
            int carInFrontPos = 0;
            int carInFrontCp = 0;
            for (int i = 0; i < totalCars; i++)
            {
                if (Cars[i].GetComponent<CarCpManager>().CarPosition == currentCarPos - 1)
                {
                    carInFront = Cars[i];
                    carInFrontCp = carInFront.GetComponent<CarCpManager>().cpCrossed;
                    carInFrontPos = carInFront.GetComponent<CarCpManager>().CarPosition;
                    break;
                }
            }
            if (currentCarcp > carInFrontCp)
            {
                currentCar.GetComponent<CarCpManager>().CarPosition = currentCarPos - 1;
                carInFront.GetComponent<CarCpManager>().CarPosition = carInFrontPos + 1;

                Debug.Log("Car " + carNumber + " has overtaken " + carInFront.GetComponent<CarCpManager>().CarNumber);
            }
        }
    }
}
