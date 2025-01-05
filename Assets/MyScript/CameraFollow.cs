using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Referencia al personaje principal
    public Transform target;

    // Desfase configurable en X e Y
    public Vector2 offset = new Vector2(0f, 0f);

    // Velocidad para suavizar el movimiento de la c�mara
    public float smoothSpeed = 5f;

    private void LateUpdate()
    {
        // Verificar que el target est� asignado
        if (target == null)
        {
            Debug.LogWarning("El target no est� asignado en CameraFollow.");
            return;
        }

        // Calcular la nueva posici�n con el desfase
        Vector3 desiredPosition = new Vector3(
            target.position.x + offset.x,
            target.position.y + offset.y,
            transform.position.z // Mantener la posici�n Z de la c�mara
        );

        // Suavizar el movimiento hacia la posici�n deseada
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Actualizar la posici�n de la c�mara
        transform.position = smoothedPosition;
    }
}
