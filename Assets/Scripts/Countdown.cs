using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Countdown : MonoBehaviour
{
    // Sets start time of each level to 200 seconds
    private float remainingTime = 0f;
    private float startTime = 200f;

    [SerializeField]
    private PlayerLives playerLives;

    private TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        remainingTime = startTime;
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        // Subtracts time remaining, at each timestep
        remainingTime -= (1 * Time.deltaTime);
        // "0" to format the time remaining, to only show whole seconds
        text.text = "TIME: " + remainingTime.ToString("0");

        if(remainingTime <= 0)
        {
            // Player will lose a life if they run out of time
            playerLives.LoseLife(1);
        }
    }
}
