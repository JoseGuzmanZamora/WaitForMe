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
    private Vector3 objectivePosition;
    private Rigidbody rb;
    private bool normalMovement = true;
    private Vector3 temporalDirection;
    private float temporalTimer = 3f;
    public Vector3 globalObjective;
    public Vector3 globalPosition;

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
        if (normalMovement)
        {
            globalObjective = objectivePosition;
            if (!foundObjective) 
            {
                MoveTowardsObjective();
                foundObjective = FoundObjective();
            }
            else
            {
                objectivePosition = transform.position;
                globalObjective = objectivePosition;
            }
        }
        else
        {
            // Move to other direction to avoid collides
            temporalTimer -= Time.fixedDeltaTime;
            var xDifference = temporalDirection.x - transform.position.x;
            var zDifference = temporalDirection.z - transform.position.z;

            var newPosition = new Vector3(xDifference, transform.position.y, zDifference).normalized * (movementSpeed * Time.fixedDeltaTime);
            if (zDifference != 0 && xDifference == 0)
            {
                newPosition = new Vector3(xDifference, transform.position.y, zDifference * 2) * (movementSpeed * Time.fixedDeltaTime);
            }
            //globalPosition = newPosition;
            rb.AddForce(newPosition);

            if (temporalTimer <= 0)
            {
                temporalTimer = 3f;
                normalMovement = true;
            }
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
        globalPosition = newPosition;
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
        globalObjective = objectivePosition;
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Environment" && normalMovement)
        {
            CollisionMovementChanger();
        }
    }

    private void OnCollisionStay(Collision other) {
        if (other.gameObject.tag == "Environment" && normalMovement)
        {
            CollisionMovementChanger();
        }
    }

    private void CollisionMovementChanger()
    {
        rb.AddForce(new Vector3(globalPosition.x * -1, globalPosition.y, globalPosition.z * -1) * 10f);
        var xDifference = objectivePosition.x - transform.position.x;
        var zDifference = objectivePosition.z - transform.position.z;
        var differenceVector = new Vector2(xDifference, zDifference);
        Debug.Log(differenceVector);
        var perpendicular = Vector2.Perpendicular(differenceVector);
        Debug.Log(perpendicular);

        // Find perpendicular vector to see where to move
        normalMovement = false;
        //var zPerpendicular = 1f;
        //var xPerpendicular = (-zDifference)/xDifference;
        temporalDirection = new Vector3(perpendicular.x, transform.position.y, perpendicular.y);
        globalObjective = temporalDirection;
    }
}
