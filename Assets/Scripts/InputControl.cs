using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControl : MonoBehaviour
{
    public float initialScale = 1.0f;
    // sensitivity for controlling the point cloud transform
    private float heightAdjustSensitivity = 0.01f;
    private float scaleAdjustSensitivity = 0.01f;
    private float yawAdjustSensitivity = 0.15f;
    // ten times smaller
    private bool isRoomSize = false;
    private float modelScale = 0.1f;
    // table height
    public float tableHeight = 1.2f;

    // Use this for initialization
    void Start()
    {
        transform.localScale = new Vector3(initialScale, initialScale, initialScale);
        // model scale initial
        transform.localScale = new Vector3(modelScale, modelScale, modelScale);
        transform.position += new Vector3(0, tableHeight, 0);
    }
    
    // Input Control
    void Update()
    {
        if (!isRoomSize)
        {
            // Check for height adjustment
            if (OVRInput.Get(OVRInput.Button.Four) && OVRInput.Get(OVRInput.Button.PrimaryHandTrigger)&&!OVRInput.Get(OVRInput.Button.Three))
            {
                transform.Translate(new Vector3(0.0f, heightAdjustSensitivity, 0.0f));
                
            }
            if (OVRInput.Get(OVRInput.Button.Four) && OVRInput.Get(OVRInput.Button.SecondaryHandTrigger) && !OVRInput.Get(OVRInput.Button.Three))
            {
                transform.Translate(new Vector3(0.0f, -heightAdjustSensitivity, 0.0f));
            }
            // Check for yaw adjustment
            if (OVRInput.Get(OVRInput.Button.Three) && OVRInput.Get(OVRInput.Button.PrimaryHandTrigger) && !OVRInput.Get(OVRInput.Button.Four))
            {
                transform.Rotate(0.0f, -yawAdjustSensitivity, 0.0f);
            }
            if (OVRInput.Get(OVRInput.Button.Three) && OVRInput.Get(OVRInput.Button.SecondaryHandTrigger) && !OVRInput.Get(OVRInput.Button.Four))
            {
                transform.Rotate(0.0f, yawAdjustSensitivity, 0.0f);
            }
            // Check for scale adjustment
            if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger) && !OVRInput.Get(OVRInput.Button.Three) && !OVRInput.Get(OVRInput.Button.Four))
            {
                transform.localScale += new Vector3(scaleAdjustSensitivity, scaleAdjustSensitivity, scaleAdjustSensitivity);
            }
            if (OVRInput.Get(OVRInput.Button.SecondaryHandTrigger) && !OVRInput.Get(OVRInput.Button.Three) && !OVRInput.Get(OVRInput.Button.Four))
            {
                if (transform.localScale.x > scaleAdjustSensitivity * 4)
                    transform.localScale -= new Vector3(scaleAdjustSensitivity, scaleAdjustSensitivity, scaleAdjustSensitivity);
            }
        }
        else
        {
            // Check for height adjustment
            if (OVRInput.Get(OVRInput.Button.Four) && OVRInput.Get(OVRInput.Button.PrimaryHandTrigger) && !OVRInput.Get(OVRInput.Button.Three))
            {
                transform.Translate(new Vector3(0.0f, heightAdjustSensitivity, 0.0f));

            }
            if (OVRInput.Get(OVRInput.Button.Four) && OVRInput.Get(OVRInput.Button.SecondaryHandTrigger) && !OVRInput.Get(OVRInput.Button.Three))
            {
                transform.Translate(new Vector3(0.0f, -heightAdjustSensitivity, 0.0f));
            }

        }
    }

    public void Transfrom2modelScale()
    {
        if (isRoomSize == true)
        {
            transform.localScale = new Vector3(modelScale, modelScale, modelScale);
            isRoomSize = false;
            transform.Translate(new Vector3(0, tableHeight, 0));
        }
    }

    public void Transfrom2roomScale()
    {
        if (isRoomSize == false)
        {
            transform.localScale = new Vector3(initialScale,initialScale,initialScale);
            isRoomSize = true;
            transform.Translate(new Vector3(0, -tableHeight, 0));
        }

    }
}
