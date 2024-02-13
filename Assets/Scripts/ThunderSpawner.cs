using System.Collections;
using UnityEngine;

public class ThunderSpawner : MonoBehaviour
{
    public GameObject prefabThunder; // Le prefab de la foudre � g�n�rer.
    public float spawnRadius = 10f; // Rayon autour du joueur o� la foudre peut appara�tre.
    public float spawnInterval = 5f; // Intervalle en secondes entre les vagues de foudre.
    public float delayBetweenThunders = 0.5f; // D�lai en secondes entre chaque foudre.

    public Transform playerTransform; // Pour stocker la position du joueur.

    private void Start()
    {
        StartCoroutine(SpawnLightningWaves());
    }

    private IEnumerator SpawnLightningWaves()
    {
        while (true) // Boucle infinie pour continuer � g�n�rer des vagues.
        {
            yield return new WaitForSeconds(spawnInterval); // Attendre pour la prochaine vague.

            // G�n�rer un nombre al�atoire de foudres pour cette vague.
            int numberOfThunders = Random.Range(2, 5); // G�n�re un nombre entre 2 et 4 inclus.
            for (int i = 0; i < numberOfThunders; i++)
            {
                SpawnThunderNearPlayer();
                // Attendre un court d�lai avant de g�n�rer la prochaine foudre, pour les faire appara�tre successivement.
                yield return new WaitForSeconds(delayBetweenThunders);
            }
        }
    }

    private void SpawnThunderNearPlayer()
    {
        // G�n�rer une position al�atoire autour du joueur.
        Vector3 spawnPosition = playerTransform.position + Random.insideUnitSphere * spawnRadius;
        spawnPosition.y = playerTransform.position.y; // Assurez-vous que la foudre appara�t au niveau du sol ou � une hauteur sp�cifique.

        // Cr�er le prefab de la foudre � la position g�n�r�e.
        Instantiate(prefabThunder, spawnPosition, Quaternion.identity);
    }
}
