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
    private float temporalTimer = 1.5f;
    public Vector3 globalObjective;
    public Vector3 globalPosition;
    public int collisionCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        seekerPosition = seeker.transform.position;
        var mapCollider = map.GetComponent<BoxCollider>();
        mapSize = mapCollider.bounds.size;

        var initialPosition = GetRandomCorner();
        transform.position = new Vector3(initialPosition.x, transform.position.y, initialPosition.y);
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

                // Just check if we need to hide again
                var nearObjects = Physics.OverlapSphere(transform.position, 10f);
                PossiblyHideAgain();
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
                temporalTimer = 1.5f;
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
        foundObjective = false;
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
        collisionCounter ++;
        rb.AddForce(new Vector3(globalPosition.x * -1, globalPosition.y, globalPosition.z * -1) * 10f);
        var xDifference = objectivePosition.x - transform.position.x;
        var zDifference = objectivePosition.z - transform.position.z;
        var differenceVector = new Vector2(xDifference, zDifference);
        var perpendicular = Vector2.Perpendicular(differenceVector);
        if (UnityEngine.Random.Range(1, 3) % 2 == 0) perpendicular = -perpendicular;

        // Find perpendicular vector to see where to move
        normalMovement = false;
        //var zPerpendicular = 1f;
        //var xPerpendicular = (-zDifference)/xDifference;
        temporalDirection = new Vector3(perpendicular.x, transform.position.y, perpendicular.y);
        globalObjective = temporalDirection;
    }

    public void PossiblyHideAgain()
    {
        // Just check if we need to hide again
        var nearObjects = Physics.OverlapSphere(transform.position, 50f);

        foreach (var nearObject in nearObjects)
        {
            if (nearObject.gameObject.tag == "Gracie")
            {
                EstablishRandomObjective();
            }
        }
    }

    public Vector2 GetRandomCorner()
    {
        var mapPosition = map.transform.position;
        var maxZ = mapPosition.z + (mapSize.z / 2);
        var minZ = mapPosition.z - (mapSize.z / 2);
        var maxX = mapPosition.x + (mapSize.x / 2);
        var minX = mapPosition.x - (mapSize.x / 2);
        var threshold = 25f;

        var corner1 = new Vector2(minX + threshold, maxZ - threshold);
        var corner2 = new Vector2(minX + threshold, minZ + threshold);
        var corner3 = new Vector2(maxX - threshold, maxZ - threshold);
        var corner4 = new Vector2(maxX - threshold, minZ + threshold);
        var options = new List<Vector2> { corner1, corner2, corner3, corner4};
        
        return options[UnityEngine.Random.Range(0, options.Count)];
    }
}
