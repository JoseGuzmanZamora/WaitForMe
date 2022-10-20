using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    public GameObject objective;
    public int movementSpeed = 6;
    public bool facesRight = true;
    private Rigidbody rb;
    public bool resetForce = true;

    private void Start() {
        rb = GetComponent<Rigidbody> ();
    }

    private void FixedUpdate() {
        var target = objective.transform.position;
        var currentPosition = transform.position;

        var xDifference = target.x - currentPosition.x;
        var zDifference = target.z - currentPosition.z;
        OrientationProcess(xDifference);

        var newPosition = new Vector3(xDifference, transform.position.y, zDifference).normalized * (movementSpeed * Time.deltaTime);
        rb.MovePosition(transform.position + newPosition);
    }

    private void Update() {
        if (resetForce) rb.velocity = Vector3.zero;
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
}
