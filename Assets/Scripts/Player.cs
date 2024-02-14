using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    public ExpBar expBar;

    public int currentExp = 0;
    public int expToNextLevel = 100;
    public int currentLevel = 1;

    public TextMeshProUGUI levelText;
    public GameManager gameManager;
    public GameObject brochettePrefab;


    private void Start()
    {
        targetPosition = transform.position;
        animator = GetComponent<Animator>();
        expBar.SetExp(currentExp, expToNextLevel); 
    }

    void Update()
    {

        print(currentExp + " " + currentLevel);
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
        // Définissez ici le nom du layer du sol
        int layerMask = LayerMask.GetMask("Ground");
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
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
        else if (other.CompareTag("Exp")) // Si l'objet a le tag "Exp"
        {
            ExpOrb expOrb = other.GetComponent<ExpOrb>();
            if (expOrb != null)
            {
                expOrb.StartMovingTowardsPlayer(transform); // Demande à l'orbe de commencer à se déplacer vers le joueur
            }
        }
        else if(other.CompareTag("SpawnBrochette"))
        {
            Vector3 spawnPosition = other.transform.position;
            Destroy(other.gameObject);
            Instantiate(brochettePrefab, spawnPosition, Quaternion.identity); // Instanciez le prefab

        }
    }

    public void GainExp(int exp)
    {
        currentExp += exp;
        expBar.SetExp(currentExp, expToNextLevel); // Mettez à jour la barre d'EXP
        if (currentExp >= expToNextLevel)
        {
            LevelUp();
        }
    }

    void LevelUp()
    {
        gameManager.ActivatePopUpCapacity();
        currentExp -= expToNextLevel; // Retire l'XP nécessaire pour le niveau actuel
        currentLevel++; // Augmente le niveau
        expToNextLevel += 100; // Augmente l'XP nécessaire pour le prochain niveau
        // Ici, vous pouvez augmenter la santé, la vitesse, etc.
        maxHealth += 20; // Exemple : Augmenter la santé maximale
        Health = maxHealth; // Restaure la santé à son maximum
                            // Mettez à jour la barre de santé ou d'autres UI si nécessaire
        expBar.SetExp(currentExp, expToNextLevel); // Mettez à jour la barre d'EXP pour le nouveau niveau
        levelText.text = currentLevel.ToString();

    }

    public void RecoverHealth(int amount)
    {
        Health += amount;
        if (Health > maxHealth) // Assure que la santé ne dépasse pas la santé maximale.
        {
            Health = maxHealth;
        }
        healthBar.SetHealth(Health, maxHealth); // Met à jour la barre de santé avec la nouvelle valeur.
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
