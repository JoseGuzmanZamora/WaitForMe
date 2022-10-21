using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject phoneBomb;
    public Camera mainCamera;
    public float force = 100;
    public LayerMask groundMask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
    }

    private void Fire()
    {
        var (success, position) = GetMousePosition();
        if (success)
        {
            // Calculate the direction
            var direction = position - transform.position;
            direction.y = 0;

            var possibleX = transform.position.x;
            var possibleZ = transform.position.z;
            var xChange = 0;
            if (direction.x > 0)
            {
                xChange = 2;
            }
            else if (direction.x < 0)
            {
                xChange = -2;
            }
            possibleX += xChange;
            var verifiedDirection = position - (new Vector3(transform.position.x + xChange, transform.position.y, transform.position.z));

            var newObject = Instantiate(phoneBomb, new Vector3(possibleX, transform.position.y, possibleZ), Quaternion.identity);
            newObject.transform.forward = verifiedDirection;
        }
    }

    private (bool success, Vector3 position) GetMousePosition()
    {
        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, groundMask))
        {
            return (success: true, position: hitInfo.point);
        }
        else
        {
            return (success: false, position: Vector3.zero);
        }
    }
}
