using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma : MonoBehaviour
{
    // Variables p�blicas para configurar la plataforma
    public bool moveInX = true; // True para mover en X, False para mover en Y
    public float speed = 2f;   // Velocidad de movimiento
    public float frequency = 2f; // Frecuencia (amplitud del movimiento)

    // Variables internas
    private Vector3 startPosition; // Posici�n inicial
    private bool isPlayerOnPlatform = false;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        // Calcular el movimiento de la plataforma
        Vector3 offset = Vector3.zero;
        float movement = Mathf.Sin(Time.time * speed) * frequency;

        if (moveInX)
            offset = new Vector3(movement, 0, 0); // Movimiento en X
        else
            offset = new Vector3(0, movement, 0); // Movimiento en Y

        transform.position = startPosition + offset;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Detectar si el jugador est� sobre la plataforma
        if (collision.collider.CompareTag("Player"))
        {
            isPlayerOnPlatform = true;
            collision.collider.transform.SetParent(transform); // Hacer que el jugador se mueva con la plataforma
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Detectar si el jugador sali� de la plataforma
        if (collision.collider.CompareTag("Player"))
        {
            isPlayerOnPlatform = false;
            collision.collider.transform.SetParent(null); // Liberar al jugador de la plataforma
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Asegurar que el jugador no atraviese la plataforma desde arriba
        if (collision.collider.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.collider.GetComponent<Rigidbody2D>();
            if (playerRb.linearVelocity.y < 0)
            {
                // Si el jugador est� cayendo, bloquear su ca�da
                playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, 0);
            }
        }
    }
}
