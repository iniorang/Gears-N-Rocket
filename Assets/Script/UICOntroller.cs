using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject RacePanel;

    public TextMeshProUGUI CurrentTimeUi;
    public TextMeshProUGUI CurrentLapUi;
    public TextMeshProUGUI MaxLapUi;
    public TextMeshProUGUI CurrentPosUi;
    public TextMeshProUGUI MaxOppentUi;

    public RaceTime UpdateUI;
    private float CurrentTime;
    private int CurrentLap = -1;
    private float MaxLap;
    private float CurrentPos;
    private float MaxOppent;

    void Start(){
        UpdateUI = GameObject.FindAnyObjectByType<RaceTime>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if(UpdateUI == null)
            return;

        if(UpdateUI.CurrentLap != CurrentLap)
        {
            CurrentLap = UpdateUI.CurrentLap;
            CurrentLapUi.text = $"{CurrentLap}";
            // CurrentLapUi.text = "Test";
        }

        if(UpdateUI.CurrentLapTime != CurrentTime)
        {
            CurrentTime = UpdateUI.CurrentLapTime;
            CurrentTimeUi.text = $"{(int)CurrentTime / 60} : {CurrentTime % 60:00.000}";
        }

        if(UpdateUI.MaxLap != MaxLap)
        {
            MaxLap = UpdateUI.MaxLap;
            MaxLapUi.text = $"{MaxLap}";
        }
    }
}
