using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed = 5.0f;
    private Rigidbody rb;
    private Vector3 direction;
    private float timeSinceLastDirectionChange; // Temps écoulé depuis le dernier changement de direction
    public float directionChangeCooldown = 0.5f; // Délai minimum entre les changements de direction
    public int damage = 100;
    public float trailSpawnInterval = 0.02f; // Intervalle entre les instanciations des particules
    public GameObject effectPoufPrefab;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Initialise la direction avec des valeurs aléatoires pour x (avant/arrière) et z (gauche/droite)
        direction = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        timeSinceLastDirectionChange = directionChangeCooldown; // Permet un changement de direction immédiat
    }

    void Update()
    {
        MoveBall();
        timeSinceLastDirectionChange += Time.deltaTime; // Met à jour le temps écoulé
        CheckBounds();
    }

    void MoveBall()
    {
        rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
    }

    void CheckBounds()
    {
        // Convertit la position de la balle en position à l'écran
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);

        // Vérifie si la balle est au bord de l'écran et si le délai depuis le dernier changement de direction est écoulé
        if (timeSinceLastDirectionChange >= directionChangeCooldown)
        {
            if (screenPos.x < 0 || screenPos.x > Screen.width - 50 )
            {
                // Inverse la composante x de la direction pour un rebond horizontal
                direction.x = -direction.x;
                timeSinceLastDirectionChange = 0; // Réinitialise le compteur de temps
            }

            if (screenPos.y < 0 || screenPos.y > Screen.height - 50)
            {
                // Inverse la composante z de la direction pour un rebond vertical
                // Notez que nous utilisons z ici car dans votre jeu z contrôle le gauche/droite
                direction.z = -direction.z;
                timeSinceLastDirectionChange = 0; // Réinitialise le compteur de temps
            }
        }
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
        }
    }
}
