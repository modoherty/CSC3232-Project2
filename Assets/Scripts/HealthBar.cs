using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    [SerializeField]
    private Gradient gradient;

    [SerializeField]
    private Image colour;


    // Sets the maximum health value, when the game/level begins
    public void SetMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;

        colour.color = gradient.Evaluate(1f);
    }

    // Used to update the display on the health bar when the user takes damage/is healed
    public void SetHealthBar(int currentHealth)
    {
        slider.value = currentHealth;

        colour.color = gradient.Evaluate(slider.normalizedValue);
    }
}
