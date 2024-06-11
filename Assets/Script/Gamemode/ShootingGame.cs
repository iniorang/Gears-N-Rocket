using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingGame : MonoBehaviour
{
  public float gameTime = 60f; // Waktu permainan dalam detik
    public int score = 0; // Skor awal
    public float timeLeft; // Waktu tersisa

    void Start()
    {
        timeLeft = gameTime;
        StartCoroutine(Countdown());
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
    }

    // Fungsi untuk menambah skor
    public void AddScore(int points)
    {
        score += points;
    }
}
