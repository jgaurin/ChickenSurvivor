using UnityEngine;

public class FloatEffect : MonoBehaviour
{
    // Param�tres personnalisables pour ajuster l'effet de flottement
    public float amplitude = 0.2f; // Amplitude du mouvement de haut en bas
    public float frequency = 1f; // Vitesse du mouvement de haut en bas

    // Position initiale de l'objet
    private Vector3 startPos;

    void Start()
    {
        // Enregistrer la position de d�part de l'objet
        startPos = transform.position;
    }

    void Update()
    {
        // Calculer la nouvelle position de l'objet
        Vector3 tempPos = startPos;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        // Appliquer la nouvelle position � l'objet
        transform.position = tempPos;
    }
}
