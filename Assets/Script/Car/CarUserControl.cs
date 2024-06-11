using System;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof(CarController))]
    public class CarUserControl : MonoBehaviour
    {
        private CarController m_Car; // the car controller we want to use
        private PowerUpManager powerUpManager;

        private void Awake()
        {
            m_Car = GetComponent<CarController>();
            powerUpManager = GetComponent<PowerUpManager>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                powerUpManager.UseSelectedPowerUp();
            }

            // Memilih power-up yang dimiliki untuk diluncurkan
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Ganti indeks power-up yang dipilih di PowerUpManager
                powerUpManager.NextPowerUp();
            }
        }

        private void FixedUpdate()
        {
            // pass the input to the car!
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
#if !MOBILE_INPUT
            float handbrake = Input.GetAxis("Jump");
            m_Car.Move(h, v, v, handbrake);
#else
            m_Car.Move(h, v, v, 0f);
#endif
        }
    }
}
