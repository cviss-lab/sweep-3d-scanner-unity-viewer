using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustTransform : MonoBehaviour
{
    public float initialScale = 1.0f;
    // toggle and sensitivity for controlling the point cloud transform
    private bool userCanAdjustHeight = true;
    private float heightAdjustSensitivity = 0.01f;
    private bool userCanAdjustScale = true;
    private float scaleAdjustSensitivity = 0.01f;
    private bool userCanAdjustYaw = true;
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

    // Update is called once per frame
    void Update()
    {
        if (userCanAdjustHeight)
        {
            if (OVRInput.Get(OVRInput.Button.Four) && OVRInput.Get(OVRInput.Button.PrimaryHandTrigger)&&!OVRInput.Get(OVRInput.Button.Three))
            {
                transform.Translate(new Vector3(0.0f, heightAdjustSensitivity, 0.0f));
                
            }
            if (OVRInput.Get(OVRInput.Button.Four) && OVRInput.Get(OVRInput.Button.SecondaryHandTrigger) && !OVRInput.Get(OVRInput.Button.Three))
            {
                transform.Translate(new Vector3(0.0f, -heightAdjustSensitivity, 0.0f));
            }
            
        }
        // Check for yaw adjustment
        if (userCanAdjustYaw)
        {
            if (OVRInput.Get(OVRInput.Button.Three) && OVRInput.Get(OVRInput.Button.PrimaryHandTrigger) && !OVRInput.Get(OVRInput.Button.Four))
            {
                transform.Rotate(0.0f, -yawAdjustSensitivity, 0.0f);
            }
            if (OVRInput.Get(OVRInput.Button.Three) && OVRInput.Get(OVRInput.Button.SecondaryHandTrigger) && !OVRInput.Get(OVRInput.Button.Four))
            {
                transform.Rotate(0.0f, yawAdjustSensitivity, 0.0f);
            }
        }
        // Check for scale adjustment
        if (userCanAdjustScale)
        {
            if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger)&& !OVRInput.Get(OVRInput.Button.Three)&& !OVRInput.Get(OVRInput.Button.Four))
            {
                transform.localScale += new Vector3(scaleAdjustSensitivity, scaleAdjustSensitivity, scaleAdjustSensitivity);
            }
            if (OVRInput.Get(OVRInput.Button.SecondaryHandTrigger) && !OVRInput.Get(OVRInput.Button.Three) && !OVRInput.Get(OVRInput.Button.Four))
            {
                if (transform.localScale.x > scaleAdjustSensitivity * 4)
                    transform.localScale -= new Vector3(scaleAdjustSensitivity, scaleAdjustSensitivity, scaleAdjustSensitivity);
            }
        }
    }

    public void Transfrom2modelScale()
    {
        if (isRoomSize == true)
        {
            userCanAdjustScale = true;
            userCanAdjustYaw = true;
            transform.localScale = new Vector3(modelScale, modelScale, modelScale);
            isRoomSize = false;
            transform.Translate(new Vector3(0, tableHeight, 0));
        }
    }

    public void Transfrom2roomScale()
    {
        if (isRoomSize == false)
        {
            userCanAdjustScale = false;
            userCanAdjustYaw = false;
            transform.localScale = new Vector3(initialScale,initialScale,initialScale);
            isRoomSize = true;
            transform.Translate(new Vector3(0, -tableHeight, 0));
        }

    }
}
