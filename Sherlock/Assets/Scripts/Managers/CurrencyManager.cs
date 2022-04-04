using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyManager : MonoBehaviour
{
    public PlayerInventory playerInv;
    public Text coinText;
    void Start()
    {
        UpdateCoins();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void UpdateCoins()
    {
        coinText.text = "" + playerInv.coins;
    }
}
