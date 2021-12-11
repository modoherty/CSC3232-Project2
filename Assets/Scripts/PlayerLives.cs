using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLives : MonoBehaviour
{
    /* This class manages the lives system in the game, allowing
     * the player to gain, lose lives, and reach 'game over' if they
     * run out of lives. */


    private int currentLives = 5;

    private TextMeshProUGUI livesText;

    private AudioManagement audioManagement;

    // Start is called before the first frame update
    void Start()
    {
        currentLives = PlayerPrefs.GetInt("currentLives");

        livesText = GetComponent<TextMeshProUGUI>();
        livesText.text = "LIVES: " + currentLives;

        audioManagement = FindObjectOfType<AudioManagement>();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public void LoseLife(int life)
    {
        currentLives -= life;

        if (currentLives > 0)
        {
            // Stores current lives to carry between levels
            PlayerPrefs.SetInt("currentLives", currentLives);
            PlayerPrefs.Save();
            Debug.Log("Lost life - lives remaining = " + currentLives);
            // Restarts level using coroutine
            StartCoroutine(Restart());
        }
        else
        {
            // Gives player back 5 initial lives - "clean slate"
            PlayerPrefs.SetInt("currentLives", 5);
            Debug.Log("No Lives Remaining - Game Over!");
            // Uses coroutine for 'game over'
            StartCoroutine(Restart());
        }

        IEnumerator Restart()
        {
            if (currentLives > 0)
            {
                // Pauses in-game time to prevent collision/sound playing issues
                Time.timeScale = 0f;
                // Play the death sound
                audioManagement.Play("Death");
                // Wait for 0.7 seconds so the whole sound can play
                yield return new WaitForSecondsRealtime(0.7f);
                // Restart the level
                RestartLevel();
            }
            else
            {
                // 'Pauses' the game
                Time.timeScale = 0f;
                // Play Game Over sound
                audioManagement.Play("GameOver");
                // Wait for 1.4 seconds before sending the player back to level 1
                yield return new WaitForSecondsRealtime(1.4f);
                // Game Over - player must restart from the beginning
                GameOver();
            }
        }
    }

    public void GainLife(int life)
    {
        if (currentLives + life > 99)
            // Sets the maximum number of allowed lives to 99
            currentLives = 99;

        else
            currentLives += life;

        PlayerPrefs.SetInt("currentLives", currentLives);
        PlayerPrefs.Save();
        // Updates text displayed on screen to show current lives
        livesText.text = "LIVES: " + currentLives;
        Debug.Log("Gained life - lives remaining = " + currentLives);
    }

    public void RestartLevel()
    {
        // Restarts the current level the user is on, resetting coin collection status
        PlayerPrefs.SetInt("Coins", 0);
        PlayerPrefs.Save();

        // Resume game before reloading current level
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void GameOver()
    {
        // Resets checkpoint value - so the user will always be reset to the beginning of the level
        bool checkpoint = false;
        PlayerPrefs.SetInt("Checkpoint", checkpoint ? 1 : 0);
        PlayerPrefs.SetInt("Coins", 0);
        PlayerPrefs.Save();

        // Restart in-game time
        Time.timeScale = 1f;
        // Load first level if player has no lives
        SceneManager.LoadScene("Level1");
    }
}
