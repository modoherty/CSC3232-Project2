using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    private int maxHealth = 4;
    private int currentHealth;

    [SerializeField]
    private HealthBar healthBar;

    [SerializeField]
    private PlayerLives playerLives;

    // Start is called before the first frame update
    void Start()
    {
        // Initially set the player's health to the maximum value
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        /* Prevents the player from having more than the maximum amount of health
           if they gain health with a full health bar */
        if (currentHealth - damage > maxHealth)
            currentHealth = maxHealth;

        if (currentHealth > 0)
        {
            // Updates the health bar to show the player's current health
            healthBar.SetHealthBar(currentHealth);
        }

        else
        {
            // The player will lose a life if their health reaches 0
            playerLives.LoseLife(1);
        }
    }
}
