using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public int damage = 10; // Jumlah kerusakan yang diakibatkan oleh rocket
    public GameObject explosionEffect; // Efek ledakan
    public float lifetime = 5f; // Waktu hidup rocket dalam detik
    public float speed = 20f; // Kecepatan rocket

    private Rigidbody rb;

    void Start()
    {
        // Mengambil komponen Rigidbody
        rb = GetComponent<Rigidbody>();

        // Memberikan gaya dorong pada rocket
        rb.velocity = transform.forward * speed;

        // Menghancurkan rocket setelah waktu hidup habis
        Invoke("DestroyRocket", lifetime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Periksa apakah objek yang ditabrak memiliki tag "Enemy"
        if (collision.gameObject.CompareTag("AI"))
        {
            // Ambil komponen Health dari objek yang ditabrak
            
            // Jika objek memiliki komponen Health, kurangi kesehatannya
            if (collision.gameObject.TryGetComponent<Health>(out var enemyHealth))
            {
                enemyHealth.TakeDamage(damage);
            }
        }
        // Periksa apakah objek yang ditabrak memiliki tag "Player"
        else if (collision.gameObject.CompareTag("Player"))
        {
            // Ambil komponen Health dari pemain
            
            // Jika objek memiliki komponen Health, kurangi kesehatannya
            if (collision.gameObject.TryGetComponent<Health>(out var playerHealth))
            {
                playerHealth.TakeDamage(damage);
            }
        }

        // Buat efek ledakan di lokasi tabrakan
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Debug.Log("Hit" + collision.gameObject.name);
        // Hancurkan rocket setelah menabrak
        Destroy(gameObject);
    }

    void DestroyRocket()
    {
        // Buat efek ledakan sebelum menghancurkan rocket
        Instantiate(explosionEffect, transform.position, Quaternion.identity);

        // Hancurkan rocket
        Destroy(gameObject);
    }
}
