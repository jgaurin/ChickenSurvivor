using UnityEngine;

public class Food : MonoBehaviour
{
    public int healthRecoveryAmount = 20; // Quantité de santé récupérée par le joueur lorsqu'il collecte la nourriture.
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
                // Accède au script du joueur pour mettre à jour sa santé.
                Player playerScript = other.GetComponent<Player>();

                if (playerScript != null)
                {
                    playerScript.RecoverHealth(healthRecoveryAmount); // Appelle la fonction de récupération de santé du joueur.
                }

                // Détruit l'objet de nourriture après que le joueur l'a collecté.
                Destroy(gameObject);
            }
        }
    }
}
