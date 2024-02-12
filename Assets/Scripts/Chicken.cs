using System.Collections;
using UnityEngine;
using UnityEngine.AI; // Importez cette biblioth�que pour utiliser NavMesh

public class Chicken : MonoBehaviour
{
    public Transform player;
    public float stopDistance = 1f; // Distance � laquelle le "Chicken" s'arr�tera

    private Animator animator; // R�f�rence � l'Animator
    private bool isAttacking = false;
    private float distanceToPlayer = 0;
    private int damage = 1;
    private float durationAttackReload = 0.5f;
    public int health = 5;

    private NavMeshAgent agent; // Ajout du NavMeshAgent
    private Rigidbody rb;

    public GameObject exp;


    void Start()
    {
        animator = GetComponent<Animator>(); // R�cup�rer le composant Animator

        // Trouver le joueur par son tag "Player"
        player = GameObject.FindWithTag("Player").transform;

        if (player == null)
        {
            Debug.LogError("Le joueur avec le tag 'Player' n'a pas �t� trouv�.");
        }

        // Initialiser le NavMeshAgent
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            agent = gameObject.AddComponent<NavMeshAgent>();
        }
        agent.stoppingDistance = stopDistance; // D�finir la distance d'arr�t

        rb = GetComponent<Rigidbody>();
        if (rb == null) // Si aucun Rigidbody n'est attach�, en ajouter un.
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        rb.useGravity = false;
    }

    void Update()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        if (player != null)
        {
            distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer > stopDistance)
            {
                agent.SetDestination(player.position); // Utilisez le NavMeshAgent pour se d�placer vers le joueur

                if (!animator.GetBool("Run"))
                {
                    animator.SetBool("Run", true);
                }
            }
            else
            {
                agent.ResetPath(); // Arr�te le mouvement

                if (animator.GetBool("Run"))
                {
                    animator.SetBool("Run", false);
                }

                if (!isAttacking && distanceToPlayer <= stopDistance)
                {
                    StartCoroutine(AttackPlayer());
                }
            }
        }
    }

    private IEnumerator AttackPlayer()
    {
        isAttacking = true;
        while (distanceToPlayer <= stopDistance)
        {
            yield return new WaitForSeconds(durationAttackReload);
            player.GetComponent<Player>().TakeDamage(damage); // Supposons que vous avez une m�thode TakeDamage dans Player

        }
        isAttacking = false;
    }

    public int TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Vector3 spawnPosition = gameObject.transform.position; // Position de l'ennemi
            Quaternion spawnRotation = Quaternion.identity; // Rotation par d�faut
            Instantiate(exp, spawnPosition, spawnRotation);
            Death();
        }
        return health;
    }

    public void Death()
    {
        Destroy(gameObject); // D�truit l'ennemi
    }
}
