using UnityEngine;

public class RotateSword : MonoBehaviour
{
    public float rotationSpeed = 100f; // Vitesse de rotation

    void Update()
    {
        // Fait tourner l'objet autour de l'axe Y
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
