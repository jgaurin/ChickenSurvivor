using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    public Transform target;
    public float speed = 5f;
    public int damage = 10; // Dégâts infligés à l'ennemi

    void Update()
    {
        // Vérifie si la cible existe encore avant de tenter d'accéder à sa position
        if (target == null)
        {
            // Optionnel : détruire la roquette si la cible n'existe plus
            Destroy(gameObject);
            return; // Quitte la méthode Update pour éviter d'exécuter le reste du code
        }

        if (target != null)
        {
            // Se déplacer vers la cible
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    void OnTriggerEnter(Collider other)
    {
        // Vérifie si l'objet entrant a le tag "Enemy"
        if (other.gameObject.CompareTag("Enemy"))
        {
            // Inflige des dégâts à l'ennemi
            // Ici, vous devrez avoir un script sur l'ennemi pour gérer sa santé
            Chicken enemyHealth = other.GetComponent<Chicken>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }

            // Optionnellement, détruire la roquette après avoir infligé des dégâts
            Destroy(gameObject);
        }
    }
}
