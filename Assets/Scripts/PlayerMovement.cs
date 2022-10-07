using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    public int movementSpeed = 5;

    void Start()
    {
        rb = GetComponent<Rigidbody> ();
    }

    private void FixedUpdate() {
        var horizontalPress = Input.GetAxis("Horizontal");
        var verticalPress = Input.GetAxis("Vertical");
        var newPosition = new Vector3(horizontalPress, transform.position.y, verticalPress).normalized * (movementSpeed * Time.deltaTime);
        rb.MovePosition(transform.position + newPosition);
    }
}
