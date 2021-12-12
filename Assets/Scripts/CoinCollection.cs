using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinCollection : MonoBehaviour
{
    // Setting the TextMeshPro
    [SerializeField]
    private TextMeshProUGUI coinsText;

    void Start()
    {
        // Initialising coin count display
        coinsText = GetComponent<TextMeshProUGUI>();
        coinsText.text = "COINS: " + PlayerPrefs.GetInt("Coins");
    }


    void Update()
    {
        // Updates the text on screen to display the player's coin count
        coinsText.text = "COINS: " + PlayerPrefs.GetInt("Coins");
    }
}
