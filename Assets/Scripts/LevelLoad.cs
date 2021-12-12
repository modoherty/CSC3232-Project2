using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoad : MonoBehaviour
{
    // The animator component, which is the fading between levels
    [SerializeField]
    private Animator animator;

    public void LoadNextLevel()
    {
        // Loads the next level in the game
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int index)
    {
        // Starts the fade animation, and plays for 0.75 seconds
        animator.SetTrigger("LevelStart");
        yield return new WaitForSeconds(0.75f);

        // Loads the next level
        SceneManager.LoadScene(index);
    }
}
