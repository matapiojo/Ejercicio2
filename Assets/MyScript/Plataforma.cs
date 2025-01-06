using System.Collections;
using System.Collections.Generic;
using Platformer.Mechanics;
using UnityEngine;

public class Plataforma : MonoBehaviour
{
    // Variables públicas para configurar la plataforma
    public bool moveInX = true; // True para mover en X, False para mover en Y
    public float speed = 2f;   // Velocidad de movimiento
    public float frequency = 2f; // Frecuencia (amplitud del movimiento)

    public PlayerController playerController;

    // Variables internas
    private Vector3 startPosition; // Posición inicial
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerController.transform.SetParent(transform);
            //playerController.AttachToPlatform(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerController.transform.SetParent(null);
            //playerController.DetachFromPlatform();

        }
    }
}
