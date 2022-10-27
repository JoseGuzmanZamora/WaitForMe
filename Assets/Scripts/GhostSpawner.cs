using UnityEngine;

public class GhostSpawner : MonoBehaviour
{
    public GameObject ghostPrefab;
    public GameObject objective;
    public float spawnCounter = 0f;
    public float spawnEvery = 5f;
    public SpawnerController parentManager;

    // Start is called before the first frame update
    void Start()
    {
        var parent = gameObject.transform.parent.gameObject;
        parentManager = parent.GetComponent<SpawnerController>();
    }

    // Update is called once per frame
    void Update()
    {
        spawnCounter += Time.deltaTime;

        if (spawnCounter >= spawnEvery)
        {
            spawnCounter = 0f;
            if (parentManager.CurrentGhostAmount >= parentManager.GhostCap)
            {
                Debug.Log("Reached ghost cap");
            }
            else
            {
                var newGhost = Instantiate(ghostPrefab.gameObject, new Vector3(transform.position.x, ghostPrefab.transform.position.y, transform.position.z), Quaternion.identity);
                var ghostController = newGhost.gameObject.GetComponent<GhostMovement>();
                ghostController.objective = objective;
                ghostController.parentController = parentManager;
                parentManager.IncreaseGhostAmount();
            }
        }
    }
}
