using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float zOffset = -20;
    public GameObject Gracie;
    void Start()
    {
        
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
