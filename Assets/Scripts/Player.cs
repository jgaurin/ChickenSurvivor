using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    private float lastAttackTime = 0f; 
    private const float attackCooldown = 0.3f;
    private const float MinMoveDistance = 0.1f; // Distance minimale pour considérer le mouvement

    private Vector3 targetPosition;
    private bool isMoving = false;
    private Animator animator;
    public List<GameObject> enemiesInRange = new List<GameObject>();
    private Transform currentEnemy;
    public GameObject effectPoufPrefab;

    public HealthBar healthBar;
    public int Health = 100;
    private int maxHealth = 100;


    private void Start()
    {
        targetPosition = transform.position;
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        HandleInput();
        if (isMoving)
        {
            MoveAndRotatePlayer();
        }
        UpdateAnimation();
        FaceEnemyWhenStill();
        TryAttackEnemy();
    }

    private void FaceEnemyWhenStill()
    {
        if (!isMoving && enemiesInRange.Count > 0 && currentEnemy != null)
        {
            RotateTowards(currentEnemy.position);
        }
    }

    private void TryAttackEnemy()
    {
        if (!isMoving && currentEnemy != null && Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time; 
            DestroyEnemyAfterDelay(currentEnemy.gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        healthBar.SetHealth(Health, maxHealth); // Mettez à jour la barre de PV
        if (Health <= 0)
        {
            // Logique pour la mort du joueur
        }
    }


    private void DestroyEnemyAfterDelay(GameObject enemy)
    {
        print("attack");
        if (enemy != null && enemiesInRange.Contains(enemy))
        {
            Vector3 spawnPosition = enemy.transform.position; // Position de l'ennemi
            Quaternion spawnRotation = Quaternion.identity; // Rotation par défaut

            Instantiate(effectPoufPrefab, spawnPosition, spawnRotation); // Instanciez le prefab
            int healthEnnemy = enemy.GetComponent<Chicken>().TakeDamage(5); 
            if(healthEnnemy <= 0)
            {
                enemiesInRange.Remove(enemy);
                enemiesInRange = FiltrerNonNull(enemiesInRange);
                UpdateCurrentEnemy();
            }
        }
    }

    List<GameObject> FiltrerNonNull(List<GameObject> listeOriginale)
    {
        return listeOriginale.FindAll(item => item != null);
    }

    private void UpdateCurrentEnemy()
    {
        currentEnemy = null;
        foreach (var enemy in enemiesInRange)
        {
            if (enemy != null)
            {
                currentEnemy = enemy.transform;
                break;
            }
        }
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0)) { isMoving = true; }
        if (Input.GetMouseButtonUp(0)) { isMoving = false; }
        if (isMoving && Input.GetMouseButton(0)) { UpdateTargetPosition(); }
    }

    private void UpdateTargetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
        }
    }

    private void MoveAndRotatePlayer()
    {
        MovePlayer();
        RotateTowards(targetPosition);
    }

    private void MovePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    private void RotateTowards(Vector3 position)
    {
        Vector3 direction = position - transform.position;
        if (direction.magnitude > MinMoveDistance)
        {
            direction.y = 0;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = rotation;
        }
    }

    private void UpdateAnimation()
    {
        bool isRunning = isMoving && Vector3.Distance(transform.position, targetPosition) > MinMoveDistance;
        animator.SetBool("isRun", isRunning);
        if (isMoving || enemiesInRange.Count == 0) {
            animator.SetBool("isAttack", false);
            
            }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && !enemiesInRange.Contains(other.gameObject))
        {
            enemiesInRange.Add(other.gameObject);
            animator.SetBool("isAttack", true);
            UpdateCurrentEnemy();
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(other.gameObject);
            enemiesInRange = FiltrerNonNull(enemiesInRange);
            UpdateCurrentEnemy();
        }
    }
}
