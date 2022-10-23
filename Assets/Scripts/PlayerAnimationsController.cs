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

    public bool animateFromInput;
    public HideController hideInfo;
    
    void FixedUpdate()
    {
        float horizontalPress = 0f;
        float verticalPress = 0f;

        if (animateFromInput)
        {
            horizontalPress = Input.GetAxis("Horizontal");
            verticalPress = Input.GetAxis("Vertical");
        }
        else
        {
            // Animation for joy, get press from difference movement
            horizontalPress = hideInfo.objectivePosition.x - hideInfo.transform.position.x;
            verticalPress = hideInfo.objectivePosition.z - hideInfo.transform.position.z;
        }

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
