using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float zOffset;
    public float yOffset;
    public GameObject Gracie;
    public GameObject mapObject;
    private float cameraHeight;
    private float cameraWidth;
    public Vector2 minPosition;
    public Vector2 maxPosition;
    void Start()
    {
        zOffset = transform.position.z;
        yOffset = transform.position.y;


        var cam = gameObject.GetComponent<Camera>();
        cam = Camera.main;
        cameraHeight = 2f * cam.orthographicSize;
        cameraWidth = cameraHeight * cam.aspect;

        // Calculate the min and max position based on the camera and map
        var mapSize = mapObject.GetComponent<Renderer>().bounds.size;
        var minPositionX = (mapObject.transform.position.x - (mapSize.x / 2)) + (cameraWidth / 2);
        var maxPositionX = (mapObject.transform.position.x + (mapSize.x / 2)) - (cameraWidth / 2);
        var minPositionZ = (mapObject.transform.position.z - (mapSize.z / 2)) + (cameraHeight / 2) - 27;
        var maxPositionZ = (mapObject.transform.position.z + (mapSize.z / 2)) - (cameraHeight / 2) - 103.25f;

        minPosition = new Vector2(minPositionX, minPositionZ);
        maxPosition = new Vector2(maxPositionX, maxPositionZ);
    }

    private void FixedUpdate() {
        if (Gracie != null && Gracie.transform.position != transform.position)
        {
            var GracieTransform = Gracie.transform.position;
            Vector3 targetPosition = new Vector3(GracieTransform.x, transform.position.y, GracieTransform.z + zOffset);
            targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
            targetPosition.z = Mathf.Clamp(targetPosition.z, minPosition.y, maxPosition.y);
            transform.position = Vector3.Lerp(transform.position, targetPosition, 0.1f);
        }
    }
}
