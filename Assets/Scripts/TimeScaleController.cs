using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TimeScaleController : MonoBehaviour
{
    public List<MessageManager> messages;
    public bool pausedGame = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var messagesShown = messages.Any(m => m.shown);

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
