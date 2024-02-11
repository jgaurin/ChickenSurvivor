using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    public Transform target;
    public float speed = 5f;
    public int damage = 2;
    public GameObject effectPoufPrefab;


    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return; 
        }

        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Chicken enemyHealth = other.GetComponent<Chicken>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }

            Vector3 spawnPosition = gameObject.transform.position; // Position de l'ennemi
            Quaternion spawnRotation = Quaternion.identity; // Rotation par défaut
            Instantiate(effectPoufPrefab, spawnPosition, spawnRotation); // Instanciez le prefab

            Destroy(gameObject);
        }
    }
}