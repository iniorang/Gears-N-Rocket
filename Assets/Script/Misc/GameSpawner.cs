using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpawner : MonoBehaviour
{
    public GameObject[] enemyCarPrefabs; // Array untuk menyimpan prefab mobil musuh
    public Transform[] spawnPoints; // Array untuk menyimpan titik spawn
    public Transform playerSpawnPoint; // Titik spawn untuk mobil pemain
    public GameObject[] playerCarPrefabs; // Array untuk menyimpan prefab mobil pemain

    void Start()
    {
        SpawnPlayerCar();
        SpawnEnemyCars();
    }

    void SpawnPlayerCar()
    {
        int selectedCarIndex = PlayerPrefs.GetInt("carIndex", 0);

        // Validasi selectedCarIndex
        if (selectedCarIndex < 0 || selectedCarIndex >= playerCarPrefabs.Length)
        {
            Debug.LogError("Selected car index is out of range. Defaulting to index 0.");
            selectedCarIndex = 0;
        }

        GameObject selectedCarPrefab = playerCarPrefabs[selectedCarIndex];
        GameObject playerCar = Instantiate(selectedCarPrefab, playerSpawnPoint.position, playerSpawnPoint.rotation);

        // Pastikan mobil pemain memiliki tag 'Player'
        playerCar.tag = "Player";
    }

    void SpawnEnemyCars()
    {
        if (enemyCarPrefabs.Length == 0)
        {
            Debug.LogError("No enemy car prefabs assigned!");
            return;
        }

        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points assigned!");
            return;
        }

        foreach (Transform spawnPoint in spawnPoints)
        {
            // Pilih prefab mobil musuh secara acak
            int randomIndex = Random.Range(0, enemyCarPrefabs.Length);
            GameObject selectedPrefab = enemyCarPrefabs[randomIndex];

            // Spawn mobil musuh di titik spawn
            Instantiate(selectedPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
