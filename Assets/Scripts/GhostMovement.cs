using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    public GameObject objective;
    public float movementSpeed = 0.002f;
    public bool facesRight = true;
    private Rigidbody rb;
    public bool resetForce = true;
    public int life = 100;
    private float lifeCounter = 2f;
    private Vector3 globalPosition;
    private bool suspendMovement = false;
    private float suspensionTimer = 1f;
    private Vector3 temporalDirection;
    private bool normalMovement = true;
    private float temporalTimer = 1.5f;
    private int collisionCounter = 0;

    private void Start() {
        rb = GetComponent<Rigidbody> ();
    }

    private void FixedUpdate() {
        Vector3 target = new Vector3();
        if (normalMovement)
        {
            target = objective.transform.position;
        }
        else
        {
            target = temporalDirection;
            temporalTimer -= Time.fixedDeltaTime;
            if (temporalTimer <= 0)
            {
                normalMovement = true;
                temporalTimer = 1.5f;
                target = objective.transform.position;
            }
        }
        var currentPosition = transform.position;

        var xDifference = target.x - currentPosition.x;
        var zDifference = target.z - currentPosition.z;
        OrientationProcess(xDifference);

        var newPosition = new Vector3(xDifference, transform.position.y, zDifference).normalized * (movementSpeed * Time.fixedDeltaTime);
        if (zDifference != 0 && xDifference == 0)
        {
            newPosition = new Vector3(xDifference, transform.position.y, zDifference * 2) * (movementSpeed * Time.fixedDeltaTime);
        }
        globalPosition = newPosition;
        //rb.MovePosition(transform.position + newPosition);
        if (suspendMovement is false) 
        {
            rb.AddForce(newPosition);
        }
        else
        {
            suspensionTimer -= Time.fixedDeltaTime;
            if (suspensionTimer <= 0)
            {
                suspensionTimer = 1f;
                suspendMovement = false;
            }
        }
    }

    private void Update() {
        if (resetForce) rb.velocity = Vector3.zero;

        // Check out the life counter and destroy gameobject so it wont appear floating around
        if (life < 0)
        {
            lifeCounter -= Time.deltaTime;
            if (lifeCounter < 0) Destroy(gameObject);
        }
    }

    public void MakeDamage(int amount)
    {
        life -= amount;
    }

    public void OrientationProcess(float xDifference)
    {
        // Move the sprite
        if (xDifference > 0)
        {
            if (facesRight)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
        }
        else if (xDifference < 0)
        {
            if (facesRight)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Gracie")
        {
            suspendMovement = true;
            rb.AddForce(new Vector3(globalPosition.x * -1, globalPosition.y, globalPosition.z * -1) * 20f);
        }
        else if (other.gameObject.tag == "Environment" && normalMovement)
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
        rb.AddForce(new Vector3(globalPosition.x * -1, globalPosition.y, globalPosition.z * -1) * 20f);
        var xDifference = objective.transform.position.x - transform.position.x;
        var zDifference = objective.transform.position.z - transform.position.z;
        var differenceVector = new Vector2(xDifference, zDifference);
        var perpendicular = Vector2.Perpendicular(-differenceVector);
        if (UnityEngine.Random.Range(1, 3) % 2 == 0) perpendicular = -perpendicular;

        // Find perpendicular vector to see where to move
        normalMovement = false;
        temporalDirection = new Vector3(perpendicular.x, transform.position.y, perpendicular.y);
    }
}
