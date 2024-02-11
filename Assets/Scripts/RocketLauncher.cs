using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    public GameObject rocketPrefab; // Assignez votre prefab de roquette ici dans l'inspecteur
    public Transform launchPoint; // Point d'instanciation de la roquette
    public Transform player; // Référence à l'objet joueur
    public float rotationSpeed = 15f; // Vitesse de rotation vers la cible
    public float launchThreshold = 0.99f; // Seuil pour déterminer si l'orientation est suffisamment proche
    public float amplitude = 0.2f; // Amplitude du mouvement de haut en bas
    public float frequency = 1f; // Vitesse du mouvement de haut en bas

    private Transform target; // Cible actuellement visée
    private bool isRocketLaunched; // Pour s'assurer qu'une roquette est lancée une seule fois par détection
    private Vector3 offset; // Décalage entre le joueur et le lance-roquettes
    private Vector3 baseHeight; // Pour stocker la hauteur de base lors du flottement


    void Start()
    {
        if (player != null)
        {
            offset = transform.position - player.position;
            baseHeight = transform.position; // Initialise la hauteur de base pour l'effet de flottement
        }
    }

    void Update()
    {
        if (target != null && !isRocketLaunched)
        {
            if (RotateTowardsTarget())
            {
                // Si la rotation est suffisamment proche de la cible, lance la roquette
                LaunchRocket();
                isRocketLaunched = true; // Empêche le lancement de multiples roquettes
            }
        }
    }

    void LateUpdate()
    {
        if (player != null)
        {
            // Calcul de la nouvelle position avec le décalage
            Vector3 newPos = player.position + offset;

            // Application de l'effet de flottement uniquement sur l'axe Y
            newPos.y += Mathf.Sin(Time.time * Mathf.PI * frequency) * amplitude;

            transform.position = newPos;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            target = other.transform;
            isRocketLaunched = false; // Réinitialise pour permettre un nouveau lancement
        }
    }

    bool RotateTowardsTarget()
    {
        Vector3 targetDirection = target.position - transform.position;
        targetDirection.y = 0;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        return Quaternion.Dot(transform.rotation, targetRotation) > launchThreshold;
    }

    void LaunchRocket()
    {
        if (target == null) return;

        GameObject rocket = Instantiate(rocketPrefab, launchPoint.position, Quaternion.identity);
        Vector3 directionToTarget = target.position - rocket.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget) * Quaternion.Euler(90, 0, 0);
        rocket.transform.rotation = lookRotation;

        RocketMovement rocketMovement = rocket.GetComponent<RocketMovement>();
        if (rocketMovement != null)
        {
            rocketMovement.SetTarget(target);
        }
    }
}
