using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAttack : MonoBehaviour
{
    public int damage = 10; // Montant des dégâts infligés
    public GameObject effectPoufPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) // Vérifie si c'est bien un ennemi
        {
            // Appelle une méthode sur l'ennemi pour lui infliger des dégâts
            Chicken enemy = other.GetComponent<Chicken>();
            Vector3 spawnPosition = enemy.transform.position; // Position de l'ennemi
            Quaternion spawnRotation = Quaternion.identity; // Rotation par défaut

            Instantiate(effectPoufPrefab, spawnPosition, spawnRotation);
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}
