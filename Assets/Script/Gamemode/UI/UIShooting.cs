using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIShooting : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI timeText;

    private ShootingGame shootingGame;
    private Health playerHealth;

    void Start()
    {
        shootingGame = FindObjectOfType<ShootingGame>();
        playerHealth = FindObjectOfType<Health>();
    }

    void Update()
    {
        if (shootingGame != null)
        {
            // Ensure positive time value
            float timeLeft = Mathf.Max(0, shootingGame.timeLeft);

            // Format time string without milliseconds (using "f" format specifier)
            timeText.text = timeLeft.ToString("f0"); // Example output: 123 (for 123.456 seconds)

            scoreText.text = shootingGame.score.ToString();
        }

        if (playerHealth != null)
        {
            healthText.text = playerHealth.currentHealth.ToString();
        }
    }
}
