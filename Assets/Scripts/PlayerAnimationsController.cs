using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationsController : MonoBehaviour
{
    public Animator frontAnimator;
    public Animator backAnimator;
    public Animator sideAnimator;

    public GameObject frontObject;
    public GameObject backObject;
    public GameObject sideObject;

    void Start()
    {
        
    }
    
    void FixedUpdate()
    {
        var horizontalPress = Input.GetAxis("Horizontal");
        var verticalPress = Input.GetAxis("Vertical");

        if (horizontalPress > 0)
        {
            ActivateSingeObject(sideObject);
            sideAnimator.SetBool("moving", true);
            sideObject.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (horizontalPress < 0)
        {
            ActivateSingeObject(sideObject);
            sideAnimator.SetBool("moving", true);
            sideObject.transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (verticalPress < 0)
        {
            ActivateSingeObject(frontObject);
            frontAnimator.SetBool("moving", true);
        }
        else if (verticalPress > 0)
        {
            ActivateSingeObject(backObject);
            backAnimator.SetBool("moving", true);
        }
        else
        {
            if (frontObject.activeSelf) frontAnimator.SetBool("moving", false);
            if (backObject.activeSelf) backAnimator.SetBool("moving", false);
            if (sideObject.activeSelf) sideAnimator.SetBool("moving", false);
        }
    }

    private void ActivateSingeObject(GameObject objectToActivate)
    {
        if (!objectToActivate.activeSelf)
        {
            objectToActivate.SetActive(true);
        }

        if (objectToActivate == frontObject)
        {
            if (backObject.activeSelf) 
            {
                backAnimator.WriteDefaultValues();
                backObject.SetActive(false);
            }
            if (sideObject.activeSelf)
            {
                sideAnimator.WriteDefaultValues();
                sideObject.SetActive(false);
            }
        }
        else if (objectToActivate == backObject)
        {
            if (sideObject.activeSelf) 
            {
                sideAnimator.WriteDefaultValues();
                sideObject.SetActive(false);
            }
            if (frontObject.activeSelf) 
            {
                frontAnimator.WriteDefaultValues();
                frontObject.SetActive(false);
            }
        }
        else
        {
            if (backObject.activeSelf) 
            {
                backAnimator.WriteDefaultValues();
                backObject.SetActive(false);
            }
            if (frontObject.activeSelf) 
            {
                frontAnimator.WriteDefaultValues();
                frontObject.SetActive(false);
            }
        }
    }
}
