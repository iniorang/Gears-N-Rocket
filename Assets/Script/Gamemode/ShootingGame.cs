using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Vehicles.Car;

public class ShootingGame : MonoBehaviour
{
    public float gameTime = 60f; // Waktu permainan dalam detik
    public int score = 0; // Skor awal
    public float timeLeft; // Waktu tersisa
    public GameObject gameOverText; // Referensi ke teks "Game Over"
    public GameObject Ui;
    public TextMeshProUGUI finalScoreText; // Referensi ke teks skor akhir

    void Start()
    {
        timeLeft = gameTime;
        StartCoroutine(Countdown());
        gameOverText.gameObject.SetActive(false); // Menyembunyikan teks "Game Over" di awal
        Ui.gameObject.SetActive(true);
    }

    void Update()
    {
        // Kurangi waktu yang tersisa setiap frame
        timeLeft -= Time.deltaTime;

        // Cek jika waktu habis
        if (timeLeft <= 0f)
        {
            GameOver();
        }
    }

    IEnumerator Countdown()
    {
        // Loop countdown
        while (timeLeft > 0f)
        {
            yield return null;
        }
        GameOver();
    }

    void GameOver()
    {
        // Lakukan sesuatu ketika permainan berakhir, misalnya menampilkan skor akhir
        Debug.Log("Game Over! Your Score: " + score);
        CarUserControl user = FindObjectOfType<CarUserControl>();
        user.enabled = false;
        CarAIControl aIControl = FindObjectOfType<CarAIControl>();
        aIControl.enabled = true;

        // Menampilkan teks "Game Over" dan skor akhir
        gameOverText.gameObject.SetActive(true);
        Ui.gameObject.SetActive(false);
        finalScoreText.text = score.ToString();
    }

    // Fungsi untuk menambah skor
    public void AddScore(int points)
    {
        score += points;
    }
}
