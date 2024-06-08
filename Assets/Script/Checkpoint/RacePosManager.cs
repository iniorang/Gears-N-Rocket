using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RacePosManager : MonoBehaviour
{
    public TextMeshProUGUI playerPositionText; // UI Text untuk menampilkan posisi pemain

    private GameObject playerCar;
    private List<GameObject> enemyCars = new List<GameObject>();
    private int playerCheckpointIndex = 0;
    private Dictionary<GameObject, int> enemyCheckpointIndices = new Dictionary<GameObject, int>(); // Dictionary untuk menyimpan indeks checkpoint musuh
    private int totalCheckpoints;

    void Start()
    {
        // Temukan semua checkpoint di scene
        totalCheckpoints = GameObject.FindGameObjectsWithTag("Checkpoint").Length;

        // Temukan mobil pemain berdasarkan tag
        playerCar = GameObject.FindGameObjectWithTag("Player");

        // Temukan semua mobil musuh berdasarkan tag
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("AI");
        foreach (GameObject enemy in enemies)
        {
            enemyCars.Add(enemy);
            enemyCheckpointIndices.Add(enemy, 0);
        }

        UpdatePositionUI();
    }

    public void PlayerHitCheckpoint()
    {
        playerCheckpointIndex++;
        if (playerCheckpointIndex >= totalCheckpoints)
        {
            playerCheckpointIndex = totalCheckpoints - 1; // Jangan biarkan nilai melebihi jumlah checkpoint
        }
        UpdatePositionUI();
    }

    public void EnemyHitCheckpoint(GameObject enemyCar)
    {
        enemyCheckpointIndices[enemyCar]++;
        if (enemyCheckpointIndices[enemyCar] >= totalCheckpoints)
        {
            enemyCheckpointIndices[enemyCar] = totalCheckpoints - 1; // Jangan biarkan nilai melebihi jumlah checkpoint
        }
        Debug.Log(enemyCar.name + " hit checkpoint " + enemyCheckpointIndices[enemyCar]);
    }

    void UpdatePositionUI()
    {
        List<KeyValuePair<GameObject, int>> allCheckpoints = new List<KeyValuePair<GameObject, int>>
        {
            // Tambahkan checkpoint pemain
            new KeyValuePair<GameObject, int>(playerCar, playerCheckpointIndex)
        };

        // Tambahkan checkpoint semua musuh
        foreach (var enemy in enemyCheckpointIndices)
        {
            allCheckpoints.Add(enemy);
        }

        // Urutkan berdasarkan jumlah checkpoint yang telah dilewati, dari yang terbesar ke yang terkecil
        allCheckpoints.Sort((x, y) => y.Value.CompareTo(x.Value));

        // Perbarui UI posisi pemain
        for (int i = 0; i < allCheckpoints.Count; i++)
        {
            if (allCheckpoints[i].Key == playerCar)
            {
                playerPositionText.text = (i + 1).ToString();
            }
        }
    }
}
