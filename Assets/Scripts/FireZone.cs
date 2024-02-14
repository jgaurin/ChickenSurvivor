using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireZone : MonoBehaviour
{
    public int damageAmount = 5; // Le nombre de points de d�g�ts inflig�s aux ennemis dans la zone.
    public float damageInterval = 1f; // L'intervalle en secondes � laquelle les d�g�ts sont appliqu�s.

    // Utilisez une liste pour suivre les ennemis actuellement dans la zone.
    private List<GameObject> enemiesInZone = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Ajoute l'ennemi entrant � la liste s'il n'est pas d�j� pr�sent.
            if (!enemiesInZone.Contains(other.gameObject))
            {
                enemiesInZone.Add(other.gameObject);
                // Commence � appliquer des d�g�ts � l'ennemi.
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
        // Continue � infliger des d�g�ts tant que l'ennemi est dans la zone.
        while (enemiesInZone.Contains(enemy))
        {
            // Assurez-vous que l'ennemi existe toujours avant d'appliquer des d�g�ts.
            if (enemy != null)
            {
                Chicken enemyScript = enemy.GetComponent<Chicken>(); // Remplacez 'Enemy' par le nom du script de votre ennemi.
                if (enemyScript != null)
                {
                    enemyScript.TakeDamage(damageAmount); // Applique des d�g�ts. Assurez-vous que votre script ennemi a une m�thode TakeDamage.
                }
            }

            // Attend l'intervalle sp�cifi� avant de r�appliquer des d�g�ts.
            yield return new WaitForSeconds(damageInterval);
        }
    }
}
