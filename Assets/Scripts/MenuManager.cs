using UnityEngine;
using UnityEngine.SceneManagement; // Nécessaire pour la gestion des scènes

public class MenuManager : MonoBehaviour
{
    // Cette fonction publique peut être appelée pour charger la scène "Game"
    public void LoadGameScene()
    {
        SceneManager.LoadScene("Game");
    }
}
