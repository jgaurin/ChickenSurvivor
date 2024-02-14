using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireZone : MonoBehaviour
{
    public int damageAmount = 5; // Le nombre de points de dégâts infligés aux ennemis dans la zone.
    public float damageInterval = 1f; // L'intervalle en secondes à laquelle les dégâts sont appliqués.

    // Utilisez une liste pour suivre les ennemis actuellement dans la zone.
    private List<GameObject> enemiesInZone = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Ajoute l'ennemi entrant à la liste s'il n'est pas déjà présent.
            if (!enemiesInZone.Contains(other.gameObject))
            {
                enemiesInZone.Add(other.gameObject);
                // Commence à appliquer des dégâts à l'ennemi.
                StartCoroutine(ApplyDamage(other.gameObject));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy") && enemiesInZone.Contains(other.gameObject))
        {
            // Retire l'ennemi de la liste lorsqu'il quitte la zone.
            enemiesInZone.Remove(other.gameObject);
        }
    }

    private IEnumerator ApplyDamage(GameObject enemy)
    {
        // Continue à infliger des dégâts tant que l'ennemi est dans la zone.
        while (enemiesInZone.Contains(enemy))
        {
            // Assurez-vous que l'ennemi existe toujours avant d'appliquer des dégâts.
            if (enemy != null)
            {
                Chicken enemyScript = enemy.GetComponent<Chicken>(); // Remplacez 'Enemy' par le nom du script de votre ennemi.
                if (enemyScript != null)
                {
                    enemyScript.TakeDamage(damageAmount); // Applique des dégâts. Assurez-vous que votre script ennemi a une méthode TakeDamage.
                }
            }

            // Attend l'intervalle spécifié avant de réappliquer des dégâts.
            yield return new WaitForSeconds(damageInterval);
        }
    }
}
