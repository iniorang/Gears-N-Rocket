using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public int damage = 10; // Jumlah kerusakan yang diakibatkan oleh rocket
    public GameObject explosionEffect; // Efek ledakan
    public float lifetime = 5f; // Waktu hidup rocket dalam detik
    public float speed = 20f; // Kecepatan rocket
    public float explosionForce = 1000f; // Gaya dorong yang diberikan saat ledakan
    public float explosionRadius = 5f; // Radius ledakan

    private Rigidbody rb;

    void Start()
    {
        // Mengambil komponen Rigidbody
        rb = GetComponent<Rigidbody>();

        // Rotasi roket agar berada dalam posisi tidur (horizontal)
        // transform.Rotate(90, 0, 0);

        // Memberikan gaya dorong pada rocket
        rb.velocity = transform.forward * speed;

        // Menghancurkan rocket setelah waktu hidup habis
        Invoke("DestroyRocket", lifetime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Buat efek ledakan di lokasi tabrakan
        Instantiate(explosionEffect, transform.position, Quaternion.identity);

        // Terapkan gaya dorong pada objek yang terkena dalam radius ledakan
        ApplyExplosionForce(collision.contacts[0].point);

        // Periksa apakah objek yang ditabrak memiliki tag "Enemy" atau "Player"
        if (collision.gameObject.CompareTag("AI") || collision.gameObject.CompareTag("Player"))
        {
            // Ambil komponen Health dari objek yang ditabrak
            if (collision.gameObject.TryGetComponent<Health>(out var health))
            {
                health.TakeDamage(damage);
            }
        }

        Debug.Log("Hit " + collision.gameObject.name);

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

    void ApplyExplosionForce(Vector3 explosionPoint)
    {
        // Cari semua collider dalam radius ledakan
        Collider[] colliders = Physics.OverlapSphere(explosionPoint, explosionRadius);

        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Terapkan gaya dorong ke objek
                rb.AddExplosionForce(explosionForce, explosionPoint, explosionRadius);
            }
        }
    }
}
