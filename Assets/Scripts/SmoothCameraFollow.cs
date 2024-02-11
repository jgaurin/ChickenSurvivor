using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    public Transform target; // Cible que la cam�ra doit suivre (g�n�ralement le joueur)
    public float smoothSpeed = 0.125f; // Vitesse de suivi de la cam�ra
    private Vector3 offset; // D�calage entre la cam�ra et la cible

    void Start()
    {
        // Calculez l'offset initial en soustrayant la position de la cible de celle de la cam�ra
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

       // transform.LookAt(target); // La cam�ra regarde toujours vers la cible
    }
}
