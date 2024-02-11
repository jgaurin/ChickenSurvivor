using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    public Transform target; // Cible que la caméra doit suivre (généralement le joueur)
    public float smoothSpeed = 0.125f; // Vitesse de suivi de la caméra
    private Vector3 offset; // Décalage entre la caméra et la cible

    void Start()
    {
        // Calculez l'offset initial en soustrayant la position de la cible de celle de la caméra
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

       // transform.LookAt(target); // La caméra regarde toujours vers la cible
    }
}
