using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float zOffset;
    public float yOffset;
    public GameObject Gracie;
    void Start()
    {
        zOffset = transform.position.z;
        yOffset = transform.position.y;
    }

    private void FixedUpdate() {
        if (Gracie != null && Gracie.transform.position != transform.position)
        {
            var GracieTransform = Gracie.transform.position;
            Vector3 targetPosition = new Vector3(GracieTransform.x, transform.position.y, GracieTransform.z + zOffset);
            transform.position = Vector3.Lerp(transform.position, targetPosition, 0.1f);
        }
    }
}
