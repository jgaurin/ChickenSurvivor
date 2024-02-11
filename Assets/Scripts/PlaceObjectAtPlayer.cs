using UnityEngine;

public class PlaceObjectAtPlayer : MonoBehaviour
{
    public Transform player; // Référence à l'objet joueur
    public GameObject targetObject; // Référence à l'objet à déplacer

    // Cette fonction est appelée pour déplacer l'objet
    public void MoveObjectToPlayer()
    {
        if (player != null && targetObject != null)
        {
            targetObject.transform.position = player.position;
        }
        else
        {
            Debug.LogError("Player or Target Object is not set!");
        }
    }

    private void LateUpdate()
    {
        MoveObjectToPlayer();
    }
}
