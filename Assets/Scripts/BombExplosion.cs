using UnityEngine;

public class BombExplosion : MonoBehaviour
{
    public Transform playerTransform; // Référence à la position du joueur.

    private void OnTriggerEnter(Collider other)
    {
        // Vérifie si l'objet qui entre dans le trigger est celui que nous voulons.
        if (other.CompareTag("Player"))
        {

            playerTransform = other.transform;

            // Calcule la distance entre la bombe et le joueur.
            float distanceToPlayer = Vector3.Distance(playerTransform.position, transform.position);

            // Vérifie si le joueur est à une distance inférieure ou égale à 0.1 unité.
            if (distanceToPlayer <= 1f)
            {
                // Trouve tous les objets avec le tag "Enemy" dans la scène.
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

                // Boucle à travers chaque objet ennemi trouvé et le détruit.
                foreach (GameObject enemy in enemies)
                {
                    Destroy(enemy);
                }

                // Détruit la bombe elle-même.
                Destroy(gameObject);
            }
        }
    }
}
