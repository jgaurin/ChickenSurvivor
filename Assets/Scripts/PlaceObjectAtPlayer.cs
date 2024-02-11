using UnityEngine;

public class PlaceObjectAtPlayer : MonoBehaviour
{
    public Transform player; // R�f�rence � l'objet joueur
    public GameObject targetObject; // R�f�rence � l'objet � d�placer

    // Cette fonction est appel�e pour d�placer l'objet
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
