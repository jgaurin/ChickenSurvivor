using UnityEngine;

public class ExpOrb : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Transform targetTransform;
    private bool moveTowardsPlayer = false;
    public int expGain = 1 ;

    public void StartMovingTowardsPlayer(Transform playerTransform)
    {
        targetTransform = playerTransform;
        moveTowardsPlayer = true;
    }

    private void Update()
    {
        if (moveTowardsPlayer && targetTransform != null)
        {
            MoveTowardsTarget();
        }
    }

    private void MoveTowardsTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetTransform.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetTransform.position) < 0.1f)
        {
            // Logique pour attribuer l'XP ici
            targetTransform.gameObject.GetComponent<Player>().GainExp(expGain);
            Destroy(gameObject); // Détruit l'orbe une fois qu'il atteint le joueur
        }
    }
}
