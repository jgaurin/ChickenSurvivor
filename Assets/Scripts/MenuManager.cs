using UnityEngine;
using UnityEngine.SceneManagement; // N�cessaire pour la gestion des sc�nes

public class MenuManager : MonoBehaviour
{
    // Cette fonction publique peut �tre appel�e pour charger la sc�ne "Game"
    public void LoadGameScene()
    {
        SceneManager.LoadScene("Game");
    }
}
