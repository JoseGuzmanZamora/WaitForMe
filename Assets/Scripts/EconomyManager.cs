using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    public int totalAvailableCoins = 0;
    public int availableMoney = 0;
    private const int CoinValue = 25;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Coin")
        {
            // Found one coin, we should adjust the amounts
            totalAvailableCoins --;
            availableMoney += CoinValue;
            Destroy(other.gameObject);
        }
    }
}
