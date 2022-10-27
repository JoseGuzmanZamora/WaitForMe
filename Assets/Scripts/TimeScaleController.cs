using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleController : MonoBehaviour
{
    public MessageManager pause;
    public bool pausedGame = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var messagesShown = (pause?.shown ?? false);

        if (messagesShown)
        {
            Time.timeScale = 0;
            pausedGame = true;
        }
        else
        {
            Time.timeScale = 1;
            pausedGame = false;
        }
    }
}
