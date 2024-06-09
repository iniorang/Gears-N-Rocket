using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrackSelection : MonoBehaviour
{
    public void OvalTrack(){
        SceneManager.LoadScene(3);
    }

    public void RacewayTrack(){
        SceneManager.LoadScene(2);
    }
    public void FigureEight(){
        SceneManager.LoadScene(4);
    }

    public void CarSelection(){
        SceneManager.LoadScene(0);
    }
}
