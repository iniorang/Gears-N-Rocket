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
    public float slowAmount = 0.5f; // Jumlah pelambatan (0.5 berarti kecepatan menjadi 50%)
    public float bounceForce = 500f; // Gaya pantulan

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("AI"))
        {
            // Buat efek ledakan di lokasi tabrakan
            Instantiate(explosionEffect, transform.position, Quaternion.identity);

            // Mainkan suara ledakan
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);

            // Ambil komponen Health dari objek yang ditabrak
            if (other.TryGetComponent<Health>(out var health))
            {
                health.TakeDamage(damage);
            }

            // Ambil komponen Rigidbody dari objek yang ditabrak
            if (other.TryGetComponent<Rigidbody>(out var rb))
            {
                rb.AddExplosionForce(bounceForce, transform.position, 5f);
            }

            // Perlambat objek yang ditabrak
            if (other.TryGetComponent<CarController>(out var carController))
            {
                StartCoroutine(SlowDown(carController));
            }

            // Hancurkan mine setelah ledakan
            Destroy(gameObject);
        }
    }

    private IEnumerator SlowDown(CarController carController)
    {
        carController.m_Topspeed *= slowAmount;
        yield return new WaitForSeconds(slowDuration);
        carController.m_Topspeed /= slowAmount;
    }
}
