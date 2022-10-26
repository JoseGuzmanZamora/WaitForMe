using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationsInitialization : MonoBehaviour
{
    public List<GameObject> decorationPefabs;
    public int amountPerDecoration = 10;
    public List<GameObject> ambients;
    public int amountPerAmbient = 5;
    public float limitsThreshold = 25f;
    private Vector3 mapSize;
    // Start is called before the first frame update
    void Start()
    {
        var mapCollider = GetComponent<BoxCollider>();
        mapSize = mapCollider.bounds.size;

        foreach (var decoration in decorationPefabs)
        {
            for(int i = 0; i < amountPerDecoration; i++) InitializeObject(decoration);
        }

        foreach (var ambient in ambients)
        {
            for(int i = 0; i < amountPerAmbient; i++) InitializeObject(ambient, true);
        }
    }

    void InitializeObject(GameObject objectToInitialize, bool useThreshold = false)
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
