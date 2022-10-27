using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    public GameObject coinPrefab;
    public GameObject map;
    public int totalAvailableCoins = 0;
    public int availableMoney = 0;
    private const int CoinValue = 25;
    public int totalInitialCoins = 100;
    public float inflationCounter = 0f;
    public float inflationLimit = 5f;
    private Vector3 mapSize;
    public float limitsThreshold = 30f;
    // Start is called before the first frame update
    void Start()
    {
        var mapCollider = map.GetComponent<BoxCollider>();
        mapSize = mapCollider.bounds.size;

        for (int i = 0; i < totalInitialCoins; i++)
        {
            InitializeObject(coinPrefab, true);
            totalAvailableCoins ++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        inflationCounter += Time.deltaTime;
        if (inflationCounter >= inflationLimit)
        {
            InitializeObject(coinPrefab, true);
            totalAvailableCoins++;
            inflationCounter = 0;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Coin")
        {
            // Found one coin, we should adjust the amounts
            totalAvailableCoins --;
            availableMoney += CoinValue;
            // find parent
            var parent = other.gameObject.transform.parent.gameObject;
            if (parent != null) 
            {
                Destroy(parent);
            }
        }
    }

    public void InitializeObject(GameObject objectToInitialize, bool useThreshold = false)
    {
        var mapPosition = transform.position;

        var maxZ = (mapPosition.z + (mapSize.z / 2));
        var minZ = (mapPosition.z - (mapSize.z / 2));
        var maxX = (mapPosition.x + (mapSize.x / 2));
        var minX = (mapPosition.x - (mapSize.x / 2));

        if (useThreshold)
        {
            maxZ -= limitsThreshold;
            minZ += limitsThreshold;
            maxX -= limitsThreshold;
            minX += limitsThreshold;
        }


        var randomPosition = new Vector3(UnityEngine.Random.Range(minX, maxX), objectToInitialize.transform.position.y, UnityEngine.Random.Range(minZ, maxZ));
        Instantiate(objectToInitialize, randomPosition, Quaternion.identity);
    }
}
