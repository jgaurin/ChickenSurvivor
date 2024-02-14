using UnityEngine;

public class BombExplosion : MonoBehaviour
{
    public Transform playerTransform; // R�f�rence � la position du joueur.

    private void OnTriggerEnter(Collider other)
    {
        // V�rifie si l'objet qui entre dans le trigger est celui que nous voulons.
        if (other.CompareTag("Player"))
        {

            playerTransform = other.transform;

            // Calcule la distance entre la bombe et le joueur.
            float distanceToPlayer = Vector3.Distance(playerTransform.position, transform.position);

            // V�rifie si le joueur est � une distance inf�rieure ou �gale � 0.1 unit�.
            if (distanceToPlayer <= 1f)
            {
                // Trouve tous les objets avec le tag "Enemy" dans la sc�ne.
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

                // Boucle � travers chaque objet ennemi trouv� et le d�truit.
                foreach (GameObject enemy in enemies)
                {
                    Destroy(enemy);
                }

                // D�truit la bombe elle-m�me.
                Destroy(gameObject);
            }
        }
    }
}
