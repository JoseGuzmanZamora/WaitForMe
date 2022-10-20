using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject phoneBomb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space"))
        {
            Instantiate(phoneBomb, new Vector3(transform.position.x, 0, transform.position.z), Quaternion.identity);
        }
    }
}
