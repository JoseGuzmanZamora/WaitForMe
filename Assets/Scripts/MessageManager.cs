using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageManager : MonoBehaviour
{
    public bool hideOnStart;
    public bool shown;
    private RectTransform canvasTransform;
    private Vector3 originalTransform;
    // Start is called before the first frame update
    void Start()
    {
        canvasTransform = gameObject.GetComponent<RectTransform>();
        originalTransform = canvasTransform.position;
        if (hideOnStart) HideMe();
    }

    // Update is called once per frame
    void Update()
    {
        if (!shown)
        {
            if (Input.GetKeyDown(KeyCode.P)) ShowMe();
        }
    }

    public void ShowMe()
    {
        CanvasGroup cg = this.gameObject.GetComponent<CanvasGroup>();
        cg.interactable = true;
        cg.alpha = 1;
        shown = true;
        canvasTransform.position = new Vector3(originalTransform.x, originalTransform.y, originalTransform.z);
    }

    public void HideMe()
    {
        CanvasGroup cg = this.gameObject.GetComponent<CanvasGroup>();
        cg.interactable = false;
        cg.alpha = 0;
        shown = false;
        canvasTransform.position = new Vector3(originalTransform.x - 5000, originalTransform.y, originalTransform.z);
    }
}
