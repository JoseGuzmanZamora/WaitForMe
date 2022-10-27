using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public int GhostCap = 200;
    public int CurrentGhostAmount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseGhostAmount()
    {
        CurrentGhostAmount ++;
    }

    public void DecreaseGhostAmount()
    {
        CurrentGhostAmount --;
    }
}
