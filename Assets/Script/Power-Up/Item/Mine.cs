using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class Mine : MonoBehaviour
{
    public int damage = 10; // Jumlah kerusakan yang diakibatkan oleh mine
    public GameObject explosionEffect; // Efek ledakan
    public AudioClip explosionSound; // Suara ledakan
    public float slowDuration = 3f; // Durasi pemain atau musuh melambat
    public float slowAmount = 0.5f; // Pengurangan kecepatan (50%)
    public float bounceForce = 500f; // Gaya pantulan
    public int scoreValue = 100; // Nilai skor yang diberikan saat mengenai musuh

    void OnTriggerEnter(Collider other)
    {
        // Buat efek ledakan di lokasi tabrakan
        Instantiate(explosionEffect, transform.position, Quaternion.identity);

        // Mainkan suara ledakan
        AudioSource.PlayClipAtPoint(explosionSound, transform.position);

        // Terapkan pelambatan dan pantulan
        if (other.CompareTag("AI") || other.CompareTag("Player"))
        {
            if (other.CompareTag("AI"))
            {
                // Tambahkan skor jika mengenai musuh
                FindObjectOfType<ShootingGame>().AddScore(scoreValue);
            }

            // Ambil komponen Rigidbody dari objek yang ditabrak
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Terapkan gaya pantulan ke objek
                rb.AddExplosionForce(bounceForce, transform.position, 1f);

                // Terapkan pelambatan
                StartCoroutine(ApplySlowEffect(other.gameObject));
            }
        }

        // Hancurkan ranjau setelah meledak
        Destroy(gameObject);
    }

    private IEnumerator ApplySlowEffect(GameObject target)
    {
        CarController vehicle = target.GetComponent<CarController>();
        if (vehicle != null)
        {
            float originalSpeed = vehicle.m_Topspeed;
            vehicle.m_Topspeed *= slowAmount;

            yield return new WaitForSeconds(slowDuration);

            vehicle.m_Topspeed = originalSpeed;
        }
    }
}
