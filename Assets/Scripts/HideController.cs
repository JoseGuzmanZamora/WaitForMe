using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideController : MonoBehaviour
{
    public GameObject seeker;
    public GameObject map;
    private Vector3 seekerPosition;
    private Vector3 mapSize;
    // Start is called before the first frame update
    void Start()
    {
        seekerPosition = seeker.transform.position;
        var mapCollider = map.GetComponent<BoxCollider>();
        mapSize = mapCollider.bounds.size;

        // Establish initial random position
        EstablishRandomObjective();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void EstablishRandomObjective()
    {
        var mapPosition = map.transform.position;

        var maxZ = mapPosition.z + (mapSize.z / 2);
        var minZ = mapPosition.z - (mapSize.z / 2);
        var maxX = mapPosition.x + (mapSize.x / 2);
        var minX = mapPosition.x - (mapSize.x / 2);


        var randomPosition = new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ));
        Instantiate(seeker, randomPosition, Quaternion.identity);
        Debug.Log(randomPosition);
    }
}
