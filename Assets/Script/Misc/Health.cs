using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100; // Kesehatan maksimal
    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth; // Atur kesehatan awal ke nilai maksimal
    }

    // Fungsi untuk menerima damage
    public void TakeDamage(int amount)
    {
        currentHealth -= amount; // Kurangi kesehatan saat menerima damage

        // Periksa jika kesehatan kurang dari atau sama dengan 0
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Fungsi untuk menambah kesehatan
    public void Heal(int amount)
    {
        currentHealth += amount; // Tambahkan kesehatan
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth; // Pastikan kesehatan tidak melebihi maksimal
        }
    }

    // Fungsi untuk menangani kematian
    void Die()
    {
        // Lakukan sesuatu saat objek mati, seperti mematikan game objek
        Debug.Log(gameObject.name + " has died!");
        Destroy(gameObject); // Hancurkan objek ini
    }
}
