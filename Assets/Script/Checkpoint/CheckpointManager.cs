using UnityEngine;
using System.Collections.Generic;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance;

    private Dictionary<GameObject, int> checkpointPositions = new Dictionary<GameObject, int>();
    private Dictionary<GameObject, int> lapCounts = new Dictionary<GameObject, int>();
    private Dictionary<GameObject, LapTimer> lapTimers = new Dictionary<GameObject, LapTimer>();

    private List<Transform> checkpoints = new List<Transform>();
    private int checkpointLayer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Mencari semua checkpoint di scene
        GameObject[] checkpointObjects = GameObject.FindGameObjectsWithTag("Checkpoint");
        foreach (GameObject checkpointObject in checkpointObjects)
        {
            checkpoints.Add(checkpointObject.transform);
        }

        // Mendaftarkan semua mobil di scene
        RegisterCars("Player");
        RegisterCars("AI");

        // Set layer for checkpoints
        if (checkpoints.Count > 0)
        {
            checkpointLayer = checkpoints[0].gameObject.layer;
        }
    }

    private void RegisterCars(string tag)
    {
        foreach (GameObject car in GameObject.FindGameObjectsWithTag(tag))
        {
            RegisterCar(car);
        }
    }

    private void RegisterCar(GameObject car)
    {
        if (!checkpointPositions.ContainsKey(car))
        {
            checkpointPositions[car] = 0;
            lapCounts[car] = 0;
            lapTimers[car] = car.GetComponent<LapTimer>();
        }
    }

    public void CheckpointReached(GameObject car, int checkpointIndex)
    {
        if (!checkpointPositions.ContainsKey(car))
        {
            RegisterCar(car);
        }

        if (checkpointIndex == (checkpointPositions[car] + 1) % checkpoints.Count)
        {
            checkpointPositions[car] = checkpointIndex;

            if (checkpointIndex == 0)
            {
                lapCounts[car]++;
                lapTimers[car].OnLapCompleted();
            }

            UpdatePositions();
        }
    }

    private void UpdatePositions()
    {
        List<GameObject> cars = new List<GameObject>(checkpointPositions.Keys);
        cars.Sort((car1, car2) =>
        {
            int lapCountComparison = lapCounts[car2].CompareTo(lapCounts[car1]);
            if (lapCountComparison == 0)
            {
                return checkpointPositions[car2].CompareTo(checkpointPositions[car1]);
            }
            return lapCountComparison;
        });

        for (int i = 0; i < cars.Count; i++)
        {
            Debug.Log($"Position {i + 1}: {cars[i].name}");
        }

        for (int i = 0; i < cars.Count; i++)
        {
            Debug.Log($"{cars[i].name} - Lap: {lapCounts[cars[i]]}, Checkpoint Index: {checkpointPositions[cars[i]]}");
        }
    }

    public List<GameObject> GetSortedCars()
    {
        List<GameObject> cars = new List<GameObject>(checkpointPositions.Keys);
        cars.Sort((car1, car2) =>
        {
            int lapCountComparison = lapCounts[car2].CompareTo(lapCounts[car1]);
            if (lapCountComparison == 0)
            {
                return checkpointPositions[car2].CompareTo(checkpointPositions[car1]);
            }
            return lapCountComparison;
        });
        return cars;
    }

    public int GetCheckpointIndex(Transform checkpoint)
    {
        if (int.TryParse(checkpoint.gameObject.name, out int checkpointIndex))
        {
            return checkpointIndex;
        }
        else
        {
            Debug.LogError("Checkpoint name is not a valid integer: " + checkpoint.gameObject.name);
            return -1;
        }
    }

    public int GetCheckpointCount()
    {
        return checkpoints.Count;
    }

    public bool IsCheckpointLayer(int layer)
    {
        return layer == checkpointLayer;
    }
}
