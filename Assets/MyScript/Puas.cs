using System.Collections;
using UnityEngine;

namespace Platformer.Mechanics
{
    public class SpikeTrap : MonoBehaviour
    {
        public float riseHeight = 0.1f; // Altura a la que las púas se elevarán
        public float riseTime = 0.1f; // Tiempo que tardan en subir (rápido)
        public float fallTime = 1f; // Tiempo que tardan en bajar (lento)
        public float waitTime = 1f; // Tiempo que permanecen arriba
        public float damage = 10f; // Daño infligido al jugador

        private Vector3 startPosition;
        private bool isRaising = true;

        void Start()
        {
            startPosition = transform.position; // Guardar la posición inicial
            StartCoroutine(SpikeMovement());
        }

        private IEnumerator SpikeMovement()
        {
            while (true)
            {
                // Elevar las púas
                if (isRaising)
                {
                    float elapsedTime = 0f;

                    while (elapsedTime < riseTime)
                    {
                        transform.position = Vector3.Lerp(startPosition, startPosition + Vector3.up * riseHeight, (elapsedTime / riseTime));
                        elapsedTime += Time.deltaTime;
                        yield return null;
                    }

                    transform.position = startPosition + Vector3.up * riseHeight; // Asegurarse de que llegue a la posición final
                    isRaising = false;

                    // Esperar en la posición elevada
                    yield return new WaitForSeconds(waitTime);
                }
                else
                {
                    // Bajar las púas
                    float elapsedTime = 0f;

                    while (elapsedTime < fallTime)
                    {
                        transform.position = Vector3.Lerp(startPosition + Vector3.up * riseHeight, startPosition, (elapsedTime / fallTime));
                        elapsedTime += Time.deltaTime;
                        yield return null;
                    }

                    transform.position = startPosition; // Asegurarse de que vuelva a la posición inicial
                    isRaising = true;

                    // Esperar antes de elevarse nuevamente
                    yield return new WaitForSeconds(waitTime);
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                // Infligir daño al jugador
                Health playerHealth = other.GetComponent<Health>();
                if (playerHealth != null)
                {
                    playerHealth.Decrement();
                }
            }
        }
    }
}