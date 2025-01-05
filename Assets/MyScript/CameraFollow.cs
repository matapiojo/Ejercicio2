using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Referencia al personaje principal
    public Transform target;

    // Desfase configurable en X e Y
    public Vector2 offset = new Vector2(0f, 0f);

    // Velocidad para suavizar el movimiento de la cámara
    public float smoothSpeed = 5f;

    private void LateUpdate()
    {
        // Verificar que el target está asignado
        if (target == null)
        {
            Debug.LogWarning("El target no está asignado en CameraFollow.");
            return;
        }

        // Calcular la nueva posición con el desfase
        Vector3 desiredPosition = new Vector3(
            target.position.x + offset.x,
            target.position.y + offset.y,
            transform.position.z // Mantener la posición Z de la cámara
        );

        // Suavizar el movimiento hacia la posición deseada
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Actualizar la posición de la cámara
        transform.position = smoothedPosition;
    }
}
