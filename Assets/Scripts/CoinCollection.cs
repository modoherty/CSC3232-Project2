using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinCollection : MonoBehaviour
{
    // Setting the TextMeshPro
    [SerializeField]
    private TextMeshProUGUI coinsText;
    // Start is called before the first frame update
    void Start()
    {
        // Initialising coin count display
        coinsText = GetComponent<TextMeshProUGUI>();
        coinsText.text = "COINS: " + PlayerPrefs.GetInt("Coins");
    }

    // Update is called once per frame
    void Update()
    {
        // Updates the text on screen to display the player's coin count
        coinsText.text = "COINS: " + PlayerPrefs.GetInt("Coins");
    }
}
