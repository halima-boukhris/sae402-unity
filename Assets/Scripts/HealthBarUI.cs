using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public PlayerData playerData;
    public Slider slider;

    void Update()
    {
        float value = (float)playerData.currentHealth / (float)playerData.maxHealth;
        Debug.Log("HP: " + playerData.currentHealth + " / " + playerData.maxHealth + " = " + value);
        slider.value = value;
    }
}
