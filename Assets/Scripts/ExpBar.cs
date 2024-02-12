using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    public Image expBarImage;

    public void SetExp(int currentExp, int expToNextLevel)
    {
        float expPercentage = (float)currentExp / expToNextLevel;
        expBarImage.fillAmount = expPercentage;
    }
}
