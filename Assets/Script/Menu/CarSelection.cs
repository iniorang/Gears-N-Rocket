using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CarSelection : MonoBehaviour
{
    public GameObject[] cars;
    int MaxCar;
    public Button NextButton;
    public Button PrevButton;
    int index;

    // Start is called before the first frame update
    void Start()
    {
        index = PlayerPrefs.GetInt("carIndex");
        MaxCar = cars.Length;

        for (int i = 0; i < MaxCar; i++) {
            cars[i].SetActive(false);
        }
        cars[index].SetActive(true);
    }

    // Update is called once per frame
    // void Update()
    // {
    //     NextButton.interactable = index < MaxCar - 1;
    //     PrevButton.interactable = index > 0;
    // }

    public void NextCar()
    {
        if (index < MaxCar - 1) {
            index++;
            UpdateCarSelection();
        }
        else{
            index = 0;
            UpdateCarSelection();
        }
    }

    public void PrevCar()
    {
        if (index > 0) {
            index--;
            UpdateCarSelection();
        }else{
            index = MaxCar - 1;
            UpdateCarSelection();
        }
    }

    private void UpdateCarSelection()
    {
        for (int i = 0; i < cars.Length; i++) {
            cars[i].SetActive(false);
        }
        cars[index].SetActive(true);
        PlayerPrefs.SetInt("carIndex", index);
        PlayerPrefs.Save();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
