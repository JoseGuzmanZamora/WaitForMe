using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    public float movementSpeed = 5;

    void Start()
    {
        rb = GetComponent<Rigidbody> ();
    }

    private void Update() {
        rb.velocity = Vector3.zero;
    }

    private void FixedUpdate() {
        var horizontalPress = Input.GetAxis("Horizontal");
        var verticalPress = Input.GetAxis("Vertical");
        var newPosition = new Vector3(horizontalPress, transform.position.y, verticalPress).normalized * (movementSpeed * Time.fixedDeltaTime);
        if (verticalPress != 0 && horizontalPress == 0)
        {
            newPosition = new Vector3(horizontalPress, transform.position.y, verticalPress * 2) * (movementSpeed * Time.fixedDeltaTime);
        }
        //rb.MovePosition(transform.position + newPosition);
        rb.AddForce(newPosition);
    }
}
