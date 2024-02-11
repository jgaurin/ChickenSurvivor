using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAttack : MonoBehaviour
{
    public int damage = 10; // Montant des d�g�ts inflig�s
    public GameObject effectPoufPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) // V�rifie si c'est bien un ennemi
        {
            // Appelle une m�thode sur l'ennemi pour lui infliger des d�g�ts
            Chicken enemy = other.GetComponent<Chicken>();
            Vector3 spawnPosition = enemy.transform.position; // Position de l'ennemi
            Quaternion spawnRotation = Quaternion.identity; // Rotation par d�faut

            Instantiate(effectPoufPrefab, spawnPosition, spawnRotation);
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}
