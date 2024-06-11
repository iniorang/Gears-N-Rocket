using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CheckpointManager : MonoBehaviour
{
    //     public static CheckpointManager Instance;

    //     private Dictionary<GameObject, int> checkpointPositions = new Dictionary<GameObject, int>();
    //     private Dictionary<GameObject, int> lapCounts = new Dictionary<GameObject, int>();
    //     private Dictionary<GameObject, LapTimer> lapTimers = new Dictionary<GameObject, LapTimer>();

    //     private List<Transform> checkpoints = new List<Transform>();
    //     private int checkpointLayer;

    //     private void Awake()
    //     {
    //         if (Instance == null)
    //         {
    //             Instance = this;
    //             DontDestroyOnLoad(gameObject);  // Persist through scene loads
    //         }
    //         else
    //         {
    //             Destroy(gameObject);
    //         }
    //     }

    //     private void Start()
    //     {
    //         RegisterCheckpoints();
    //         RegisterAllCars();
    //         SetCheckpointLayer();
    //     }

    //     private void RegisterCheckpoints()
    //     {
    //         GameObject[] checkpointObjects = GameObject.FindGameObjectsWithTag("Checkpoint");
    //         foreach (GameObject checkpointObject in checkpointObjects)
    //         {
    //             checkpoints.Add(checkpointObject.transform);
    //         }
    //     }

    //     private void RegisterAllCars()
    //     {
    //         RegisterCars("Player");
    //         RegisterCars("AI");
    //     }

    //     private void SetCheckpointLayer()
    //     {
    //         if (checkpoints.Count > 0)
    //         {
    //             checkpointLayer = checkpoints[0].gameObject.layer;
    //         }
    //     }

    //     private void RegisterCars(string tag)
    //     {
    //         foreach (GameObject car in GameObject.FindGameObjectsWithTag(tag))
    //         {
    //             RegisterCar(car);
    //         }
    //     }

    //     private void RegisterCar(GameObject car)
    //     {
    //         if (!checkpointPositions.ContainsKey(car))
    //         {
    //             checkpointPositions[car] = 0;
    //             lapCounts[car] = 0;  // Start from lap 0
    //             lapTimers[car] = car.GetComponent<LapTimer>();
    //         }
    //     }

    //     public void CheckpointReached(GameObject car, int checkpointIndex)
    //     {
    //         if (!checkpointPositions.ContainsKey(car))
    //         {
    //             RegisterCar(car);
    //         }

    //         int expectedCheckpointIndex = (checkpointPositions[car] + 1) % checkpoints.Count;
    //         if (checkpointIndex == expectedCheckpointIndex)
    //         {
    //             checkpointPositions[car] = checkpointIndex;

    //             if (checkpointIndex == 0)
    //             {
    //                 lapCounts[car]++;
    //                 lapTimers[car].OnLapCompleted();
    //             }

    //             UpdatePositions();
    //         }
    //     }

    //     private void UpdatePositions()
    //     {
    //         List<GameObject> cars = new List<GameObject>(checkpointPositions.Keys);
    //         cars.Sort((car1, car2) =>
    //         {
    //             int lapCountComparison = lapCounts[car2].CompareTo(lapCounts[car1]);
    //             if (lapCountComparison == 0)
    //             {
    //                 return checkpointPositions[car2].CompareTo(checkpointPositions[car1]);
    //             }
    //             return lapCountComparison;
    //         });

    //         for (int i = 0; i < cars.Count; i++)
    //         {
    //             Debug.Log($"Position {i + 1}: {cars[i].name} - Lap: {lapCounts[cars[i]]}, Checkpoint: {checkpointPositions[cars[i]]}");
    //         }
    //     }

    //     public List<GameObject> GetSortedCars()
    //     {
    //         List<GameObject> cars = new List<GameObject>(checkpointPositions.Keys);
    //         cars.Sort((car1, car2) =>
    //         {
    //             int lapCountComparison = lapCounts[car2].CompareTo(lapCounts[car1]);
    //             if (lapCountComparison == 0)
    //             {
    //                 return checkpointPositions[car2].CompareTo(checkpointPositions[car1]);
    //             }
    //             return lapCountComparison;
    //         });
    //         return cars;
    //     }

    //     public int GetCheckpointIndex(Transform checkpoint)
    //     {
    //         if (int.TryParse(checkpoint.gameObject.name, out int checkpointIndex))
    //         {
    //             return checkpointIndex;
    //         }
    //         else
    //         {
    //             Debug.LogError("Checkpoint name is not a valid integer: " + checkpoint.gameObject.name);
    //             return -1;
    //         }
    //     }

    //     public int GetCheckpointCount()
    //     {
    //         return checkpoints.Count;
    //     }

    //     public bool IsCheckpointLayer(int layer)
    //     {
    //         return layer == checkpointLayer;
    //     }

    public List<Transform> checkpoints; // List checkpoint
    private List<Transform> players = new List<Transform>(); // List pemain
    private List<Transform> enemies = new List<Transform>(); // List musuh
    public Transform leader; // Pemimpin balap
    public bool raceOver = false; // Status balapan selesai

    void Start()
    {
        StartCoroutine(StartRace());
    }

    IEnumerator StartRace()
    {
        yield return new WaitForSeconds(0.5f); // Delay sebelum memulai balapan

        FindPlayersAndEnemies();

        while (!raceOver)
        {
            UpdateLeader();
            CheckRaceStatus();
            yield return null;
        }
    }

    void FindPlayersAndEnemies()
    {
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject playerObject in playerObjects)
        {
            players.Add(playerObject.transform);
        }

        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("AI");
        foreach (GameObject enemyObject in enemyObjects)
        {
            enemies.Add(enemyObject.transform);
        }
    }

    void UpdateLeader()
    {
        float closestDistance = Mathf.Infinity;
        Transform closestEntity = null;

        foreach (Transform player in players)
        {
            float distanceToLeader = DistanceToLeader(player.position);
            if (distanceToLeader < closestDistance)
            {
                closestDistance = distanceToLeader;
                closestEntity = player;
            }
        }

        foreach (Transform enemy in enemies)
        {
            float distanceToLeader = DistanceToLeader(enemy.position);
            if (distanceToLeader < closestDistance)
            {
                closestDistance = distanceToLeader;
                closestEntity = enemy;
            }
        }

        leader = closestEntity;
    }

    float DistanceToLeader(Vector3 position)
    {
        float distance = 0f;

        for (int i = 0; i < checkpoints.Count; i++)
        {
            if (i == 0)
            {
                distance += Vector3.Distance(position, checkpoints[i].position);
            }
            else
            {
                float checkpointDistance = Vector3.Distance(checkpoints[i].position, checkpoints[i - 1].position);
                distance += checkpointDistance;
                if (leader != null && leader.position == checkpoints[i].position)
                {
                    break;
                }
            }
        }

        return distance;
    }

    void CheckRaceStatus()
    {
        foreach (Transform player in players)
        {
            float distanceToFinish = DistanceToLeader(player.position);
            if (distanceToFinish < 1f) // Jarak ke garis finish
            {
                raceOver = true;
                Debug.Log("Player Wins!");
                return;
            }
        }

        foreach (Transform enemy in enemies)
        {
            float distanceToFinish = DistanceToLeader(enemy.position);
            if (distanceToFinish < 1f && enemy == leader) // Jarak ke garis finish dan pemimpin
            {
                raceOver = true;
                Debug.Log("Enemy Wins!");
                return;
            }
        }
    }
}
