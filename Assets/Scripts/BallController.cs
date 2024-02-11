using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed = 5.0f;
    private Rigidbody rb;
    private Vector3 direction;
    private float timeSinceLastDirectionChange; // Temps �coul� depuis le dernier changement de direction
    public float directionChangeCooldown = 0.5f; // D�lai minimum entre les changements de direction
    public int damage = 100;
    public float trailSpawnInterval = 0.02f; // Intervalle entre les instanciations des particules
    public GameObject effectPoufPrefab;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Initialise la direction avec des valeurs al�atoires pour x (avant/arri�re) et z (gauche/droite)
        direction = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        timeSinceLastDirectionChange = directionChangeCooldown; // Permet un changement de direction imm�diat
    }

    void Update()
    {
        MoveBall();
        timeSinceLastDirectionChange += Time.deltaTime; // Met � jour le temps �coul�
        CheckBounds();
    }

    void MoveBall()
    {
        rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
    }

    void CheckBounds()
    {
        // Convertit la position de la balle en position � l'�cran
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);

        // V�rifie si la balle est au bord de l'�cran et si le d�lai depuis le dernier changement de direction est �coul�
        if (timeSinceLastDirectionChange >= directionChangeCooldown)
        {
            if (screenPos.x < 0 || screenPos.x > Screen.width - 50 )
            {
                // Inverse la composante x de la direction pour un rebond horizontal
                direction.x = -direction.x;
                timeSinceLastDirectionChange = 0; // R�initialise le compteur de temps
            }

            if (screenPos.y < 0 || screenPos.y > Screen.height - 50)
            {
                // Inverse la composante z de la direction pour un rebond vertical
                // Notez que nous utilisons z ici car dans votre jeu z contr�le le gauche/droite
                direction.z = -direction.z;
                timeSinceLastDirectionChange = 0; // R�initialise le compteur de temps
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
            Quaternion spawnRotation = Quaternion.identity; // Rotation par d�faut
            Instantiate(effectPoufPrefab, spawnPosition, spawnRotation); // Instanciez le prefab
        }
    }
}
