using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    public GameObject rocketPrefab;
    public Transform launchPoint;
    public Transform player;
    public float rotationSpeed = 15f;
    public float launchThreshold = 0.99f;
    public float amplitude = 0.2f;
    public float frequency = 1f;
    public float launchInterval = 1f; // Intervalle de temps entre les lancements de roquettes

    public List<Transform> enemies = new List<Transform>();
    private Transform target;
    private Vector3 offset;
    private Vector3 baseHeight;
    private Coroutine launchCoroutine; // Référence à la coroutine de lancement

    void Start()
    {
        if (player != null)
        {
            offset = transform.position - player.position;
            baseHeight = transform.position;
        }
    }

    void Update()
    {
        UpdateTarget();

        if (target != null && launchCoroutine == null)
        {
            // Démarrer la coroutine pour lancer les roquettes à intervalles réguliers
            launchCoroutine = StartCoroutine(LaunchRocketRepeatedly());
        }
    }

    void LateUpdate()
    {
        if (player != null)
        {
            if(target != null)
            {
                RotateTowardsTarget();
            }
            Vector3 newPos = player.position + offset;
            newPos.y += Mathf.Sin(Time.time * Mathf.PI * frequency) * amplitude;
            transform.position = newPos;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && !enemies.Contains(other.transform))
        {
            enemies.Add(other.transform);
            if (target == null)
            {
                target = enemies[0];
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // Vérifie si l'ennemi sortant est dans la liste et le retire
            if (enemies.Contains(other.transform))
            {
                enemies.Remove(other.transform);

                // Si l'ennemi sortant était la cible actuelle, mettez à jour la cible
                if (target == other.transform)
                {
                    UpdateTargetImmediately();
                }
            }
        }
    }

    void UpdateTargetImmediately()
    {
        // Nettoyez d'abord les références nulles
        enemies.RemoveAll(enemy => enemy == null);

        if (enemies.Count > 0)
        {
            // Mettez à jour la cible avec le premier ennemi disponible dans la liste
            target = enemies[0];
        }
        else
        {
            // Arrêtez la coroutine de lancement si plus aucun ennemi n'est présent et réinitialisez la cible
            target = null;
            if (launchCoroutine != null)
            {
                StopCoroutine(launchCoroutine);
                launchCoroutine = null;
            }
        }
    }


    void UpdateTarget()
    {
        if (target == null || target.gameObject == null)
        {
            if (enemies.Count > 0)
            {
                enemies.RemoveAll(enemy => enemy == null);
                if (enemies.Count > 0)
                {
                    target = enemies[0];
                }
                else
                {
                    // Arrêter la coroutine si plus aucun ennemi n'est présent
                    if (launchCoroutine != null)
                    {
                        StopCoroutine(launchCoroutine);
                        launchCoroutine = null;
                    }
                }
            }
        }
    }

    IEnumerator LaunchRocketRepeatedly()
    {
        while (target != null)
        {
            LaunchRocket();
            yield return new WaitForSeconds(launchInterval); // Attend l'intervalle spécifié avant de relancer
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
