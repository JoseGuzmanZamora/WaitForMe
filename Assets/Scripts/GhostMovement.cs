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

    private void Start() {
        rb = GetComponent<Rigidbody> ();
    }

    private void FixedUpdate() {
        var target = objective.transform.position;
        var currentPosition = transform.position;

        var xDifference = target.x - currentPosition.x;
        var zDifference = target.z - currentPosition.z;
        OrientationProcess(xDifference);

        var newPosition = new Vector3(xDifference, transform.position.y, zDifference).normalized * (movementSpeed * Time.fixedDeltaTime);
        if (zDifference != 0 && xDifference == 0)
        {
            newPosition = new Vector3(xDifference, transform.position.y, zDifference * 2) * (movementSpeed * Time.fixedDeltaTime);
        }
        //rb.MovePosition(transform.position + newPosition);
        rb.AddForce(newPosition);
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
}
