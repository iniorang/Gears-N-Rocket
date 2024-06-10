using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PosDisplay : MonoBehaviour
{
    public TextMeshProUGUI positionText;
    public TextMeshProUGUI maxCarOnScene;

    private void Update()
    {
        List<GameObject> sortedCars = CheckpointManager.Instance.GetSortedCars();
        int playerPosition = sortedCars.IndexOf(GameObject.FindGameObjectWithTag("Player")) + 1;
        positionText.text = playerPosition.ToString();
        maxCarOnScene.text = sortedCars.Count.ToString();
    }
}
