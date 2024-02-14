using UnityEngine;

public class Food : MonoBehaviour
{
    public int healthRecoveryAmount = 20; // Quantit� de sant� r�cup�r�e par le joueur lorsqu'il collecte la nourriture.
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
                // Acc�de au script du joueur pour mettre � jour sa sant�.
                Player playerScript = other.GetComponent<Player>();

                if (playerScript != null)
                {
                    playerScript.RecoverHealth(healthRecoveryAmount); // Appelle la fonction de r�cup�ration de sant� du joueur.
                }

                // D�truit l'objet de nourriture apr�s que le joueur l'a collect�.
                Destroy(gameObject);
            }
        }
    }
}
