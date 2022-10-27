using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Text candyText;
    public int initialCandyAmount = 200;
    public int currentCandyAmount = 0;
    private EconomyManager econ;
    // Start is called before the first frame update
    void Start()
    {
        currentCandyAmount = initialCandyAmount;
        econ = gameObject.GetComponent<EconomyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        candyText.text = $"Candy: {currentCandyAmount.ToString()}";
    }

    public void BuyCandyInGame1()
    {
        var cost = 200;
        if (cost > econ.availableMoney)
        {
            Debug.Log("Cant buy");
        }
        else
        {
            currentCandyAmount += 100;
            econ.availableMoney -= cost;
        }
    }

    public void BuyCandyInGame2()
    {
        var cost = 275;
        if (cost > econ.availableMoney)
        {
            Debug.Log("Cant buy");
        }
        else
        {
            currentCandyAmount += 150;
            econ.availableMoney -= cost;
        }
    }

    public void BuyCandyInGame3()
    {
        var cost = 500;
        if (cost > econ.availableMoney)
        {
            Debug.Log("Cant buy");
        }
        else
        {
            currentCandyAmount += 300;
            econ.availableMoney -= cost;
        }
    }
}
