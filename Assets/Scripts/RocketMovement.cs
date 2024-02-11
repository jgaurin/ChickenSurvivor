using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    public Transform target;
    public float speed = 5f;
    public int damage = 10; // D�g�ts inflig�s � l'ennemi

    void Update()
    {
        // V�rifie si la cible existe encore avant de tenter d'acc�der � sa position
        if (target == null)
        {
            // Optionnel : d�truire la roquette si la cible n'existe plus
            Destroy(gameObject);
            return; // Quitte la m�thode Update pour �viter d'ex�cuter le reste du code
        }

        if (target != null)
        {
            // Se d�placer vers la cible
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    void OnTriggerEnter(Collider other)
    {
        // V�rifie si l'objet entrant a le tag "Enemy"
        if (other.gameObject.CompareTag("Enemy"))
        {
            // Inflige des d�g�ts � l'ennemi
            // Ici, vous devrez avoir un script sur l'ennemi pour g�rer sa sant�
            Chicken enemyHealth = other.GetComponent<Chicken>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }

            // Optionnellement, d�truire la roquette apr�s avoir inflig� des d�g�ts
            Destroy(gameObject);
        }
    }
}
