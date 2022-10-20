using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    public Sprite[] possibleSprites;
    // Start is called before the first frame update
    void Start()
    {
        var renderer = GetComponent<SpriteRenderer>();
        var spriteToUse = possibleSprites[UnityEngine.Random.Range(0, possibleSprites.Length - 1)];

        // set one of the random sprites
        renderer.sprite = spriteToUse;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
