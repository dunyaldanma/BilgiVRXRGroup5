using UnityEngine;
using UnityEngine.UI;

public class PlayerEnergy : MonoBehaviour
{
    public float maxEnergy = 100f;
    public float currentEnergy;
    public float criticalThreshold = 10f;

    public Slider energySlider; // UI Slider 
    public bool IsOutOfEnergy => currentEnergy <= 0;

    void Start()
    {
        currentEnergy = maxEnergy;
        UpdateUI();
    }

    public void UseEnergy(float amount)
    {
        currentEnergy = Mathf.Max(currentEnergy - amount, 0f);
        UpdateUI();
    }

    public void AddEnergy(float amount)
    {
        currentEnergy = Mathf.Min(currentEnergy + amount, maxEnergy);
        UpdateUI();
    }

    void UpdateUI()
    {
        if (energySlider != null)
        {
            float ratio = currentEnergy / maxEnergy;
            energySlider.value = ratio;

            // Access the fill image to update the color
            Image fillImage = energySlider.fillRect.GetComponent<Image>();
            if (fillImage != null)
            {
                if (ratio > 0.5f)
                    fillImage.color = Color.green;
                else if (ratio > 0.2f)
                    fillImage.color = Color.yellow;
                else
                    fillImage.color = Color.red;
            }
        }
    }

}
