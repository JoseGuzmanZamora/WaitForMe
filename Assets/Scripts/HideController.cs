using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideController : MonoBehaviour
{
    public float movementSpeed = 5f;
    public GameObject seeker;
    public GameObject map;
    private Vector3 seekerPosition;
    private Vector3 mapSize;
    private bool foundObjective = false;
    public Vector3 objectivePosition;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        seekerPosition = seeker.transform.position;
        var mapCollider = map.GetComponent<BoxCollider>();
        mapSize = mapCollider.bounds.size;

        // Establish initial random position
        EstablishRandomObjective();
    }

    private void Update() {
        rb.velocity = Vector3.zero;
    }

    private void FixedUpdate() {
        if (!foundObjective) 
        {
            MoveTowardsObjective();
            foundObjective = FoundObjective();
        }
        else
        {
            objectivePosition = transform.position;
        }
    }

    private bool FoundObjective()
    {
        var xDifference = Math.Abs(objectivePosition.x - transform.position.x);
        var zDifference = Math.Abs(objectivePosition.z - transform.position.z);

        var threshold = 5f;
        if (xDifference <= threshold && zDifference <= threshold)
        {
            return true;
        }
        return false;
    }

    void MoveTowardsObjective()
    {
        //var direction = (objectivePosition - transform.position).normalized * (Time.fixedDeltaTime * movementSpeed);
        //rb.MovePosition(transform.position + direction);

        var xDifference = objectivePosition.x - transform.position.x;
        var zDifference = objectivePosition.z - transform.position.z;

        var newPosition = new Vector3(xDifference, transform.position.y, zDifference).normalized * (movementSpeed * Time.fixedDeltaTime);
        if (zDifference != 0 && xDifference == 0)
        {
            newPosition = new Vector3(xDifference, transform.position.y, zDifference * 2) * (movementSpeed * Time.fixedDeltaTime);
        }
        rb.AddForce(newPosition);
    }

    void EstablishRandomObjective()
    {
        var mapPosition = map.transform.position;

        var maxZ = mapPosition.z + (mapSize.z / 2);
        var minZ = mapPosition.z - (mapSize.z / 2);
        var maxX = mapPosition.x + (mapSize.x / 2);
        var minX = mapPosition.x - (mapSize.x / 2);


        objectivePosition = new Vector3(UnityEngine.Random.Range(minX, maxX), 0, UnityEngine.Random.Range(minZ, maxZ));
    }
}
