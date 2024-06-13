using System.Collections;
using UnityEngine;
using TMPro;
using UnityStandardAssets.Vehicles.Car;

public class Countdown : MonoBehaviour
{
    public GameObject CountDown;
    public AudioSource GetReady;
    public AudioSource GoAudio;
    public GameObject LapTimer;
    public GameObject Blocker;
    

    void Start()
    {
        StartCoroutine(StartCountDown());
    }

    IEnumerator StartCountDown()
    {
        yield return new WaitForSeconds(.5f);
        TextMeshProUGUI textComponent = CountDown.GetComponent<TextMeshProUGUI>();
        textComponent.text = "3";
        GetReady.Play();
        CountDown.SetActive(true);
        yield return new WaitForSeconds(1);
        CountDown.SetActive(false);
        textComponent.text = "2";
        GetReady.Play();
        CountDown.SetActive(true);
        yield return new WaitForSeconds(1);
        CountDown.SetActive(false);
        textComponent.text = "1";
        GetReady.Play();
        CountDown.SetActive(true);
        yield return new WaitForSeconds(1);
        CountDown.SetActive(false);
        Blocker.SetActive(false);
        GoAudio.Play();
    }
}
