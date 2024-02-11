using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBarImage;
    public Color lowHealthColor = Color.red;    // Couleur pour PV bas
    public Color mediumHealthColor = Color.yellow; // ou Color.orange
    public Color highHealthColor = Color.green; // Couleur pour PV �lev�s

    public void SetHealth(float health, float maxHealth)
    {
        healthBarImage.fillAmount = health / maxHealth;

        // Changer la couleur en fonction des PV restants
        float healthPercentage = health / maxHealth;
        if (healthPercentage <= 0.15f)
        {
            healthBarImage.color = lowHealthColor; // Rouge pour une sant� basse
        }
        else if (healthPercentage <= 0.35f)
        {
            healthBarImage.color = mediumHealthColor; // Jaune ou Orange pour une sant� moyenne
        }
        else
        {
            healthBarImage.color = highHealthColor; // Vert pour une sant� �lev�e
        }
    }

}
