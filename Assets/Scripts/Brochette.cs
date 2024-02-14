using UnityEngine;

public class Brochette : MonoBehaviour
{
    // R�f�rence � votre prefab � instancier
    public GameObject prefabToSpawn;
    public GameObject prefabPouf;

    // Vitesse de rotation (degr�s par seconde)
    public float rotationSpeed = 90.0f;

    private void Update()
    {
        // Fait tourner l'objet sur l'axe X en continu
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        // V�rifie si l'objet entrant a le tag "Enemy"
        if (other.CompareTag("Enemy"))
        {
            // Obtient la position de la collision
            Vector3 spawnPosition = other.transform.position;
            // Ajuste la coordonn�e y � 0.5
            spawnPosition.y = 0.5f;
            spawnPosition.z = gameObject.transform.position.z;

            // Instancie le prefab � la position ajust�e
            GameObject spawnedObject = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
            Instantiate(prefabPouf, spawnPosition, Quaternion.identity);
            other.gameObject.GetComponent<Chicken>().Death();

            // D�finit le GameObject instanci� comme enfant de l'objet qui d�tient ce script (Brochette)
            spawnedObject.transform.SetParent(this.transform);
        }
    }
}
