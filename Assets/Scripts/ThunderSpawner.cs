using System.Collections;
using UnityEngine;

public class ThunderSpawner : MonoBehaviour
{
    public GameObject prefabThunder; // Le prefab de la foudre à générer.
    public float spawnRadius = 10f; // Rayon autour du joueur où la foudre peut apparaître.
    public float spawnInterval = 5f; // Intervalle en secondes entre les vagues de foudre.
    public float delayBetweenThunders = 0.5f; // Délai en secondes entre chaque foudre.

    public Transform playerTransform; // Pour stocker la position du joueur.

    private void Start()
    {
        StartCoroutine(SpawnLightningWaves());
    }

    private IEnumerator SpawnLightningWaves()
    {
        while (true) // Boucle infinie pour continuer à générer des vagues.
        {
            yield return new WaitForSeconds(spawnInterval); // Attendre pour la prochaine vague.

            // Générer un nombre aléatoire de foudres pour cette vague.
            int numberOfThunders = Random.Range(2, 5); // Génère un nombre entre 2 et 4 inclus.
            for (int i = 0; i < numberOfThunders; i++)
            {
                SpawnThunderNearPlayer();
                // Attendre un court délai avant de générer la prochaine foudre, pour les faire apparaître successivement.
                yield return new WaitForSeconds(delayBetweenThunders);
            }
        }
    }

    private void SpawnThunderNearPlayer()
    {
        // Générer une position aléatoire autour du joueur.
        Vector3 spawnPosition = playerTransform.position + Random.insideUnitSphere * spawnRadius;
        spawnPosition.y = playerTransform.position.y; // Assurez-vous que la foudre apparaît au niveau du sol ou à une hauteur spécifique.

        // Créer le prefab de la foudre à la position générée.
        Instantiate(prefabThunder, spawnPosition, Quaternion.identity);
    }
}
